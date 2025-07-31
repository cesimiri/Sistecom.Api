using Identity.Api.DTO;
using OfficeOpenXml;

namespace Identity.Api.Reporteria
{
    public static class UsuarioDetalleExcelGenerator
    {
        public static byte[] GenerarExcel(List<UsuarioDetalleDTO> datos)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Usuarios");

                // Cabeceras
                worksheet.Cells[1, 1].Value = "Cédula";
                worksheet.Cells[1, 2].Value = "Departamento";
                worksheet.Cells[1, 3].Value = "Cargo";
                worksheet.Cells[1, 4].Value = "Nombres";
                worksheet.Cells[1, 5].Value = "Sucursal";
                worksheet.Cells[1, 6].Value = "Estado";

                int row = 2;
                foreach (var emp in datos)
                {
                    worksheet.Cells[row, 1].Value = emp.Cedula;
                    worksheet.Cells[row, 2].Value = emp.NombreDepartamento;
                    worksheet.Cells[row, 3].Value = emp.NombreCargo;
                    worksheet.Cells[row, 4].Value = emp.NombreCedula;
                    worksheet.Cells[row, 5].Value = emp.NombreSucursal;
                    worksheet.Cells[row, 6].Value = emp.Estado;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
    }
}

