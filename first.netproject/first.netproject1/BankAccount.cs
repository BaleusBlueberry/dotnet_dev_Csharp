// See https://aka.ms/new-console-template for more information

namespace Loops { 

    public class BankAccount
    {
        public string AccountHolderName;
        public decimal Balance;

/*        public BankAccount(string accountHolderName, decimal balance )
        {
            this.AccountHolderName = accountHolderName;
            this.Balance = balance;
        }*/

        public BankAccount() { }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;

        }

        public bool Withdraw(decimal amount)
        {
            if (this.Balance <= amount) {
            this.Balance -= amount;
                return true;
            }
            return false;
        }
        public void DisplayAccountInfo()
        {
            Console.WriteLine($"Account Holder {AccountHolderName}");
            Console.WriteLine($"Balance: {Balance}");
        }
    }
}
