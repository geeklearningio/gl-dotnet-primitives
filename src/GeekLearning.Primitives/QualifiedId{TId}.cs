namespace GeekLearning.Primitives
{
    using System;
    using System.Linq;

    public class QualifiedId<TId> : IQualifiedId<TId>
    {
        private string idAsString;
        private string full;

        public string Qualifier { get; private set; }
        public TId Id { get; private set; }
        public string Full => full ?? (full = this.ToFullString());
        public string IdAsString => idAsString ?? (idAsString = this.ToUnqualifiedString());

        public QualifiedId(string qualifier, TId id)
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

        public static implicit operator TId(QualifiedId<TId> id)
        {
            return id.Id;
        }

        public static implicit operator string(QualifiedId<TId> id)
        {
            return id.Full;
        }

        public static implicit operator QualifiedId<TId>(string qualifiedId)
        {
            return new QualifiedId<TId>(qualifiedId);
        }
    }
}