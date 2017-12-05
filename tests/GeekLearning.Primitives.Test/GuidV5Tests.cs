using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GeekLearning.Primitives.Test
{
    public class GuidV5Tests
    {
        [Theory]
        [InlineData("78f6eacf-a0f9-5346-81a7-bb4728491510", "Title", "dbe7b03b-c3c4-511f-9435-d014db6ef895")]
        [InlineData("8dc079dd-0313-4563-864f-008eb45bf87f", "hello", "c506b68b-ed29-5662-bb90-7f43e624e333")]
        [InlineData("8dc079dd-0313-4563-864f-008eb45bf87f", "world", "669a6357-2584-534e-84bb-ac69f1c8ef44")]
        [InlineData(GuidV5.DNSString, "hello.example.com", "fdda765f-fc57-5604-a269-52a7df8164ec")]
        [InlineData(GuidV5.URLString, "http://example.com/hello", "3bbcee75-cecc-5b56-8031-b6641c1ed1f1")]
        [InlineData("1b671a64-40d5-491e-99b0-da01ff1f3341", "Hello, World!", "630eb68f-e0fa-5ecc-887a-7c7a62614681")]
        public void SampleIds(string @namespace, string source, string expected)
        {
            Assert.Equal(GuidV5.CreateGuid(source, Guid.Parse(@namespace)), Guid.Parse(expected));
        }

    }
}
