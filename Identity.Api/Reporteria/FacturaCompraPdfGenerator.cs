using Identity.Api.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Identity.Api.Reporteria
{
    public static class FacturaCompraPdfGenerator
    {
        public static byte[] GenerarPdf(List<FacturasCompraDTO> empresas, string? correo)
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
                            col.Item().AlignCenter().Text("Listado de Facturas")
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


                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); // # Factura
                            columns.RelativeColumn(3); // Proveedor
                            columns.RelativeColumn(3); // Fecha Emisión
                            columns.RelativeColumn(2); // Valor Total
                            columns.RelativeColumn(2); // Forma Pago
                            //columns.RelativeColumn(2); // Estado
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("# Factura").Bold();
                            header.Cell().Text("Proveedor").Bold();
                            header.Cell().Text("Fecha Emisión").Bold();
                            header.Cell().Text("Valor Total").Bold();
                            header.Cell().Text("Forma Pago").Bold();
                            //header.Cell().Text("Estado").Bold();
                        });

                        foreach (var emp in empresas)
                        {
                            table.Cell().Text(emp.NumeroFactura);
                            table.Cell().Text(emp.NombreProveedor);
                            table.Cell().Text(emp.FechaEmision.ToString("dd/MM/yyyy"));
                            table.Cell().Text(emp.ValorTotal.ToString());
                            table.Cell().Text(emp.FormaPago);
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
