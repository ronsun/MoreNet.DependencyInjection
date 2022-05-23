using FluentAssertions;
using MoreNet.DependencyInjection.Extensions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Extensions.DependencyInjection.Tests
{
    [TestFixture()]
    public partial class ServiceCollectionExtensionsTests
    {
        [Test()]
        public void NamedServiceDictionaryFactoryTest_ThrowExpectedException()
        {
            // arrange
            var stubA = new FakeImplementationA();
            var stubA2 = new FakeImplementationA2();
            var stubImplementations = new List<IFakeInterface> { stubA, stubA2 };

            var mockedServiceProvider = Substitute.For<IServiceProvider>();
            mockedServiceProvider.GetService(Arg.Is(typeof(IEnumerable<IFakeInterface>))).Returns(stubImplementations);

            var expected = new ReadOnlyDictionary<string, IFakeInterface>(new Dictionary<string, IFakeInterface>
            {
                [stubA.Name] = stubA,
                [stubA2.Name] = stubA2,
            });

            // act
            Action action = () => ServiceCollectionExtensions.NamedServiceDictionaryFactory<IFakeInterface>(mockedServiceProvider);

            // assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}