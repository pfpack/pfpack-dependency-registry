﻿#nullable enable

using NUnit.Framework;
using System;

namespace PrimeFuncPack.Core.Objects.Tests
{
    partial class StringPredicateExtensionsTests
    {
        [Test]
        public void IsNullOrEmpty_SourceIsNull_ExpectTrue()
        {
            string? source = null;

            var actual = source.IsNullOrEmpty();
            Assert.True(actual);
        }

        [Test]
        public void IsNullOrEmpty_SourceIsEmpty_ExpectTrue()
        {
            string source = string.Empty;

            var actual = source.IsNullOrEmpty();
            Assert.True(actual);
        }

        [Test]
        [TestCase(" ")]
        [TestCase("some")]
        public void IsNullOrEmpty_SourceIsNotEmpty_ExpectFalse(
            in string source)
        {
            var actual = source.IsNullOrEmpty();
            Assert.False(actual);
        }
    }
}
