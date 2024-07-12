using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Pages
{
    public class UploadModel : PageModel
    {
        private readonly ILogger<UploadModel> _logger;

        public UploadModel(ILogger<UploadModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public IFormFile File { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (File != null && File.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Fileupload", File.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(stream);
                }

                Message = "File uploaded successfully!";
            }

            return Page();
        }
    }
}
