using System;

class Account
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }

    public Account(string accountNumber, decimal balance)
    {
        AccountNumber = accountNumber;
        Balance = balance;
    }

    public virtual void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Invalid withdrawal amount.");
            return;
        }

        if (amount > Balance)
        {
            Console.WriteLine("Insufficient balance.");
            return;
        }

        Balance -= amount;
    }
}

class CurrentAccount : Account
{
    private int transactionCount;

    public CurrentAccount(string accountNumber, decimal balance) : base(accountNumber, balance)
    {
        transactionCount = 0;
    }

    public override void Withdraw(decimal amount)
    {
        base.Withdraw(amount);

        transactionCount++;

        if (transactionCount > 3)
        {
            Balance -= 500;
            Console.WriteLine("Exceeded transaction limit. 500 deducted from your account.");
            return; // End the process after deducting 500
        }
    }
}

class SavingsAccount : Account
{
    private int transactionCount;

    public SavingsAccount(string accountNumber, decimal balance) : base(accountNumber, balance)
    {
        transactionCount = 0;
    }

    public override void Withdraw(decimal amount)
    {
        base.Withdraw(amount);

        transactionCount++;

        if (transactionCount > 3)
        {
            Balance -= 500;
            Console.WriteLine("Exceeded transaction limit. 500 deducted from your account.");
            return; // End the process after deducting 500
        }
    }
}

class ChildAccount : Account
{
    public ChildAccount(string accountNumber, decimal balance) : base(accountNumber, balance)
    {
    }

    public override void Withdraw(decimal amount)
    {
        if (amount > 1000)
        {
            Console.WriteLine("Maximum withdrawal limit for Child Account is 1000.");
            return;
        }

        base.Withdraw(amount);
    }
}

class ATM
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Select an account type:");
        Console.WriteLine("1. Savings Account");
        Console.WriteLine("2. Current Account");
        Console.WriteLine("3. Child Account");

        int accountType = Convert.ToInt32(Console.ReadLine());

        Account selectedAccount;

        switch (accountType)
        {
            case 1:
                selectedAccount = new SavingsAccount("S001", 5000);
                break;
            case 2:
                selectedAccount = new CurrentAccount("C001", 10000);
                break;
            case 3:
                selectedAccount = new ChildAccount("CH001", 2000);
                break;
            default:
                Console.WriteLine("Invalid account type.");
                return;
        }

        Console.WriteLine("Account Number: " + selectedAccount.AccountNumber);
        Console.WriteLine("Current Balance: " + selectedAccount.Balance);

        int transactionCount = 0;

        while (true)
        {
            Console.WriteLine("Enter withdrawal amount (0 to exit):");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            if (amount == 0)
                break;

            selectedAccount.Withdraw(amount);

            Console.WriteLine("Updated Balance: " + selectedAccount.Balance);
            Console.WriteLine("---------------------------");

            transactionCount++;

            if (transactionCount >= 3)
            {
                selectedAccount.Balance -= 500;
                Console.WriteLine("Exceeded transaction limit. 500 deducted from your account.");
                break; // End the process after deducting 500
            }
        }
    }
}

