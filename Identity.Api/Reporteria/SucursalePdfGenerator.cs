using Identity.Api.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Identity.Api.Reporteria
{
    public static class SucursalePdfGenerator
    {
        public static byte[] GenerarPdf(List<SucursaleDTO> empresas, string? correo)
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
                            col.Item().AlignCenter().Text("Listado de Sucursales")
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
                            columns.RelativeColumn(2); // Ruc Empresa
                            columns.RelativeColumn(3); // NombreSucursal
                            columns.RelativeColumn(3); // Direccion
                            columns.RelativeColumn(2); // Telefono
                            columns.RelativeColumn(2); // Ciudad
                            columns.RelativeColumn(2); // Responsable
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("RUC").Bold();
                            header.Cell().Text("Nombre").Bold();
                            header.Cell().Text("Dirección").Bold();
                            header.Cell().Text("Telefono").Bold();
                            header.Cell().Text("Ciudad").Bold();
                            header.Cell().Text("Responsable").Bold();
                        });

                        foreach (var emp in empresas)
                        {
                            table.Cell().Text(emp.RucEmpresa);
                            table.Cell().Text(emp.NombreSucursal);
                            table.Cell().Text(emp.Direccion);
                            table.Cell().Text(emp.Telefono);
                            table.Cell().Text(emp.Ciudad);
                            table.Cell().Text(emp.Responsable);
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
