using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GeekLearning.Primitives.Test
{
    [Trait("Category", "QualifiedId"), Trait("Kind", "Unit")]
    public class QualifiedTuplesTest
    {
        [Fact]
        public void QualifiedTupleRoundtrip()
        {
            var guid = Guid.NewGuid();
            var number = 5;
            var tuple = Tuple.Create(guid, number);

            Qualifier qualifier = "a_qualifier";

            var qualifiedId = qualifier.MakeId(tuple);


            var fullId = qualifiedId.Full;

            var parseId = new QualifiedId<Tuple<Guid, int>>(fullId);


            Assert.Equal(guid, parseId.Id.Item1);
            Assert.Equal(number, parseId.Id.Item2);
        }

        [Fact]
        public void QualifiedValueTupleRoundtrip()
        {
            var guid = Guid.NewGuid();
            var number = 5;
            var tuple = (id: guid, kind: number);

            Qualifier qualifier = "a_qualifier";

            var qualifiedId = qualifier.MakeId(tuple);

            var fullId = qualifiedId.Full;

            var parseId = new QualifiedId<(Guid Id, int Kind)>(fullId);


            Assert.Equal(guid, parseId.Id.Id);
            Assert.Equal(number, parseId.Id.Kind);
        }

        [Fact]
        public void QualifiedValueTupleSerialization()
        {
            var guid = Guid.NewGuid();
            var number = 5;
            var tuple = (id: guid, kind: number);

            var expected = $"a_qualifier:({guid}, {number})";

            Qualifier qualifier = "a_qualifier";

            var qualifiedId = qualifier.MakeId(tuple);

            Assert.Equal(expected, qualifiedId.Full);
        }

        [Fact]
        public void QualifiedTupleSerialization()
        {
            var guid = Guid.NewGuid();
            var number = 5;
            var tuple = (id: guid, kind: number);

            var expected = $"a_qualifier:({guid}, {number})";

            Qualifier qualifier = "a_qualifier";

            var qualifiedId = qualifier.MakeId(tuple);

            Assert.Equal(expected, qualifiedId.Full);
        }
    }
}
