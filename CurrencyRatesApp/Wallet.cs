public class Wallet
{
    public long Id { get; set; } // Id: long 
    public decimal Balance { get; set; } // Balance: decimal
    public string? Currency { get; set; } // Currency: string | the ? makes currency nullable in order to avoid the console error
}


// Id: long (the unique identifier of the wallet)
// Balance: decimal (the available balance in the wallet)
// Currency: string (the currency in which the wallet balance is maintained)
