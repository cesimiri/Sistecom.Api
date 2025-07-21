using Identity.Api.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Identity.Api.Reporteria
{
    public class StockBodegaPdfGenerator
    {
        public static byte[] Generate(List<stockBodegaDTO> datos, string? correo)
        {
            var document = Document.Create(container =>
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
                        //era 4 alinicio 
                        row.RelativeColumn(9).Column(col =>
                        {
                            col.Item().AlignCenter().Text("Reporte Stock Bodega")
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

                    page.Content()
                        .Table(table =>
                        {
                            // Columnas con anchos fijos o proporcionales
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1); // ID
                                columns.RelativeColumn(3); // Producto
                                columns.RelativeColumn(2); // Cantidad Disponible
                                columns.RelativeColumn(2); // Cantidad Reservada
                                columns.RelativeColumn(2); // Cantidad Ensamblaje
                                columns.RelativeColumn(2); // Valor Promedio
                                columns.RelativeColumn(2); // Última Entrada
                                columns.RelativeColumn(2); // Última Salida
                            });

                            // Header
                            table.Header(header =>
                            {
                                //header.Cell().Element(CellStyle).Background(Colors.Grey.Lighten3).Text("ID")
                                //    .FontSize(12).FontColor(Colors.Blue.Darken2).SemiBold(); 
                                header.Cell().Element(CellStyle).Text("ID")
                                    .FontSize(12).FontColor(Colors.Black).Bold();
                                header.Cell().Element(CellStyle).Text("Producto")
                                    .FontSize(12).FontColor(Colors.Black).Bold();
                                header.Cell().Element(CellStyle).Text("Cant. Disponible")
                                    .FontSize(12).FontColor(Colors.Black).Bold();
                                header.Cell().Element(CellStyle).Text("Cant. Reservada")
                                    .FontSize(12).FontColor(Colors.Black).Bold();
                                header.Cell().Element(CellStyle).Text("Cant. Ensamblaje")
                                    .FontSize(11).FontColor(Colors.Black).Bold();
                                header.Cell().Element(CellStyle).Text("Valor Promedio")
                                    .FontSize(12).FontColor(Colors.Black).Bold();
                                header.Cell().Element(CellStyle).Text("Última Entrada")
                                    .FontSize(12).FontColor(Colors.Black).Bold();
                                header.Cell().Element(CellStyle).Text("Última Salida")
                                    .FontSize(12).FontColor(Colors.Black).Bold();
                            });


                            // Datos
                            foreach (var d in datos)
                            {
                                table.Cell().Element(CellStyle).Text(d.IdStock.ToString());
                                table.Cell().Element(CellStyle).Text(d.nombreProducto);
                                table.Cell().Element(CellStyle).Text(d.CantidadDisponible.ToString());
                                table.Cell().Element(CellStyle).Text(d.CantidadReservada.ToString());
                                table.Cell().Element(CellStyle).Text(d.CantidadEnsamblaje.ToString());
                                table.Cell().Element(CellStyle).Text(
                                    d.ValorPromedio.HasValue
                                        ? d.ValorPromedio.Value.ToString("F2", CultureInfo.InvariantCulture)
                                        : ""
                                );
                                table.Cell().Element(CellStyle).Text(d.UltimaEntrada?.ToString("dd/MM/yyyy") ?? "");
                                table.Cell().Element(CellStyle).Text(d.UltimaSalida?.ToString("dd/MM/yyyy") ?? "");
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
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

        private static IContainer CellStyle(IContainer container)
        {
            return container
                .Padding(5)
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten2);
        }
    }
}
