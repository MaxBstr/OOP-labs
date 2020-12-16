using System.Collections.Generic;
using Banks;

namespace Banks
{

    public interface IBuilderBank
    {
        IBuilderBank SetDebitPercent(double percent);
        IBuilderBank SetDepositPercents(List<double> percents);
        IBuilderBank SetCreditFeatures(double limit, double commission);
        IBuilderBank SetDoubtSum(int doubt);
        Bank Create();
        void Reset();
    }
    
    public class BankBuilder : IBuilderBank
    {
        private double _debitPercent;
        private List<double> _depositPercents;
        private double _creditLimit;
        private double _creditCommission;
        private int _doubtSum;


        public IBuilderBank SetDebitPercent(double percent)
        {
            _debitPercent = percent;
            return this;
        }

        public IBuilderBank SetDepositPercents(List<double> percents)
        {
            _depositPercents = percents;
            return this;
        }

        public IBuilderBank SetCreditFeatures(double limit, double commission)
        {
            _creditLimit = limit;
            _creditCommission = commission;
            return this;
        }

        public IBuilderBank SetDoubtSum(int doubt)
        {
            _doubtSum = doubt;
            return this;
        }

        public Bank Create()
        {
            return new Bank(_debitPercent, _depositPercents, _creditLimit, _creditCommission, _doubtSum);
        }

        public void Reset()
        {
            _debitPercent = 0;
            _depositPercents = null;
            _creditLimit = 0;
            _creditCommission = 0;
            _doubtSum = 0;
        }
    }
}

class BankDirector
{
    public void BuildSberBank(IBuilderBank builder)
    {
        builder.Reset();
        builder.SetDebitPercent(10).SetDepositPercents(new List<double> {1.5, 3, 4}).SetCreditFeatures(50000, 2)
            .SetDoubtSum(30000);
    }

    public void BuildAlphaBank(IBuilderBank builder)
    {
        builder.Reset();
        builder.SetDebitPercent(15).SetDepositPercents(new List<double> {2, 3.5, 5}).SetCreditFeatures(70000, 4)
            .SetDoubtSum(90000);
    }
}