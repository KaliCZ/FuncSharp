﻿using System;
using Xunit;

namespace FuncSharp.Tests
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void FastEqualsWorks()
        {
            var n1 = null as object;
            var n2 = null as object;
            var o1 = new object();
            var o2 = new object();
            var t1 = new EqualityTester { Id = 1 };
            var t2 = new EqualityTester { Id = 1 };
            var t3 = new EqualityTester { Id = 3 };
            var s = "foo";

            Assert.Equal(true.ToOption(), n1.FastEquals(null));
            Assert.Equal(true.ToOption(), n1.FastEquals(n1));
            Assert.Equal(true.ToOption(), n1.FastEquals(n2));
            Assert.Equal(true.ToOption(), o1.FastEquals(o1));
            Assert.Equal(true.ToOption(), t1.FastEquals(t1));
            Assert.Equal(true.ToOption(), s.FastEquals(s));

            Assert.Equal(false.ToOption(), o1.FastEquals(null));
            Assert.Equal(false.ToOption(), o1.FastEquals(n1));
            Assert.Equal(false.ToOption(), n1.FastEquals(o1));
            Assert.Equal(false.ToOption(), s.FastEquals(o1));
            Assert.Equal(false.ToOption(), t1.FastEquals(s));

            Assert.True(o1.FastEquals(o2).IsNone);
            Assert.True(o1.FastEquals(s).IsSome);
            Assert.True(t1.FastEquals(t2).IsNone);
            Assert.True(t1.FastEquals(t3).IsNone);
        }

        [Fact]
        public void StructurallyEqualsWorks()
        {
            var n1 = null as object;
            var n2 = null as object;
            var o1 = new object();
            var o2 = new object();
            var t1 = new EqualityTester { Id = 1 };
            var t2 = new EqualityTester { Id = 1 };
            var t3 = new EqualityTester { Id = 3 };
            var s = "foo";

            Assert.True(n1.StructurallyEquals(null));
            Assert.True(n1.StructurallyEquals(n1));
            Assert.True(n1.StructurallyEquals(n2));
            Assert.True(o1.StructurallyEquals(o1));
            Assert.True(t1.StructurallyEquals(t1));
            Assert.True(s.StructurallyEquals(s));

            Assert.False(o1.StructurallyEquals(null));
            Assert.False(o1.StructurallyEquals(n1));
            Assert.False(n1.StructurallyEquals(o1));
            Assert.False(s.StructurallyEquals(o1));
            Assert.False(t1.StructurallyEquals(s));

            Assert.False(o1.StructurallyEquals(o2));
            Assert.False(o1.StructurallyEquals(s));
            Assert.True(t1.StructurallyEquals(t2));
            Assert.False(t1.StructurallyEquals(t3));
        }

        [Fact]
        public void AsSumWorks()
        {
            Assert.Equal("foo", "foo".AsSum<string, int>().First.Value);
            Assert.Equal(42, 42.AsSum<string, int>().Second.Value);
            Assert.Equal(42, 42.AsSum<int, int>().First.Value);
            Assert.Throws<ArgumentException>(() => new object().AsSum<string, int>());
        }

        [Fact]
        public void AsSafeSumWorks()
        {
            Assert.Equal("foo", "foo".AsSafeSum<string, int>().First.Value);
            Assert.Equal(42, 42.AsSafeSum<string, int>().Second.Value);
            Assert.Equal(42, 42.AsSafeSum<int, int>().First.Value);
            Assert.Equal("foo", "foo".AsSafeSum<int, int>().Third.Value);
        }

        private class EqualityTester
        {
            public int Id { get; set; }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                var objTester = obj as EqualityTester;
                if (objTester != null)
                {
                    return Id == objTester.Id;
                }
                return false;
            }
        }
    }
}