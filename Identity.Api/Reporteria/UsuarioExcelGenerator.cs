using Identity.Api.DTO;
using OfficeOpenXml;

namespace Identity.Api.Reporteria
{
    public static class UsuarioExcelGenerator
    {
        public static byte[] GenerarExcel(List<UsuarioDTO> datos)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Empresas");

                // Cabeceras
                worksheet.Cells[1, 1].Value = "Cedula";
                worksheet.Cells[1, 2].Value = "Nombres";
                worksheet.Cells[1, 3].Value = "Apellidos";
                worksheet.Cells[1, 4].Value = "Telefono";
                worksheet.Cells[1, 5].Value = "Email";
                worksheet.Cells[1, 6].Value = "Estado";


                int row = 2;
                foreach (var emp in datos)
                {
                    worksheet.Cells[row, 1].Value = emp.Cedula;
                    worksheet.Cells[row, 2].Value = emp.Nombres;
                    worksheet.Cells[row, 3].Value = emp.Apellidos;
                    worksheet.Cells[row, 4].Value = emp.Telefono;
                    worksheet.Cells[row, 5].Value = emp.Email;
                    worksheet.Cells[row, 4].Value = emp.Estado;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
    }
}
