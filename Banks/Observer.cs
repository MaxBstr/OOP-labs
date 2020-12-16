using System.Collections.Generic;
using Banks.Accounts;

namespace Banks
{
    public class Observer
    {
        private readonly List<IAccount> _observers = new List<IAccount>();

        public void AddSubscriber(IAccount subscriber)
        {
            _observers.Add(subscriber);
        }

        public void NotifySubscribers(int days)
        {
            foreach (var account in _observers)
            {
                account.CalculatePercents(days);
            }
        }
    }
}