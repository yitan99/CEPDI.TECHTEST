using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEPDI.TECHTEST.Web.Pages
{
    public class MedicamentosModel : PageModel
    {
        private readonly ILogger<MedicamentosModel> _logger;

        public MedicamentosModel(ILogger<MedicamentosModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
