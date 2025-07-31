using Identity.Api.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Identity.Api.Reporteria
{
    public static class UsuarioPdfGenerator
    {
        public static byte[] GenerarPdf(List<UsuarioDTO> empresas, string? correo)
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
                            col.Item().AlignCenter().Text("Listado de Usuarios")
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
                            columns.RelativeColumn(2); // Cedula
                            columns.RelativeColumn(3); // Nombres
                            columns.RelativeColumn(3); // Apellidos
                            columns.RelativeColumn(2); // Telefono
                            columns.RelativeColumn(2); // Email
                            columns.RelativeColumn(2); // Estado
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Cedula").Bold();
                            header.Cell().Text("Nombres").Bold();
                            header.Cell().Text("Apellidos").Bold();
                            header.Cell().Text("Telefono").Bold();
                            header.Cell().Text("Email").Bold();
                            header.Cell().Text("Estado").Bold();
                        });

                        foreach (var emp in empresas)
                        {
                            table.Cell().Text(emp.Cedula);
                            table.Cell().Text(emp.Nombres);
                            table.Cell().Text(emp.Apellidos);
                            table.Cell().Text(emp.Telefono);
                            table.Cell().Text(emp.Email);
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
