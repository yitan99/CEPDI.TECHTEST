using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEPDI.TECHTEST.Web.Pages
{
    public class LecturaXMLModel : PageModel
    {
        private readonly ILogger<LecturaXMLModel> _logger;

        public LecturaXMLModel(ILogger<LecturaXMLModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
