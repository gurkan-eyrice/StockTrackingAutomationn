using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StockTrackingAutomationn.Pages
{
    public class SalesModel : PageModel
    {
        private readonly ILogger<SalesModel> _logger;

        public SalesModel(ILogger<SalesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
