namespace Banking.Model
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual BankAccount BankAccount { get; set; }
    }
}
