using Identity.Api.DTO;
using OfficeOpenXml;

namespace Identity.Api.Reporteria
{
    public static class SuscripcioneExcelGenerator
    {
        public static byte[] GenerarExcel(List<SuscripcionDto> datos)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Empresas");

                // Cabeceras
                worksheet.Cells[1, 1].Value = "Nombre Servicio";
                worksheet.Cells[1, 2].Value = "Nombre Proveedor";
                worksheet.Cells[1, 3].Value = "Fecha Inicio";
                worksheet.Cells[1, 4].Value = "Fecha Renovación";
                worksheet.Cells[1, 5].Value = "Costo Periodo";
                worksheet.Cells[1, 6].Value = "Usuario Incluidos";
                worksheet.Cells[1, 7].Value = "Tipo";
                worksheet.Cells[1, 8].Value = "Período de facturación";
                worksheet.Cells[1, 9].Value = "URL Acceso";


                int row = 2;
                foreach (var emp in datos)
                {
                    worksheet.Cells[row, 1].Value = emp.NombreServicio;
                    worksheet.Cells[row, 2].Value = emp.RazonSocialProveedor;
                    worksheet.Cells[row, 3].Value = emp.FechaInicio;
                    worksheet.Cells[row, 3].Style.Numberformat.Format = "dd/MM/yyyy";

                    worksheet.Cells[row, 4].Value = emp.FechaRenovacion;
                    worksheet.Cells[row, 4].Style.Numberformat.Format = "dd/MM/yyyy";
                    worksheet.Cells[row, 5].Value = emp.CostoPeriodo;
                    worksheet.Cells[row, 6].Value = emp.UsuariosIncluidos;
                    worksheet.Cells[row, 7].Value = emp.TipoSuscripcion;
                    worksheet.Cells[row, 8].Value = emp.PeriodoFacturacion;
                    worksheet.Cells[row, 9].Value = emp.UrlAcceso;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
    }
}
