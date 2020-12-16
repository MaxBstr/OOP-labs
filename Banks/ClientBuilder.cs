namespace Banks
{
    public interface IBuilder
    {
        IBuilder SetName(string name);
        IBuilder SetSurname(string surname);
        IBuilder SetAddress(string address);
        IBuilder SetPassport(int passport);
        void Reset();
        Client Create();
    }

    public class ClientBuilder : IBuilder
    {
        private string _name;
        private string _surname;
        private int _passport;
        private string _address = "";
        
        public IBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public IBuilder SetSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public IBuilder SetAddress(string address)
        {
            _address = address;
            return this;
        }

        public IBuilder SetPassport(int passport)
        {
            _passport = passport;
            return this;
        }

        public void Reset()
        {
            _passport = 0;
            _name = "";
            _surname = "";
            _address = "";
        }

        public Client Create()
        {
            return new Client(_name, _surname, _address, _passport);
        }
    }
}