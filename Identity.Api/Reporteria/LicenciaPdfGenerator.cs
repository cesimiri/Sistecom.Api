using Identity.Api.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Identity.Api.Reporteria
{
    public static class LicenciaPdfGenerator
    {
        public static byte[] GenerarPdf(List<LicenciaDTO> empresas, string? correo)
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
                    //page.Size(PageSizes.A4);  //vertical
                    page.Size(PageSizes.A4.Landscape()); //horizontal

                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Row(row =>
                    {
                        if (logoImage != null)
                            row.RelativeColumn(1).Height(40).Image(logoImage);
                        row.RelativeColumn(9).Column(col =>
                        {
                            col.Item().AlignCenter().Text("Listado de Licencias")
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
                            columns.RelativeColumn(3); // Tipo De Licencia
                            columns.RelativeColumn(3); // Producto
                            columns.RelativeColumn(3); // Tipo de Suscripción
                            columns.RelativeColumn(3); // # Factura
                            columns.RelativeColumn(2); // Fecha Adquisición
                            columns.RelativeColumn(2); // Inicio Vigencia
                            columns.RelativeColumn(2); // Fin Vigencia
                            columns.RelativeColumn(2); // Cantidad de Usuarios
                            columns.RelativeColumn(2); // costo
                            //columns.RelativeColumn(2); // Estado
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Tipo De Licencia").Bold();
                            header.Cell().Text("Producto").Bold();
                            header.Cell().Text("Tipo de Suscripción").Bold();
                            header.Cell().Text("# Factura").Bold();
                            header.Cell().Text("Fecha Adquisición").Bold();
                            header.Cell().Text("Inicio Vigencia").Bold();
                            header.Cell().Text("Fin Vigencia").Bold();
                            header.Cell().Text("Cantidad de Usuarios").Bold();
                            header.Cell().Text("costo").Bold();
                            //header.Cell().Text("Estado").Bold();
                        });

                        foreach (var emp in empresas)
                        {
                            table.Cell().Text(emp.nombreLicencia);
                            table.Cell().Text(emp.nombreProducto);
                            table.Cell().AlignCenter().AlignMiddle().Text(emp.TipoSuscripcion);
                            table.Cell().Text(emp.numeroFactura);
                            table.Cell().Text(emp.FechaAdquisicion.ToString("yyyy-MM-dd"));
                            table.Cell().Text(emp.FechaInicioVigencia?.ToString("yyyy-MM-dd"));
                            table.Cell().Text(emp.FechaFinVigencia?.ToString("yyyy-MM-dd"));
                            table.Cell().AlignCenter().AlignMiddle().Text(emp.CantidadUsuarios?.ToString() ?? "");
                            table.Cell().Text(emp.CostoLicencia.ToString());
                            //table.Cell().Text(emp.Estado);
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
