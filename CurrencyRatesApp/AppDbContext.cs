using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<CurrencyRate> CurrencyRates { get; set; }
    public DbSet<Wallet> Wallets { get; set; } // Add Wallet for 3rd task

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=currency_rates.db");
    }
}

[Index(nameof(Currency), IsUnique = true)]
public class CurrencyRate
{
    public int Id { get; set; }
    public string? Currency { get; set; }
    public decimal Rate { get; set; }
}


