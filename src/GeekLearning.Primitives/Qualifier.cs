using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLearning.Primitives
{
    public class Qualifier : IEquatable<Qualifier>, IEquatable<string>
    {
        private readonly string name;

        public Qualifier(string name) => this.name = name;

        public bool Equals(Qualifier other) => this.name.Equals(other?.name);

        public override bool Equals(object obj)
        {
            var other = obj as Qualifier;
            if (other != null)
            {
                return other.name.Equals(this.name);
            }

            var str = obj as string;
            if (str != null)
            {
                return this == str;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override string ToString() => name;

        public static implicit operator Qualifier(string name) => new Qualifier(name);

        public static explicit operator string(Qualifier qualifier) => qualifier.name;

        public static bool operator ==(Qualifier a, Qualifier b) => a?.name == b?.name;
        public static bool operator !=(Qualifier a, Qualifier b) => a?.name != b?.name;

        public static bool operator ==(Qualifier qualifier, string name) => qualifier?.name == name;
        public static bool operator !=(Qualifier qualifier, string name) => qualifier?.name != name;

        public QualifiedId<T> MakeId<T>(T value) => new QualifiedId<T>(this.name, value);

        public Qualifier Merge(Qualifier other) => new Qualifier(this.name + other.name);

        public bool Equals(string other)
        {
            return this.name == other;
        }

        public static QualifiedId<Guid> operator +(Qualifier qualifier, Guid guid) => new QualifiedGuid(qualifier.name, guid);

        public static QualifiedId<int> operator +(Qualifier qualifier, int value) => qualifier.MakeId(value);

        public static QualifiedId<long> operator +(Qualifier qualifier, long value) => qualifier.MakeId(value);

        public static QualifiedId<string> operator +(Qualifier qualifier, string value) => qualifier.MakeId(value);

        public static Qualifier operator &(Qualifier qualifier, Qualifier other) => qualifier.Merge(other);
    }
}
