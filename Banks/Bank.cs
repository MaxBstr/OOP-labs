using System;
using System.Collections.Generic;
using Banks.Accounts;

namespace Banks
{
    public class Bank
    {
        private List<IAccount> _accounts = new List<IAccount>();
        private double _debitPercent;
        private List<double> _depositPercents;
        private double _creditLimit;
        private double _creditCommission;
        private Observer _accountObserver = new Observer();
        private int _doubtSum;
        private List<Transaction> _transactions = new List<Transaction>();
        
        public void CreateDebitAccount(Client client, double money)
        {
            var newAccount = new DebitAccount(_accounts.Count + 1, client, money, _doubtSum);
            newAccount.SetPercent(_debitPercent);
            _accounts.Add(newAccount);
            _accountObserver.AddSubscriber(newAccount);
        }
        
        public void CreateDepositAccount(Client client, double money, int deadlineDays)
        {
            var newAccount = new DepositAccount(_accounts.Count + 1, client, money, deadlineDays, _doubtSum);
            newAccount.SetPercents(_depositPercents);
            _accounts.Add(newAccount);
            _accountObserver.AddSubscriber(newAccount);
        }
        
        public void CreateCreditAccount(Client client, double money)
        {
            var newAccount = new CreditAccount(_accounts.Count + 1, client, money, _doubtSum);
            newAccount.SetPercents(_creditLimit, _creditCommission);
            _accounts.Add(newAccount);
            _accountObserver.AddSubscriber(newAccount);
        }

        public Bank(double debitPercent, List<double> depositPercents, double creditLimit, double creditCommission, int doubtSum)
        {
            _debitPercent = debitPercent;
            _depositPercents = depositPercents;
            _creditLimit = creditLimit;
            _creditCommission = creditCommission;
            _doubtSum = doubtSum;
        }

        public void RunTimeMachine(int days)
        {
            _accountObserver.NotifySubscribers(days);
        }

        private IAccount FindAccount(int id)
        {
            var account = _accounts.Find(acc => acc.GetId() == id);
            if (account == null)
                throw new Exception("Account was not found!");
            return account;
        }

        public void TransferMoney(int senderId, int receiverId, int money)
        {
            var sender = FindAccount(senderId);
            var receiver = FindAccount(receiverId);
            if (sender == null || receiver == null)
                throw new Exception("WARNING! Accounts does not exist!");
            
            sender.WithdrawMoney(money);
            receiver.AddMoney(money);
            _transactions.Add(new Transaction(_transactions.Count + 1, sender, receiver, money));
        }

        public void CancelTransaction(int transactionId)
        {
            var transaction = _transactions.Find(tr => tr._id == transactionId);
            if (transaction == null)
                throw new Exception($"Transaction {transactionId} was not found");
            
            transaction._receiver.WithdrawMoney(transaction._money);
            transaction._sender.AddMoney(transaction._money);
            _transactions.Remove(transaction);

            for (var i = 0; i < _transactions.Count; ++i)
                _transactions[i]._id = i + 1;
        }
        
        public void WithdrawMoneyFromAccountById(int accId, double money)
        {
            var account = FindAccount(accId);
            account.WithdrawMoney(money);
        }

        public void AddMoneyToAccountById(int accId, double money)
        {
            var account = FindAccount(accId);
            account.AddMoney(money);
        }
    }
}