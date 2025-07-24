using Modelo.Sistecom.Modelo.Database;
using OfficeOpenXml;

namespace Identity.Api.Reporteria
{
    public static class EmpresaExcelGenerator
    {
        public static byte[] GenerarExcel(List<EmpresasCliente> datos)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Empresas");

                // Cabeceras
                worksheet.Cells[1, 1].Value = "RUC";
                worksheet.Cells[1, 2].Value = "TipoCliente";
                worksheet.Cells[1, 3].Value = "Dirección";
                worksheet.Cells[1, 4].Value = "Estado";
                //worksheet.Cells[1, 5].Value = "Teléfono";

                int row = 2;
                foreach (var emp in datos)
                {
                    worksheet.Cells[row, 1].Value = emp.Ruc;
                    worksheet.Cells[row, 2].Value = emp.RazonSocial;
                    worksheet.Cells[row, 3].Value = emp.TipoCliente;
                    worksheet.Cells[row, 4].Value = emp.Estado;
                    //worksheet.Cells[row, 5].Value = emp.Telefono;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
    }
}
