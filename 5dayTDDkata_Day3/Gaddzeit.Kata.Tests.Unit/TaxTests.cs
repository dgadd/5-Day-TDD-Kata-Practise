using Gaddzeit.Kata.Domain;
using NUnit.Framework;
using System;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class TaxTests
    {
        [Test]
        [ExpectedException(typeof(NullOrZeroConstructorParameterException))]
        public void TaxCannotBeCreatedWithAllNullProperties()
        {
            var tax = new Tax(null, null, null, JurisdictionEnum.Unspecified, 0);
        }

        [Test]
        [ExpectedException(typeof(NullOrZeroConstructorParameterException))]
        public void TaxCannotBeCreatedWithNullTaxType()
        {
            var tax = new Tax(null, DateTime.Today.AddDays(1), DateTime.Today.AddYears(1), JurisdictionEnum.City, 5);
        }

        [Test]
        [ExpectedException(typeof(NullOrZeroConstructorParameterException))]
        public void TaxCannotBeCreatedWithNullStartDate()
        {
            var tax = new Tax("PST", null, DateTime.Today.AddYears(1), JurisdictionEnum.City, 5);
        }

        [Test]
        [ExpectedException(typeof(NullOrZeroConstructorParameterException))]
        public void TaxCannotBeCreatedWithNullEndDate()
        {
            var tax = new Tax("PST", DateTime.Today.AddDays(1), null, JurisdictionEnum.City, 5);
        }

        [Test]
        [ExpectedException(typeof(NullOrZeroConstructorParameterException))]
        public void TaxCannotBeCreatedWithAnUnspecifiedJurisdiction()
        {
            var tax = new Tax("PST", DateTime.Today.AddDays(1), null, JurisdictionEnum.Unspecified, 5);
        }

        [Test]
        [ExpectedException(typeof(NullOrZeroConstructorParameterException))]
        public void TaxCannotBeCreatedWith0Percent()
        {
            var tax = new Tax("PST", DateTime.Today.AddDays(1), null, JurisdictionEnum.Unspecified, 0);
        }

        [Test]
        public void TaxCanBeCreatedWhenAllPropertiesSupplied()
        {
            var tax = new Tax("PST", DateTime.Today.AddDays(1), DateTime.Today.AddYears(1), JurisdictionEnum.City, 5);
            Assert.IsNotNull(tax);
        }

        [Test]
        [ExpectedException(typeof(InvalidTaxDateRangeException))]
        public void TaxStartDateCannotBeGreaterThanEndDate()
        {
            var tax = new Tax("PST", DateTime.Today.AddYears(1).AddDays(1), DateTime.Today.AddYears(1), JurisdictionEnum.City, 5);
            Assert.IsNotNull(tax);
        }

        [Test]
        public void TaxesAreEqualWhenConstructorParametersMatch()
        {
            var tax1 = new Tax("PST", DateTime.Today.AddDays(1), DateTime.Today.AddYears(1), JurisdictionEnum.City, 5);
            var tax2 = new Tax("PST", DateTime.Today.AddDays(1), DateTime.Today.AddYears(1), JurisdictionEnum.City, 5);

            Assert.IsTrue(tax1.Equals(tax2));
            // Assert.AreSame(tax1, tax2) failed, may be referencing another nunit namespace??
        }
    }
}
