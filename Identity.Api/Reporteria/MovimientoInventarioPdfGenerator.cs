using Identity.Api.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Identity.Api.Reporteria
{
    public static class MovimientoInventarioPdfGenerator
    {
        public static byte[] GenerarPdf(
            List<MovimientosInventarioDTO> datos,
            string? correo,
            DateTime? desde,
            DateTime? hasta,
            string? tipoMovimiento,
            int? idBodega,
            string? nombreProducto)
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

                container.Page(page =>
                {
                    page.Margin(20);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    // Header con logo, título, fecha, hora, correo
                    page.Header().Row(row =>
                    {
                        if (logoImage != null)
                            row.RelativeColumn(1).Height(40).Image(logoImage);
                        row.RelativeColumn(9).Column(col =>
                        {
                            col.Item().AlignCenter().Text("Reporte de Movimientos de Inventario")
                                .SemiBold().FontSize(14).FontColor(Colors.Blue.Medium);

                            col.Item().AlignRight().Text($"Fecha Emisión: {DateTime.Now:dd/MM/yyyy}")
                                .FontSize(7).FontColor(Colors.Grey.Darken1).Bold();

                            col.Item().AlignRight().Text($"Hora Emisión: {DateTime.Now:HH:mm:ss}")
                                .FontSize(7).FontColor(Colors.Grey.Darken1).Bold();

                            if (!string.IsNullOrWhiteSpace(correo))
                            {
                                col.Item().AlignRight().Text($"Generado por: {correo}")
                                    .FontSize(7).FontColor(Colors.Grey.Darken1).Italic();
                            }
                        });
                    });

                    // Tabla de movimientos
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40); // IdMovimiento
                            columns.RelativeColumn(4);  // Producto
                            columns.RelativeColumn(3);  // Bodega
                            columns.RelativeColumn(2);  // Tipo Movimiento
                            columns.RelativeColumn(2);  // Precio Unitario
                            columns.RelativeColumn(2);  // Fecha Movimiento

                            columns.RelativeColumn(2);  // stock anterior 

                            columns.RelativeColumn(1);  // Cantidad
                            columns.RelativeColumn(2);  // stockactual

                            //columns.RelativeColumn(3);  // Usuario Registro
                        });

                        // Header tabla
                        table.Header(header =>
                        {
                            header.Cell().Text("ID").Bold();
                            header.Cell().Text("Producto").Bold();
                            header.Cell().Text("Bodega").Bold();
                            header.Cell().Text("Tipo").Bold();
                            header.Cell().Text("Precio Unit.").Bold();
                            header.Cell().Text("Fecha").Bold();
                            header.Cell().Text("Stock Anterior").Bold();
                            header.Cell().Text("Cantidad").Bold();
                            header.Cell().Text("Stock Actual").Bold();

                            //header.Cell().Text("Usuario").Bold();
                        });

                        // Filas con datos
                        foreach (var mov in datos)
                        {
                            table.Cell().Text(mov.IdMovimiento.ToString());
                            table.Cell().Text(mov.NombreProducto ?? "-");
                            table.Cell().Text(mov.NombreBodega ?? "-");
                            table.Cell().Text(mov.TipoMovimiento ?? "-");

                            table.Cell().Text(mov.PrecioUnitario?.ToString("C2") ?? "-");
                            table.Cell().Text(mov.FechaMovimiento?.ToString("dd/MM/yyyy") ?? "-");

                            table.Cell().Text(mov.StockAnterior?.ToString("C2") ?? "-");
                            table.Cell().Text(mov.Cantidad.ToString("N2"));
                            table.Cell().Text(mov.StockActual?.ToString("C2") ?? "-");

                            //table.Cell().Text(mov.UsuarioRegistro ?? "-");
                        }
                    });

                    // Footer con paginado
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
