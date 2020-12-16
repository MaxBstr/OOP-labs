using System;

namespace Banks.Accounts
{
    public class CreditAccount : IAccount
    {
        private readonly Client _client;
        private double _creditLimit;
        private double _creditCommission;
        private double _money;
        private int _id;
        private readonly double _doubtSum;
        private int _accountDate = 1;
        private double _cashback;

        public CreditAccount(int id, Client client, double money, double doubtSum)
        {
            _client = client;
            _id = id;
            _doubtSum = doubtSum;
            _money = money;
        }

        public void SetPercents(double creditLimit, double creditCommission)
        {
            _creditLimit = creditLimit;
            _creditCommission = creditCommission / 100.0;
        }
        
        public void CalculatePercents(int days)
        {
            if (_money >= _creditLimit)
            {
                Console.WriteLine(_money);
                Console.WriteLine(_cashback);
                return;
            }
                

            var remains = _creditLimit - _money;
            
            for (var i = 1; i <= days; ++i)
            {
                _cashback += remains * _creditCommission / 365.0;
                if (_accountDate % 30 == 0)
                { 
                    _money -= _cashback; 
                    _cashback = 0;
                }
                _accountDate++;
            }
            Console.WriteLine(_money);
            Console.WriteLine(_cashback);
        }

        public void WithdrawMoney(double money)
        {
            if (money > _doubtSum && !_client.IsLegal)
            {
                throw new Exception("You can`t withdraw money. Identify your account!");
            }

            if (_money < money)
            {
                throw new Exception("You can`t withdraw money, low balance!");
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