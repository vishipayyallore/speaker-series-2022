using HealthCheckUIDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace HealthCheckUIDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ISQLDbHealthCheckService _sQLDbHealthCheckService;

        public IndexModel(ILogger<IndexModel> logger, ISQLDbHealthCheckService sQLDbHealthCheckService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _sQLDbHealthCheckService = sQLDbHealthCheckService ?? throw new ArgumentNullException(nameof(sQLDbHealthCheckService));
        }

        [BindProperty]
        public Dictionary<string, dynamic> Status { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var data = await _sQLDbHealthCheckService.GetSQLHealthCheck();

            //your result
            Status = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(data);

            //change like below
            //var d = JsonDocument.Parse(data);  //JsonDocument.Parse(reader.ReadToEnd())
            //var result = d.RootElement.EnumerateObject();

            return Page();
        }
    }
}