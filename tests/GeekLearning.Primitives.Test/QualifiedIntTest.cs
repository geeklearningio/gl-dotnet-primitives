namespace GeekLearning.Primitives.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "QualifiedId"), Trait("Kind", "Unit")]
    public class QualifiedIntTest
    {
        [Fact]
        public void QualifiedIntParsing()
        {
            int id = 1;
            var qualifiedId = "provider:1";

            var qualifiedGuid = new QualifiedId<int>(qualifiedId);

            Assert.Equal(id, qualifiedGuid.Id);
            Assert.Equal("provider", qualifiedGuid.Qualifier);
        }

        [Fact]
        public void ShouldParseInt()
        {
            int id = 1;
            var qualifiedId = "provider:1";

            var parsedId = new QualifiedId<int>(qualifiedId);
            Assert.Equal(id, parsedId.Id);
        }

        [Fact]
        public void QualifierAddInt()
        {
            Qualifier qualifier = "provider";
            int id = 1;
            var qualifiedId = qualifier + id;
            var qualifiedIdString = "provider:1";

            Assert.Equal(qualifiedId.Full, qualifiedIdString);
        }

        [Fact]
        public void ImplicitStringConversion()
        {
            Qualifier qualifier = "provider";
            var id = 1;
            var qualifiedId = qualifier + id;
            var qualifiedIdString = "provider:1";

            Func<string, string> identity = x => x;

            var result = identity(qualifiedId);

            Assert.Equal(qualifiedIdString, result);
        }

        [Fact]
        public void GuidRoundtripConversion()
        {
            Qualifier qualifier = "provider";
            int id = 1;
            var qualifiedId = qualifier + id;

            Func<string, int> reparse = x => new QualifiedId<int>(x).Id;

            var result = reparse(qualifiedId);

            Assert.Equal(id, result);
        }
    }
}
