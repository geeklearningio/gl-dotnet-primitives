namespace GeekLearning.Primitives
{
    using System;

    public class QualifiedId<TId> : IQualifiedId
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