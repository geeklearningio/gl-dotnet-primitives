namespace GeekLearning.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class QualifiedId<TId> : IQualifiedId<TId>
    {
        private string idAsString;
        private string full;

        public Qualifier Qualifier { get; private set; }
        public TId Id { get; private set; }
        public string Full => full ?? (full = this.ToFullString());
        public string IdAsString => idAsString ?? (idAsString = this.ToUnqualifiedString());

        public QualifiedId(Qualifier qualifier, TId id)
        {
            this.Id = id;
            this.Qualifier = qualifier;
        }


        public QualifiedId(string qualifiedId)
        {
            int indexOfFirstColon = qualifiedId.IndexOf(':');
            if (indexOfFirstColon < 0)
            {
                throw new ArgumentException("This is not a valid qualified id. Expected format is 'qualifier:id'.", nameof(qualifiedId));
            }

            this.Qualifier = qualifiedId.Substring(0, indexOfFirstColon);
            this.Id = this.ParseIdFromString(qualifiedId.Substring(indexOfFirstColon + 1));
        }

        protected virtual TId ParseIdFromString(string str)
        {
            var type = typeof(TId);

            if (type == typeof(Guid))
            {
                return (TId)Convert.ChangeType(Guid.Parse(str), typeof(TId));
            }
            else if (type.Namespace == "System" && type.IsConstructedGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (genericTypeDefinition.Name.StartsWith("Tuple`") || genericTypeDefinition.Name.StartsWith("ValueTuple`"))
                {
                    var values = str
                        .Substring(1, str.Length - 2)
                        .Split(new string[] { ", " }, StringSplitOptions.None)
                        .Zip(type.GenericTypeArguments, (valueAsString, valueType) =>
                        {
                            if (valueType == typeof(Guid))
                            {
                                return (object)Guid.Parse(valueAsString);
                            }
                            return Convert.ChangeType(valueAsString, valueType);
                        }).ToArray();
                    return (TId)Activator.CreateInstance(type, values);
                }
            }

            return (TId)Convert.ChangeType(str, typeof(TId));
        }

        protected virtual string ToUnqualifiedString()
        {
            return $"{this.Id}";
        }

        protected virtual string ToFullString()
        {
            return $"{this.Qualifier}:{this.ToUnqualifiedString()}";
        }

        public override string ToString()
        {
            return this.Full;
        }

        public override bool Equals(object obj)
        {
            var typedId = obj as IQualifiedId<TId>;
            if(typedId != null)
            {
                return this.Equals(typedId);
            }

            var genericId = obj as IQualifiedId;
            if (genericId != null)
            {
                return this.Equals(typedId);
            }

            return false;
        }

        public bool Equals(IQualifiedId<TId> other)
        {
            return this.Qualifier == other.Qualifier && this.Id.Equals(other.Id);
        }

        public bool Equals(IQualifiedId other)
        {
            return this.Qualifier == other.Qualifier && this.IdAsString.Equals(other.IdAsString);
        }

        public override int GetHashCode()
        {
            return -1528073796 + EqualityComparer<string>.Default.GetHashCode(Full);
        }

        public static bool operator ==(QualifiedId<TId> a, QualifiedId<TId> other) => a.Equals(other);
        public static bool operator !=(QualifiedId<TId> a, QualifiedId<TId> other) => !a.Equals(other);

        public static bool operator ==(QualifiedId<TId> a, IQualifiedId other) => a.Equals(other);
        public static bool operator !=(QualifiedId<TId> a, IQualifiedId other) => !a.Equals(other);


        public static implicit operator TId(QualifiedId<TId> id) => id.Id;

        public static implicit operator string(QualifiedId<TId> id) => id.Full;

        public static implicit operator QualifiedId<TId>(string qualifiedId) => new QualifiedId<TId>(qualifiedId);
    }
}