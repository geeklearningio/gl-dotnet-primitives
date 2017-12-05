using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Primitives
{
    public class QualifiedString : QualifiedId<String>
    {
        public QualifiedString(string qualifier, String id): base(qualifier, id)
        {

        }

        public QualifiedString(string qualifiedId) : base(qualifiedId)
        {

        }

        public QualifiedString(string qualifiedId, bool isBase64Encoded) : base(qualifiedId, isBase64Encoded)
        {

        }
    }
}
