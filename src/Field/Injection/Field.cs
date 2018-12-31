﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Field.Injection
{
    public abstract partial class SpecificationTests
    {
        [TestMethod]
        public void BaseLine()
        {
            // Act
            var result = Container.Resolve<ObjectWithThreeFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Field);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void None()
        {
            // Act
            Container.RegisterType<ObjectWithThreeFields>(
                Inject.Property("Bogus Name"));
        }

        [TestMethod]
        public void ByName()
        {
            // Setup
            Container.RegisterType<ObjectWithThreeFields>(
                Inject.Field(nameof(ObjectWithThreeFields.Field)));

            // Act
            var result = Container.Resolve<ObjectWithThreeFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Field);
            Assert.IsInstanceOfType(result.Field, typeof(object));
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void ByNameValue()
        {
            // Setup
            var test = "test";
            Container.RegisterType<ObjectWithThreeFields>(
                Inject.Field(nameof(ObjectWithThreeFields.Field), test));

            // Act
            var result = Container.Resolve<ObjectWithThreeFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Field);
            Assert.AreSame(result.Field, test);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void ByNameInDerived()
        {
            // Setup
            Container.RegisterType<ObjectWithFourFields>(
                Inject.Field(nameof(ObjectWithFourFields.Field)));

            // Act
            var result = Container.Resolve<ObjectWithFourFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Field);
            Assert.IsInstanceOfType(result.Field, typeof(object));
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void ByNameValueInDerived()
        {
            // Setup
            var test = "test";
            Container.RegisterType<ObjectWithFourFields>(
                Inject.Field(nameof(ObjectWithFourFields.Field), test));

            // Act
            var result = Container.Resolve<ObjectWithFourFields>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Field);
            Assert.AreSame(result.Field, test);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadOnlyProperty()
        {
            // Act
            Container.RegisterType<ObjectWithFourFields>(
                Inject.Field(nameof(ObjectWithFourFields.ReadOnlyField), "test"));
        }

        [TestMethod]
        public void NoneAsDependency()
        {
            // Act
            var result = Container.Resolve<ObjectWithDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.IsNull(result.Dependency.Field);
            Assert.AreEqual(result.Dependency.Name, Name);
            Assert.IsNotNull(result.Dependency.Container);
        }
    }
}
