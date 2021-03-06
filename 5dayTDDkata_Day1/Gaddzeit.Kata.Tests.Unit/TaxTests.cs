﻿using Gaddzeit.Kata.Domain;
using NUnit.Framework;
using System;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class TaxTests
    {
        [Test]
        [ExpectedException(typeof(TaxValuesMissingException))]
        public void TaxCannotBeCreatedWithAllNullProperties()
        {
            var tax = new Tax(null, null, null);
        }

        [Test]
        [ExpectedException(typeof(TaxValuesMissingException))]
        public void TaxCannotBeCreatedWithNullTaxType()
        {
            var tax = new Tax(null, DateTime.Today.AddDays(1), DateTime.Today.AddYears(1));
        }

        [Test]
        [ExpectedException(typeof(TaxValuesMissingException))]
        public void TaxCannotBeCreatedWithNullStartDate()
        {
            var tax = new Tax("PST", null, DateTime.Today.AddYears(1));
        }

        [Test]
        [ExpectedException(typeof(TaxValuesMissingException))]
        public void TaxCannotBeCreatedWithNullEndDate()
        {
            var tax = new Tax("PST", DateTime.Today.AddDays(1), null);
        }

        [Test]
        public void TaxCanBeCreatedWhenAllPropertiesSupplied()
        {
            var tax = new Tax("PST", DateTime.Today.AddDays(1), DateTime.Today.AddYears(1));
            Assert.IsNotNull(tax);
        }

        [Test]
        [ExpectedException(typeof(InvalidTaxDateRangeException))]
        public void TaxStartDateCannotBeGreaterThanEndDate()
        {
            var tax = new Tax("PST", DateTime.Today.AddYears(1).AddDays(1), DateTime.Today.AddYears(1));
            Assert.IsNotNull(tax);
        }

        [Test]
        public void TaxesAreEqualWhenConstructorParametersMatch()
        {
            var tax1 = new Tax("PST", DateTime.Today.AddDays(1), DateTime.Today.AddYears(1));
            var tax2 = new Tax("PST", DateTime.Today.AddDays(1), DateTime.Today.AddYears(1));

            Assert.IsTrue(tax1.Equals(tax2));
            // Assert.AreSame(tax1, tax2) failed, may be referencing another nunit namespace??
        }
    }
}
