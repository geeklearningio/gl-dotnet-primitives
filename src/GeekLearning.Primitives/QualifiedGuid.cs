using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Primitives
{
    public class QualifiedGuid : QualifiedId<Guid>
    {
        private string format = null;

        public QualifiedGuid(Qualifier qualifier, Guid id) : base(qualifier, id)
        {

        }

        public QualifiedGuid(string qualifiedId) : base(qualifiedId)
        {

        }

        public QualifiedGuid(Qualifier qualifier, Guid id, string format) : base(qualifier, id)
        {
            this.format = format;
        }

        public QualifiedGuid(string qualifiedId, string format) : base(qualifiedId)
        {
            this.format = format;
        }

        protected override Guid ParseIdFromString(string str)
        {
            if (!string.IsNullOrEmpty(format))
            {
                return Guid.ParseExact(str, this.format);
            }
            else
            {
                return Guid.Parse(str);
            }
        }

        protected override string ToUnqualifiedString()
        {
            return this.Id.ToString(this.format ?? "D");
        }
    }
}
