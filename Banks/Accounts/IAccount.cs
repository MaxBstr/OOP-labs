namespace Banks.Accounts
{
    public interface IAccount
    {
        void CalculatePercents(int days);
        void WithdrawMoney(double money);
        void AddMoney(double money);
        int GetId();
    }
}