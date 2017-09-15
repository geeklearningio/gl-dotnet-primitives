using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLearning.Primitives
{
    public class Qualifier : IEquatable<Qualifier>
    {
        private readonly string name;

        public Qualifier(string name)
        {
            this.name = name;
        }

        public bool Equals(Qualifier other)
        {
            return other.name.Equals(this.name);
        }

        public override string ToString()
        {
            return name;
        }

        public static implicit operator Qualifier(string name)
        {
            return new Qualifier(name);
        }

        public QualifiedId<T> Join<T>(T value)
        {
            return new QualifiedId<T>(this.name, value);
        }

        public static QualifiedId<Guid> operator +(Qualifier qualifier, Guid guid)
        {
            return new QualifiedGuid(qualifier.name, guid);
        }

        public static QualifiedId<int> operator +(Qualifier qualifier, int value)
        {
            return qualifier.Join(value);
        }

        public static QualifiedId<long> operator +(Qualifier qualifier, long value)
        {
            return qualifier.Join(value);
        }

        public static QualifiedId<string> operator +(Qualifier qualifier, string value)
        {
            return qualifier.Join(value);
        }
    }
}
