using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<CurrencyRate> CurrencyRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=currency_rates.db");
    }
}

public class CurrencyRate
{
    public int Id { get; set; }
    public string? Currency { get; set; }
    public decimal Rate { get; set; }
}
