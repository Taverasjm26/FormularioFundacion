using Microsoft.AspNetCore.Mvc;

namespace Formulario.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public AdminController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET: /Admin
        public IActionResult Index(string key)
        {
            var folder = Path.Combine(_env.ContentRootPath, "Storage", "vouchers");

            if (!Directory.Exists(folder))
                return View(Array.Empty<string>());

            var files = Directory.GetFiles(folder, "*.pdf")
                                 .Select(Path.GetFileName)
                                 .OrderByDescending(x => x)
                                 .ToArray();

            return View(files);
        }

        public IActionResult Descargar(string file)
        {
            var folder = Path.Combine(_env.ContentRootPath, "Storage", "vouchers");
            var fullPath = Path.Combine(folder, file);

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var bytes = System.IO.File.ReadAllBytes(fullPath);
            return File(bytes, "application/pdf", file);
        }
    }
}
