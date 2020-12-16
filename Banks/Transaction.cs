using Banks.Accounts;

namespace Banks
{
    public class Transaction
    {
        public IAccount _sender;
        public IAccount _receiver;
        public double _money;
        public int _id;

        public Transaction(int id, IAccount sender, IAccount receiver, double money)
        {
            _sender = sender;
            _receiver = receiver;
            _money = money;
            _id = id;
        }
    }
}