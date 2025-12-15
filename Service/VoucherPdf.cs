using System;
using Formulario.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Formulario.Service
{
    public static class VoucherPdf
    {
        public static byte[] Generate(SolicitudInput input, string codigo)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var fecha = DateTime.Now;

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A6);
                    page.Margin(16);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("COMPROBANTE")
                        .SemiBold().FontSize(14).AlignCenter();

                    page.Content().Column(col =>
                    {
                        col.Spacing(6);

                        col.Item().Text($"Código: {codigo}").SemiBold();
                        col.Item().Text($"Fecha: {fecha:dd/MM/yyyy HH:mm}");

                        col.Item().LineHorizontal(1);

                        col.Item().Text($"Nombre: {input.Nombre}");
                        col.Item().Text($"Teléfono: {input.Telefono}");
                        col.Item().Text($"País: {input.Pais}");
                        col.Item().Text($"Cédula: {input.Cedula}");
                        col.Item().Text($"Cantidad: RD$ {input.Cantidad:N2}").SemiBold();
                        col.Item().Text($"Propósito: {input.Proposito}");

                        col.Item().LineHorizontal(1);

                        col.Item().Text("Documento informativo – No es comprobante de pago.")
                           .FontSize(8).FontColor(Colors.Grey.Darken2);
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Uso interno del negocio")
                        .FontSize(8).FontColor(Colors.Grey.Darken2);
                });
            });

            return doc.GeneratePdf();
        }
    }
}

