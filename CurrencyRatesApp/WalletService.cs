using System;
using System.Linq;

public class WalletService
{
    private readonly AppDbContext _dbContext;

    public WalletService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Create a new wallet
    public Wallet CreateWallet(decimal initialBalance, string currency)
    {
        var wallet = new Wallet
        {
            Balance = initialBalance,
            Currency = currency
        };

        _dbContext.Wallets.Add(wallet);
        _dbContext.SaveChanges();

        return wallet;
    }

    // Retrieve wallet balance
    public decimal GetWalletBalance(long walletId, string? targetCurrency = null)
    {
        var wallet = _dbContext.Wallets.FirstOrDefault(w => w.Id == walletId);
        if (wallet == null)
            throw new Exception("Wallet not found");

        if (string.IsNullOrEmpty(targetCurrency) || wallet.Currency == targetCurrency)
            return wallet.Balance;

        // Perform currency conversion using CurrencyRates
        var rate = _dbContext.CurrencyRates.FirstOrDefault(r => r.Currency == targetCurrency)?.Rate;
        if (rate == null)
            throw new Exception("Target currency not supported");

        return wallet.Balance * (decimal)rate; // Cast rate to decimal
    }

    // Adjust wallet balance
    public void AdjustWalletBalance(long walletId, decimal amount, string strategy)
    {
        var wallet = _dbContext.Wallets.FirstOrDefault(w => w.Id == walletId);
        if (wallet == null)
            throw new Exception("Wallet not found");

        switch (strategy.ToLower())
        {
            case "addfunds":
                wallet.Balance += amount;
                break;

            case "subtractfunds":
                if (wallet.Balance < amount)
                    throw new Exception("Insufficient funds");
                wallet.Balance -= amount;
                break;

            case "forcesubtractfunds":
                wallet.Balance -= amount;
                break;

            default:
                throw new Exception("Invalid strategy");
        }

        _dbContext.SaveChanges();
    }
}
