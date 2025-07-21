using Identity.Api.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Identity.Api.Reporteria
{
    public static class SuscripcionePdfGenerator
    {
        public static byte[] GenerarPdf(List<SuscripcionDto> empresas, string? correo)
        {
            var doc = Document.Create(container =>
            {
                // ruta del logo 
                var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Sistecom2.jpeg");
                byte[] logoImage = null;
                if (File.Exists(logoPath))
                {
                    logoImage = File.ReadAllBytes(logoPath);
                }
                //fin

                container.Page(page =>
                {
                    page.Margin(20);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Row(row =>
                    {
                        if (logoImage != null)
                            row.RelativeColumn(1).Height(40).Image(logoImage);
                        row.RelativeColumn(9).Column(col =>
                        {
                            col.Item().AlignCenter().Text("Listado de Suscripciones")
                                .SemiBold().FontSize(14).FontColor(Colors.Blue.Medium);

                            col.Item().AlignRight().Text($"Fecha Emisión: {DateTime.Now:dd/MM/yyyy}")
                                .FontSize(7).FontColor(Colors.Grey.Darken1).Bold();

                            col.Item().AlignRight().Text($"Hora Emisión: {DateTime.Now: HH:mm:ss}")
                                .FontSize(7).FontColor(Colors.Grey.Darken1).Bold();

                            if (!string.IsNullOrWhiteSpace(correo))
                            {
                                col.Item().AlignRight().Text($"Generado por: {correo}")
                                    .FontSize(7).FontColor(Colors.Grey.Darken1).Italic();
                            }
                        });
                    });

                    //    page.Header().Column(col =>
                    //{
                    //    col.Item().AlignCenter().Text("Listado de Empresas").FontSize(14).Bold();
                    //    col.Item().AlignRight().Text($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}").FontSize(9);
                    //    col.Item().AlignRight().Text($"Usuario: {correo}").FontSize(9);
                    //});

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); // Servicio
                            columns.RelativeColumn(3); // Proveedor
                            columns.RelativeColumn(5); // Empresa
                            columns.RelativeColumn(3); // Tiposuscripcion
                            columns.RelativeColumn(3); // PeriodoFacturacion
                            columns.RelativeColumn(3); // Fecha Renovacion
                            columns.RelativeColumn(2); // Costo
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Servicio").Bold();
                            header.Cell().Text("Proveedor").Bold();
                            header.Cell().Text("Empresa").Bold();
                            header.Cell().Text("Tipo").Bold();
                            header.Cell().Text("Periodo").Bold();
                            header.Cell().Text("Fecha Renovacion").Bold();
                            header.Cell().Text("Costo").Bold();
                        });

                        foreach (var emp in empresas)
                        {
                            table.Cell().Text(emp.NombreServicio);
                            table.Cell().Text(emp.RazonSocialProveedor);
                            table.Cell().Text(emp.RazonSocialEmpresa);
                            table.Cell().Text(emp.TipoSuscripcion);
                            table.Cell().Text(emp.PeriodoFacturacion);
                            table.Cell().Text(emp.FechaRenovacion.ToString("yyyy-MM-dd"));
                            table.Cell().Text(emp.CostoPeriodo);
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
                });
            });

            return doc.GeneratePdf();
        }
    }
}
