using Identity.Api.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Identity.Api.Reporteria
{
    public static class ProductoPdfGenerator
    {
        public static byte[] GenerarPdf(List<ProductoDTO> empresas, string? correo)
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
                            col.Item().AlignCenter().Text("Reporte de Productos")
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
                            columns.RelativeColumn(1); // Codigo
                            columns.RelativeColumn(2); // Marca
                            columns.RelativeColumn(2); // Modelo
                            columns.RelativeColumn(2); // Tipo
                            columns.RelativeColumn(3); // Descripcion
                            columns.RelativeColumn(1); // Precio
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Codigo").Bold();
                            header.Cell().Text("Marca").Bold();
                            header.Cell().Text("Modelo").Bold();
                            header.Cell().Text("Tipo").Bold();
                            header.Cell().Text("Descripcion").Bold();
                            header.Cell().Text("Precio").Bold();
                        });

                        foreach (var emp in empresas)
                        {
                            table.Cell().Text(emp.CodigoPrincipal);
                            table.Cell().Text(emp.NombreMarca);
                            table.Cell().Text(emp.NombreModelo);
                            table.Cell().Text(emp.TipoProducto);
                            table.Cell().Text(emp.Descripcion);
                            table.Cell().Text(emp.PrecioUnitario);
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
