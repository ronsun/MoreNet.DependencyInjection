using FluentAssertions;
using MoreNet.DependencyInjection;
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
        private IServiceCollection _mockedServiceCollection;

        private interface IFakeInterface : INameable { }

        private class FakeImplementationA : IFakeInterface
        {
            public string Name => "A";
        }

        private class FakeImplementationA2 : IFakeInterface
        {
            public string Name => "A";
        }

        private class FakeImplementationB : IFakeInterface
        {
            public string Name => "B";
        }

        [SetUp]
        public void Init()
        {
            _mockedServiceCollection = Substitute.For<IServiceCollection>();
        }

        #region AddNamedSingleton

        [Test()]
        public void AddNamedSingletonTest_ReturnsCurrentCollection()
        {
            // arrange
            var expectedReturns = _mockedServiceCollection;

            // act
            var actualReturns = ServiceCollectionExtensions.AddNamedSingleton<IFakeInterface>(_mockedServiceCollection);

            // assert
            actualReturns.Should().BeSameAs(actualReturns);
        }

        [Test()]
        public void AddNamedSingletonTest_GotSingletonLifetime()
        {
            // arrange
            var expectedLifeTime = ServiceLifetime.Singleton;

            // act
            ServiceCollectionExtensions.AddNamedSingleton<IFakeInterface>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(1).Add(Arg.Is<ServiceDescriptor>(r => r.Lifetime == expectedLifeTime));
        }

        #endregion

        #region AddNamedScoped

        [Test()]
        public void AddNamedScopedTest_ReturnsCurrentCollection()
        {
            // arrange
            var expectedReturns = _mockedServiceCollection;

            // act
            var actualReturns = ServiceCollectionExtensions.AddNamedScoped<IFakeInterface>(_mockedServiceCollection);

            // assert
            actualReturns.Should().BeSameAs(actualReturns);
        }

        [Test()]
        public void AddNamedScopedTest_GotScopedLifetime()
        {
            // arrange
            var expectedLifeTime = ServiceLifetime.Scoped;

            // act
            ServiceCollectionExtensions.AddNamedScoped<IFakeInterface>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(1).Add(Arg.Is<ServiceDescriptor>(r => r.Lifetime == expectedLifeTime));
        }

        #endregion

        #region AddNamedTransient

        [Test()]
        public void AddNamedTransientTest_ReturnsCurrentCollection()
        {
            // arrange
            var expectedReturns = _mockedServiceCollection;

            // act
            var actualReturns = ServiceCollectionExtensions.AddNamedTransient<IFakeInterface>(_mockedServiceCollection);

            // assert
            actualReturns.Should().BeSameAs(actualReturns);
        }

        [Test()]
        public void AddNamedTransientTest_GotTransientLifetime()
        {
            // arrange
            var expectedLifeTime = ServiceLifetime.Transient;

            // act
            ServiceCollectionExtensions.AddNamedTransient<IFakeInterface>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(1).Add(Arg.Is<ServiceDescriptor>(r => r.Lifetime == expectedLifeTime));
        }

        #endregion

        [Test()]
        public void NamedServiceDictionaryFactoryTest_ReturnsExpectedDictionary()
        {
            // arrange
            var stubA = new FakeImplementationA();
            var stubB = new FakeImplementationB();
            var stubImplementations = new List<IFakeInterface> { stubA, stubB };

            var mockedServiceProvider = Substitute.For<IServiceProvider>();
            mockedServiceProvider.GetService(Arg.Is(typeof(IEnumerable<IFakeInterface>))).Returns(stubImplementations);

            var expected = new ReadOnlyDictionary<string, IFakeInterface>(new Dictionary<string, IFakeInterface>
            {
                [stubA.Name] = stubA,
                [stubB.Name] = stubB,
            });

            // act
            IReadOnlyDictionary<string, IFakeInterface> actual = ServiceCollectionExtensions.NamedServiceDictionaryFactory<IFakeInterface>(mockedServiceProvider) as IReadOnlyDictionary<string, IFakeInterface>;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}

namespace Microsoft.Extensions.DependencyInjection.Extensions.Tests
{
    [TestFixture()]
    public class ServiceCollectionExtensionsTests
    {
        private IServiceCollection _mockedServiceCollection;

        private interface IFakeInterface : INameable { }

        private class FakeImplementationA : IFakeInterface
        {
            public string Name => "A";
        }

        private class FakeImplementationB : IFakeInterface
        {
            public string Name => "B";
        }

        [SetUp]
        public void Init()
        {
            _mockedServiceCollection = Substitute.For<IServiceCollection>();
        }

        #region TryAddNamedSingleton

        [Test()]
        public void TryAddNamedSingletonTest_ReturnsCurrentCollection()
        {
            // arrange
            var expectedReturns = _mockedServiceCollection;

            // act
            var actualReturns = ServiceCollectionExtensions.TryAddNamedSingleton<IFakeInterface>(_mockedServiceCollection);

            // assert
            actualReturns.Should().BeSameAs(actualReturns);
        }

        [Test()]
        public void TryAddNamedSingletonTest_GotSingletonLifetime()
        {
            // arrange
            var expectedLifeTime = ServiceLifetime.Singleton;

            // act
            ServiceCollectionExtensions.TryAddNamedSingleton<IFakeInterface>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(1).Add(Arg.Is<ServiceDescriptor>(r => r.Lifetime == expectedLifeTime));
        }
        #endregion

        #region TryAddNamedScoped

