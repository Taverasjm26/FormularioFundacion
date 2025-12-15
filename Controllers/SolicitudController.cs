using Formulario.Models;
using Formulario.Service;
using Microsoft.AspNetCore.Mvc;

namespace Formulario.Controllers
{
    public class SolicitudController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public SolicitudController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET: /Solicitud/Crear
        [HttpGet]
        public IActionResult Crear()
        {
            return View(new SolicitudInput());
        }

        // POST: /Solicitud/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(SolicitudInput input)
        {
            if (!ModelState.IsValid)
                return View(input);

            // 1) Crear un código único para el voucher
            var codigo = $"VCH-{DateTime.Now:yyyyMMdd-HHmmss}";

            // 2) Generar el PDF en memoria
            var pdfBytes = VoucherPdf.Generate(input, codigo);

            // 3) Guardar PDF en wwwroot/vouchers
            var folder = Path.Combine(_env.ContentRootPath, "Storage", "vouchers");
            Directory.CreateDirectory(folder);

            var fileName = $"{codigo}.pdf";
            var fullPath = Path.Combine(folder, fileName);

            System.IO.File.WriteAllBytes(fullPath, pdfBytes);

            // 4) Mensaje para el popup y pasar el nombre del archivo
            TempData["Mensaje"] = @"¡Listo! 
          Felicidades, estas a un paso de recibir tu ayuda economica. Para concluir con el proceso debe dirigirse a una sucursal de Western Union y enviar el pago del impuesto correspondiente.
           Una vez haya enviado el pago debera enviar el recibo al Licenciado Carlos
           Ramirez para liberar el envio de su ayuda de inmediato. Al presionar el boton aceptar recibira la informacion necesaria para hacer el pago del impuesto!.";
            TempData["Archivo"] = fileName;

            return RedirectToAction("Listo");
        }

        // GET: /Solicitud/Listo
        [HttpGet]
        public IActionResult Listo()
        {
            return View();
        }
    }
}



  