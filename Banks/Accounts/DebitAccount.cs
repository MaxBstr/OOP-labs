using System;

namespace Banks.Accounts
{
    public class DebitAccount : IAccount
    {
        private readonly Client _client;
        private double _percent;
        private double _money;
        private int _id;
        private readonly double _doubtSum;
        private int _accountDate = 1;
        private double _cashback;

        public DebitAccount(int id, Client client, double money, double doubtSum)
        {
            _id = id;
            _client = client;
            _money = money;
            _doubtSum = doubtSum;
        }

        public void SetPercent(double percent)
        {
            _percent = percent / 100.0;
        }

        public void CalculatePercents(int days)
        {
            for (var i = 1; i <= days; ++i)
            {
                _cashback += _money * _percent / 365.0;
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
            if (money > _doubtSum && !_client.IsLegal || money > _money)
            {
                throw new Exception("You can`t withdraw money. Your account is not identified or low balance!");
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