        [Test()]
        public void TryAddNamedScopedTest_ReturnsCurrentCollection()
        {
            // arrange
            var expectedReturns = _mockedServiceCollection;

            // act
            var actualReturns = ServiceCollectionExtensions.TryAddNamedScoped<IFakeInterface>(_mockedServiceCollection);

            // assert
            actualReturns.Should().BeSameAs(actualReturns);
        }

        [Test()]
        public void TryAddNamedScopedTest_GotScopedLifetime()
        {
            // arrange
            var expectedLifeTime = ServiceLifetime.Scoped;

            // act
            ServiceCollectionExtensions.TryAddNamedScoped<IFakeInterface>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(1).Add(Arg.Is<ServiceDescriptor>(r => r.Lifetime == expectedLifeTime));
        }

        #endregion

        #region TryAddNamedTransient

        [Test()]
        public void TryAddNamedTransientTest_ReturnsCurrentCollection()
        {
            // arrange
            var expectedReturns = _mockedServiceCollection;

            // act
            var actualReturns = ServiceCollectionExtensions.TryAddNamedTransient<IFakeInterface>(_mockedServiceCollection);

            // assert
            actualReturns.Should().BeSameAs(actualReturns);
        }

        [Test()]
        public void TryAddNamedTransientTest_GotTransientLifetime()
        {
            // arrange
            var expectedLifeTime = ServiceLifetime.Transient;

            // act
            ServiceCollectionExtensions.TryAddNamedTransient<IFakeInterface>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(1).Add(Arg.Is<ServiceDescriptor>(r => r.Lifetime == expectedLifeTime));
        }

        #endregion

        #region TryAddSingletonEnumerable

        [Test()]
        public void TryAddSingletonEnumerableTest_ReturnsCurrentCollection()
        {
            // arrange
            var expectedReturns = _mockedServiceCollection;

            // act
            var actualReturns = ServiceCollectionExtensions.TryAddSingletonEnumerable<IFakeInterface, FakeImplementationA>(_mockedServiceCollection);

            // assert
            actualReturns.Should().BeSameAs(actualReturns);
        }

        [Test()]
        public void TryAddSingletonEnumerableTest_GotSingletonLifetime()
        {
            // arrange
            var expectedLifeTime = ServiceLifetime.Singleton;

            // act
            ServiceCollectionExtensions.TryAddSingletonEnumerable<IFakeInterface, FakeImplementationA>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(1).Add(Arg.Is<ServiceDescriptor>(r => r.Lifetime == expectedLifeTime));
        }

        [Test()]
        public void TryAddSingletonEnumerableTest_Add2_Receive2()
        {
            // arrange

            // act
            ServiceCollectionExtensions.TryAddSingletonEnumerable<IFakeInterface, FakeImplementationA>(_mockedServiceCollection);
            ServiceCollectionExtensions.TryAddSingletonEnumerable<IFakeInterface, FakeImplementationB>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(2).Add(Arg.Any<ServiceDescriptor>());
        }
        #endregion

        #region TryAddScopedEnumerable

        [Test()]
        public void TryAddScopedEnumerableTest_ReturnsCurrentCollection()
        {
            // arrange
            var expectedReturns = _mockedServiceCollection;

            // act
            var actualReturns = ServiceCollectionExtensions.TryAddScopedEnumerable<IFakeInterface, FakeImplementationA>(_mockedServiceCollection);

            // assert
            actualReturns.Should().BeSameAs(actualReturns);
        }

        [Test()]
        public void TryAddScopedEnumerableTest_GotScopedLifetime()
        {
            // arrange
            var expectedLifeTime = ServiceLifetime.Scoped;

            // act
            ServiceCollectionExtensions.TryAddScopedEnumerable<IFakeInterface, FakeImplementationA>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(1).Add(Arg.Is<ServiceDescriptor>(r => r.Lifetime == expectedLifeTime));
        }

        [Test()]
        public void TryAddScopedEnumerableTest_Add2_Receive2()
        {
            // arrange

            // act
            ServiceCollectionExtensions.TryAddScopedEnumerable<IFakeInterface, FakeImplementationA>(_mockedServiceCollection);
            ServiceCollectionExtensions.TryAddScopedEnumerable<IFakeInterface, FakeImplementationB>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(2).Add(Arg.Any<ServiceDescriptor>());
        }

        #endregion

        #region TryAddTransientEnumerable

        [Test()]
        public void TryAddTransientEnumerableTest_ReturnsCurrentCollection()
        {
            // arrange
            var expectedReturns = _mockedServiceCollection;

            // act
            var actualReturns = ServiceCollectionExtensions.TryAddTransientEnumerable<IFakeInterface, FakeImplementationA>(_mockedServiceCollection);

            // assert
            actualReturns.Should().BeSameAs(actualReturns);
        }

        [Test()]
        public void TryAddTransientEnumerableTest_GotTransientLifetime()
        {
            // arrange
            var expectedLifeTime = ServiceLifetime.Transient;

            // act
            ServiceCollectionExtensions.TryAddTransientEnumerable<IFakeInterface, FakeImplementationA>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(1).Add(Arg.Is<ServiceDescriptor>(r => r.Lifetime == expectedLifeTime));
        }

        [Test()]
        public void TryAddTransientEnumerableTest_Add2_Receive2()
        {
            // arrange

            // act
            ServiceCollectionExtensions.TryAddTransientEnumerable<IFakeInterface, FakeImplementationA>(_mockedServiceCollection);
            ServiceCollectionExtensions.TryAddTransientEnumerable<IFakeInterface, FakeImplementationB>(_mockedServiceCollection);

            // assert
            _mockedServiceCollection.Received(2).Add(Arg.Any<ServiceDescriptor>());
        }

        #endregion
    }
}