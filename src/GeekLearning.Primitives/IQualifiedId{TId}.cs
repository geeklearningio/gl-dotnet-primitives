using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Primitives
{
    public interface IQualifiedId<TId>: IQualifiedId
    {
        TId Id { get; }
    }
}
