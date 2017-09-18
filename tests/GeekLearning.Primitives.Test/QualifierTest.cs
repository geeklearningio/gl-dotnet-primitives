using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GeekLearning.Primitives.Test
{
    [Trait("Category", "Qualifier"), Trait("Kind", "Unit")]
    public class QualifierTest
    {
        [Fact]
        public void QualifierEquality()
        {

            Qualifier a = "aname";
            Qualifier b = "a" + "name";


            Assert.True(a.Equals(b));
        }

        [Fact]
        public void QualifierEqualityOperator()
        {

            Qualifier a = "aname";
            Qualifier b = "a" + "name";


            Assert.True(a == b);
        }


        [Fact]
        public void QualifierInEqualityOperator()
        {

            Qualifier a = "aname";
            Qualifier b = "a" + "name";


            Assert.False(a != b);
        }

        [Fact]
        public void QualifierObjectEquality()
        {

            Qualifier a = "aname";
            object b = new Qualifier("a" + "name");


            Assert.True(a.Equals(b));
        }

        [Fact]
        public void QualifierObject2Equality()
        {

            object a = new Qualifier("aname");
            object b = new Qualifier("a" + "name");


            Assert.True(Object.Equals(a, b));
        }

        [Fact]
        public void QualifierStringEquality()
        {

            Qualifier a = new Qualifier("aname");
            string b = "aname";


            Assert.True(a == b);
        }

        [Fact]
        public void QualifierString2Equality()
        {

            object a = new Qualifier("aname");
            object b = "aname";


            Assert.True(object.Equals(a, b));
        }

        [Fact]
        public void QualifierConcat()
        {

            Qualifier a = "a";
            Qualifier name = "name";

            var composite = a & name;

            Assert.Equal(new Qualifier("aname"), composite);
        }
    }
}
