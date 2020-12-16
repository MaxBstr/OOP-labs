namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var bankDirector = new BankDirector();
            var builder = new BankBuilder();
            bankDirector.BuildSberBank(builder);
            var bank = builder.Create();
            
            var client = new Client("max", "asdas", "", 0);
            bank.CreateCreditAccount(client, 50000);
            bank.CreateDebitAccount(client, 100000);
            bank.CreateDepositAccount(client, 60000, 31);
            bank.RunTimeMachine(15);
            bank.RunTimeMachine(15);
        }
    }
}