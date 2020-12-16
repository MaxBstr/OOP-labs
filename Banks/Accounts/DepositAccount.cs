using System;
using System.Collections.Generic;

namespace Banks.Accounts
{
    public class DepositAccount : IAccount
    {
        private readonly Client _client;
        private List<double> _percents = new List<double>();
        private double _money;
        private readonly int _deadlineDay;
        private int _id;
        private readonly double _doubtSum;
        private int _accountDate = 1;
        private double _cashback;

        public DepositAccount(int id, Client client, double money, int deadlineDays, double doubtSum)
        {
            _id = id;
            _client = client;
            _money = money;
            _deadlineDay = deadlineDays;
            _doubtSum = doubtSum;
        }

        public void SetPercents(List<double> percents)
        {
            _percents = percents;
        }
        
        public void CalculatePercents(int days)
        {
            double percent;
            
            if (_money <= 50000)
                percent = _percents[0] / 100.0;
            else if (_money <= 100000)
                percent = _percents[1] / 100.0;
            else
                percent = _percents[2] / 100.0;
            
            for (var i = 1; i <= days; ++i)
            {
                _cashback += _money * percent / 365.0;
                if (_accountDate % 30 == 0)
                { 
                    _money += _cashback; 
                    _cashback = 0;
                }
                _accountDate++;
            }
            
            Console.WriteLine(_money);
            Console.WriteLine(_cashback);
        }

        public void WithdrawMoney(double money)
        {
            if (money > _doubtSum && !_client.IsLegal || _accountDate < _deadlineDay || money > _money)
            {
                throw new Exception("You can`t withdraw money. Deadline did not come " +
                                    "or you did not identify your account or low balance");
            }
            
            _money -= money;
            Console.WriteLine(_money);
        }

        public void AddMoney(double money)
        {
            _money += money;
            Console.WriteLine(_money);
        }
        
        public int GetId() { return _id; }
    }
}