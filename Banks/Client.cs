namespace Banks
{
    public class Client
    {
        private string _name;
        private string _surname;
        private int _passport;
        private string _address;
        public bool IsLegal;

        public Client(string name, string surname, string address, int passport)
        {
            this._name = name;
            this._surname = surname;
            this._address = address;
            this._passport = passport;
            if (address != "" || passport != 0)
                IsLegal = true;
        }

        public void SetPassport(int pass)
        {
            _passport = pass;
            IsLegal = true;
        }

        public void SetAddress(string address)
        {
            _address = address;
            IsLegal = true;
        }
    }
}