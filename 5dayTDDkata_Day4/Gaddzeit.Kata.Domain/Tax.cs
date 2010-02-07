using System;

namespace Gaddzeit.Kata.Domain
{
    public class Tax
    {
        private readonly string _taxType;
        private readonly DateTime? _startDate;
        private readonly DateTime? _endDate;
        private readonly JurisdictionEnum _jurisdiction;
        private readonly int _percent;

        private Tax()
        {
        }

        public Tax(string taxType, DateTime? startDate, DateTime? endDate, JurisdictionEnum jurisdiction, int percent)
        {
            _taxType = taxType;
            _startDate = startDate;
            _endDate = endDate;
            _jurisdiction = jurisdiction;
            _percent = percent;

            ValidateAllParametersHaveNonNullValue();

            ValidateEndDateGreaterThanStartDate();
        }

        public string TaxType
        {
            get { return _taxType; }
        }

        public DateTime? StartDate
        {
            get { return _startDate; }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
        }

        public JurisdictionEnum Jurisdiction
        {
            get { return _jurisdiction; }
        }

        public int Percent
        {
            get { return _percent; }
        }

        public override bool Equals(object obj)
        {
            var otherTax = (Tax) obj;

            var isEqual = otherTax.TaxType.Equals(this.TaxType)
                   && otherTax.StartDate.Value.Equals(this.StartDate.Value)
                   && otherTax.EndDate.Value.Equals(this.EndDate.Value)
                   && otherTax.Jurisdiction.Equals(this.Jurisdiction)
                   && otherTax.Percent.Equals(this.Percent);

            return isEqual;
        }

        public override int GetHashCode()
        {
            return StartDate.GetHashCode() 
                + EndDate.GetHashCode() 
                + TaxType.GetHashCode() 
                + Jurisdiction.GetHashCode()
                + Percent.GetHashCode();
        }

        private void ValidateEndDateGreaterThanStartDate()
        {
            if (StartDate.Value > EndDate.Value)
                throw new InvalidTaxDateRangeException();
        }

        private void ValidateAllParametersHaveNonNullValue()
        {
            if (TaxType == null
                || !StartDate.HasValue
                || !EndDate.HasValue
                || Jurisdiction == JurisdictionEnum.Unspecified
                || Percent.Equals(0))
                throw new NullOrZeroConstructorParameterException();
        }
    }
}