using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GeekLearning.Primitives.Test
{
    [Trait("Category", "Qualifier"), Trait("Kind", "Unit")]
    public class QualifiedIdTests
    {

        [Fact]
        public void QualifieIdEquality()
        {

            Qualifier qualifier = "qualifier";

            var id = Guid.NewGuid();

            var id1 = qualifier + id;

            var id2 = new QualifiedGuid(qualifier, id);

            Assert.True(id1.Equals(id2));
        }

        [Fact]
        public void QualifieIdEqualityOperator()
        {

            Qualifier qualifier = "qualifier";

            var id = Guid.NewGuid();

            var id1 = qualifier + id;

            var id2 = new QualifiedGuid(qualifier, id);

            Assert.True(id1 == id2);
        }

        [Fact]
        public void QualifieIdInEqualityOperator()
        {

            Qualifier qualifier = "qualifier";

            var id = Guid.NewGuid();

            var id1 = qualifier + id;

            var id2 = new QualifiedGuid(qualifier, id);

            Assert.False(id1 != id2);
        }

        [Fact]
        public void MixedQualifieIdInEqualityOperator()
        {

            Qualifier qualifier = "qualifier";

            var id = Guid.NewGuid();

            var id1 = qualifier + id;

            IQualifiedId id2 = new QualifiedGuid(qualifier, id);

            Assert.False(id1 != id2);
        }

        [Fact]
        public void IQualifieIdEquality()
        {

            Qualifier qualifier = "qualifier";

            var id = Guid.NewGuid();

            IQualifiedId id1 = qualifier + id;

            IQualifiedId id2 = new QualifiedString(id1.Full);

            Assert.True(id1.Equals(id2));
        }

        [Fact]
        public void QualifiedIdObjectEquality()
        {

            Qualifier qualifier = "qualifier";

            var id = Guid.NewGuid();

            var id1 = qualifier + id;

            object id2 = qualifier + id;

            Assert.True(id1.Equals(id2));
        }

        [Fact]
        public void QualifiedIdObject2Equality()
        {


            Qualifier qualifier = "qualifier";

            var id = Guid.NewGuid();

            object id1 = qualifier + id;

            object id2 = qualifier + id;

            Assert.True(object.Equals(id1, id2));

        }
    }
}
