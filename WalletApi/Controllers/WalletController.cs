using CurrencyRatesGateway;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WalletController : ControllerBase
{
    private readonly WalletService _walletService;

    public WalletController(AppDbContext dbContext)
    {
        _walletService = new WalletService(dbContext);
    }

    // POST /api/wallets
    [HttpPost]
    public IActionResult CreateWallet([FromBody] CreateWalletRequest request)
    {
        var wallet = _walletService.CreateWallet(request.InitialBalance, request.Currency);
        return Ok(wallet);
    }

    // GET /api/wallets/{walletId}
    [HttpGet("{walletId}")]
    public IActionResult GetWalletBalance(long walletId, [FromQuery] string? currency = null)
    {
        try
        {
            var balance = _walletService.GetWalletBalance(walletId, currency);
            return Ok(new { WalletId = walletId, Balance = balance, Currency = currency ?? "Original Currency" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST /api/wallets/{walletId}/adjustbalance
    [HttpPost("{walletId}/adjustbalance")]
    public IActionResult AdjustWalletBalance(long walletId, [FromBody] AdjustBalanceRequest request)
    {
        try
        {
            _walletService.AdjustWalletBalance(walletId, request.Amount, request.Strategy);
            return Ok(new { Message = "Balance updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class CreateWalletRequest
{
    public decimal InitialBalance { get; set; }
    public string Currency { get; set; }
}

public class AdjustBalanceRequest
{
    public decimal Amount { get; set; }
    public string Strategy { get; set; }
}
