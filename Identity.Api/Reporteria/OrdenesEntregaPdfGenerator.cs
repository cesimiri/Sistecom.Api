using Identity.Api.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Identity.Api.Reporteria
{
    public static class OrdenesEntregaPdfGenerator
    {
        public static byte[] GenerarPdf(OrdenesEntregaDTO orden, List<DetalleOrdenEntregaDTO> detalles)
        {
            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Sistecom2.jpeg");
            byte[]? logoImage = null;

            if (File.Exists(logoPath))
                logoImage = File.ReadAllBytes(logoPath);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    // ============================
                    // HEADER
                    // ============================
                    page.Header().Row(row =>
                    {
                        // Logo
                        row.RelativeColumn(1).Element(x =>
                        {
                            if (logoImage != null)
                                x.Width(100).Image(logoImage, ImageScaling.FitWidth);
                        });

                        // Datos de la orden
                        row.RelativeColumn(3).AlignMiddle().AlignRight().Column(col =>
                        {
                            col.Item().Text("ORDEN DE ENTREGA")
                                .Bold()
                                .FontSize(18);

                            col.Item().Text($"Orden N°: {orden.NumeroOrden}")
                                .Bold()
                                .FontSize(14);

                            col.Item().Text($"Departamento: {orden.NombreDepartamento}");
                            col.Item().Text($"Solicitud #: {orden.NumeroSolicitud}");

                            col.Item().Text($"Fecha programada: {orden.FechaProgramada:dd/MM/yyyy}");
                            col.Item().Text($"Fecha entrega: {orden.FechaEntrega:dd/MM/yyyy}");

                            col.Item().PaddingTop(5)
                                .Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}")
                                .FontSize(8)
                                .FontColor(Colors.Grey.Darken1);
                        });
                    });

                    // ============================
                    // CONTENIDO
                    // ============================
                    page.Content().PaddingTop(10).Column(col =>
                    {
                        // Línea separadora
                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        // Información adicional
                        col.Item().Column(info =>
                        {
                            info.Item().Text($"Dirección entrega: {orden.DireccionEntrega}");
                            info.Item().Text($"Persona recepción: {orden.ContactoRecepcion}");
                            info.Item().Text($"Teléfono: {orden.TelefonoContacto}");
                            info.Item().Text($"Guía remisión: {orden.GuiaRemision ?? "No aplica"}");
                            info.Item().Text($"Transportista: {orden.Transportista ?? "No registrado"}");
                            info.Item().Text($"Observaciones: {orden.ObservacionesEntrega ?? "Sin observaciones"}");
                        });

                        col.Item().PaddingTop(10);

                        // ============================
                        // TABLA DE DETALLES
                        // ============================
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {

                                columns.ConstantColumn(80); // Cantidad entregad
                                columns.RelativeColumn(4);  // Producto
                                columns.RelativeColumn(4);  // Observaciones
                            });

                            // Header de la tabla
                            table.Header(header =>
                            {

                                header.Cell().Element(HeaderCell).AlignRight().Text("Entregado");
                                header.Cell().Element(HeaderCell).Text("Producto");
                                header.Cell().Element(HeaderCell).Text("Observaciones");

                                static IContainer HeaderCell(IContainer c) =>
                                    c.Padding(5)
                                     .Background(Colors.Grey.Lighten2)
                                     .BorderBottom(1)
                                     .BorderColor(Colors.Black);
                            });

                            // Filas
                            foreach (var item in detalles)
                            {

                                table.Cell().Element(Cell).AlignRight().Text(item.CantidadEntregada.ToString());
                                table.Cell().Element(Cell).Text(item.NombreProducto);
                                table.Cell().Element(Cell).Text(item.Observaciones ?? "");


                                static IContainer Cell(IContainer c) =>
                                    c.Padding(5);
                            }
                        });

                        col.Item().PaddingTop(25);

                        // ============================
                        // FIRMAS
                        // ============================
                        col.Item().Row(row =>
                        {
                            row.RelativeColumn().Column(c =>
                            {
                                c.Item().Text("Firma de quien entrega:").Bold();
                                c.Item().PaddingTop(25).LineHorizontal(1);
                            });

                            row.RelativeColumn().Column(c =>
                            {
                                c.Item().Text("Firma de quien recibe:").Bold();
                                c.Item().PaddingTop(25).LineHorizontal(1);
                            });
                        });
                    });

                    // ============================
                    // FOOTER
                    // ============================
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
