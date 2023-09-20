using System;

class BankAccount
{
    private decimal balance;

    public BankAccount(decimal initialBalance)
    {
        if (initialBalance < 0)
        {
            throw new ArgumentException("Початковий баланс не може бути від'ємним.");
        }

        this.balance = initialBalance;
    }

    public decimal GetBalance()
    {
        return balance;
    }

    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Сума для внесення не може бути від'ємною.");
        }

        balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Сума для зняття не може бути від'ємною.");
        }

        if (amount > balance)
        {
            throw new InsufficientFundsException("Недостатньо коштів на рахунку.");
        }

        balance -= amount;
    }
}

class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message) : base(message)
    {
    }
}

class ATM
{
    public static void WithdrawMoney(BankAccount account, decimal amount)
    {
        try
        {
            account.Withdraw(amount);
            Console.WriteLine($"Знято {amount} грн. Поточний баланс: {account.GetBalance()} грн.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            BankAccount account = new BankAccount(1000); 
            Console.WriteLine($"Початковий баланс: {account.GetBalance()} грн.");

            
            account.Deposit(500);
            Console.WriteLine($"Внесено 500 грн. Поточний баланс: {account.GetBalance()} грн.");

           
            ATM.WithdrawMoney(account, 300); 

            
            ATM.WithdrawMoney(account, 1500); 

            
            account.Deposit(-200); 
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}
