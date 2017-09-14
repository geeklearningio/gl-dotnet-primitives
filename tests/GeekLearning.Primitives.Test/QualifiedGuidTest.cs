namespace GeekLearning.Primitives.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "QualifiedId"), Trait("Kind", "Unit")]
    public class QualifiedGuidTest
    {
        [Fact]
        public void QualifiedGuidParsing()
        {
            var guid = Guid.Parse("D2180A07-F8DA-48E7-BB0E-855176DA58A0");
            var qualifiedId = "provider:d2180a07-f8da-48e7-bb0e-855176da58a0";

            var qualifiedGuid = new QualifiedGuid(qualifiedId);

            Assert.Equal(guid, qualifiedGuid.Id);
            Assert.Equal("provider", qualifiedGuid.Qualifier);
        }

        [Fact]
        public void QualifiedGuidFormatting()
        {
            var guid = Guid.Parse("D2180A07-F8DA-48E7-BB0E-855176DA58A0");
            var qualifiedId = "provider:d2180a07f8da48e7bb0e855176da58a0";

            var qualifiedGuid = new QualifiedGuid("provider", guid, "N");

            Assert.Equal(qualifiedId, qualifiedGuid.Full);
        }

        [Fact]
        public void ShouldThrowOnUnQualifiedGuidParsing()
        {
            var guid = Guid.Parse("D2180A07-F8DA-48E7-BB0E-855176DA58A0");
            var qualifiedId = "D2180A07-F8DA-48E7-BB0E-855176DA58A0";

            Assert.Throws<ArgumentException>(()=>{
                var qualifiedGuid = new QualifiedGuid("qualifiedId", qualifiedId);
            });
        }

        [Fact]
        public void ShouldParseGuid()
        {
            var guid = Guid.Parse("D2180A07-F8DA-48E7-BB0E-855176DA58A0");
            var qualifiedId = "provider:d2180a07f8da48e7bb0e855176da58a0";

            var parsedId = new QualifiedId<Guid>(qualifiedId);
            Assert.Equal(guid, parsedId.Id);
        }
    }
}
