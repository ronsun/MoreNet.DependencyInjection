using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace MoreNet.DependencyInjection.Tests
{
    [TestFixture()]
    public class NamedServiceDictionaryTests
    {
        private interface IFakeInterface : INameable { }

        private class FakeImplementationA : IFakeInterface
        {
            public string Name => "A";
        }

        [Test()]
        public void GetServiceTest_Found()
        {
            // arrange
            var stubImplementation = new FakeImplementationA();
            var stubDictionary = new Dictionary<string, IFakeInterface>()
            {
                ["A"] = stubImplementation,
            };

            var target = new NamedServiceDictionary<IFakeInterface>(stubDictionary);
            var expected = stubImplementation;

            // act
            var actual = target.GetService("A");

            // assert
            actual.Should().BeSameAs(expected);
        }


        [Test()]
        public void GetServiceTest_NotFound()
        {
            // arrange
            var stubDictionary = new Dictionary<string, IFakeInterface>()
            {
                ["A"] = new FakeImplementationA(),
            };

            var target = new NamedServiceDictionary<IFakeInterface>(stubDictionary);

            // act
            var actual = target.GetService("NotFound");

            // assert
            actual.Should().BeNull();
        }
    }
}