using Identity.Api.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Identity.Api.Reporteria
{
    public static class FacturaPdfGenerator
    {
        public static byte[] GenerarPdf(FacturasCompraDTO factura, List<DetalleFacturaCompraDTO> detalles, string correoUsuario)
        {
            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Sistecom2.jpeg");
            byte[] logoImage = null;
            if (File.Exists(logoPath))
            {
                logoImage = File.ReadAllBytes(logoPath);
            }

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    // HEADER con logo y datos
                    page.Header().Row(row =>
                    {
                        // Columna logo
                        row.RelativeColumn(1).Element(x =>
                        {
                            if (logoImage != null)
                                x.Width(100).Image(logoImage, ImageScaling.FitWidth);
                        });

                        // Columna datos factura
                        row.RelativeColumn(3).AlignMiddle().AlignRight().Column(col =>
                        {
                            col.Item().Text($"Factura n°: {factura.NumeroFactura}").Bold().FontSize(16);
                            col.Item().Text($"Proveedor: {factura.RazonSocial}");
                            col.Item().Text($"Fecha: {factura.FechaEmision:dd/MM/yyyy}");
                            col.Item().Text($"Forma de pago: {factura.FormaPago}");
                            if (!string.IsNullOrWhiteSpace(correoUsuario))
                            {
                                col.Item().Text($"Generado por: {correoUsuario}").Italic().FontSize(8).FontColor(Colors.Grey.Darken1);
                            }
                            col.Item().Text($"Fecha emisión PDF: {DateTime.Now:dd/MM/yyyy HH:mm:ss}").FontSize(8).FontColor(Colors.Grey.Darken1);
                        });
                    });

                    page.Content().PaddingTop(10).Column(col =>
                    {
                        // Separador
                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        // Tabla detalles
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); // Producto
                                columns.RelativeColumn(4); // Observación
                                columns.ConstantColumn(60); // Cantidad
                                columns.ConstantColumn(80); // Precio
                                columns.ConstantColumn(80); // Subtotal
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Producto");
                                header.Cell().Element(CellStyle).Text("Observación");
                                header.Cell().Element(CellStyle).AlignRight().Text("Cantidad");
                                header.Cell().Element(CellStyle).AlignRight().Text("Precio");
                                header.Cell().Element(CellStyle).AlignRight().Text("Subtotal");

                                static IContainer CellStyle(IContainer container) =>
                                    container.Padding(5).Background(Colors.Grey.Lighten2).BorderBottom(1).BorderColor(Colors.Black);
                            });

                            foreach (var item in detalles)
                            {
                                //table.Cell().Element(CellStyle).Text(item.IdProducto.ToString());
                                table.Cell().Element(CellStyle).Text(item.NombreProducto ?? "Sin nombre");
                                table.Cell().Element(CellStyle).Text(item.NumerosSerie ?? "");
                                table.Cell().Element(CellStyle).AlignRight().Text(item.Cantidad.ToString());
                                table.Cell().Element(CellStyle).AlignRight().Text(item.PrecioUnitario.ToString("0.00"));
                                table.Cell().Element(CellStyle).AlignRight().Text(item.Subtotal.ToString("0.00"));

                                static IContainer CellStyle(IContainer container) =>
                                    container.Padding(5);
                            }
                        });

                        // Totales al final, alineados a la derecha
                        col.Item().PaddingTop(10).AlignRight().Column(total =>
                        {
                            var subtotal = detalles.Sum(d => d.Subtotal);
                            var iva = factura.Iva;
                            var totalVal = factura.ValorTotal;

                            total.Item().Text($"Subtotal: ${subtotal:0.00}");
                            total.Item().Text($"IVA: ${iva:0.00}");
                            total.Item().Text($"Total: ${totalVal:0.00}").Bold();
                        });
                    });

                    // Footer con paginación
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
                });
            });

            return document.GeneratePdf();
        }

    }
}
