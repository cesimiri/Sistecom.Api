using Identity.Api.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Identity.Api.Reporteria
{
    public static class UsuarioDetallePdfGenerator
    {
        public static byte[] GenerarPdf(List<UsuarioDetalleDTO> empresas, string? correo)
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
                            col.Item().AlignCenter().Text("Listado de Empresas")
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
                            columns.RelativeColumn(2); // 
                            columns.RelativeColumn(3); // 
                            columns.RelativeColumn(3); // 
                            columns.RelativeColumn(4); //nombres
                            columns.RelativeColumn(2); // Sucursal
                            columns.RelativeColumn(3); // Estado
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Cedula").Bold();
                            header.Cell().Text("Departamento").Bold();
                            header.Cell().Text("Cargo").Bold();
                            header.Cell().Text("Nombres").Bold();
                            header.Cell().Text("Sucursal").Bold();
                            header.Cell().Text("Estado").Bold();
                        });

                        foreach (var emp in empresas)
                        {
                            table.Cell().Text(emp.Cedula);
                            table.Cell().Text(emp.NombreDepartamento);
                            table.Cell().Text(emp.NombreCargo);
                            table.Cell().Text(emp.NombreCedula);
                            table.Cell().Text(emp.NombreSucursal);
                            table.Cell().Text(emp.Estado);
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
