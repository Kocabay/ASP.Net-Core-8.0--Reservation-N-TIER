using BusinessLayer.Abstract;
using ClosedXML.Excel;
using DataAccessLayer.Concrete;
using iTextSharp.text.rtf.parser.destinations;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using TraversalCoreProje.Models;

namespace TraversalCoreProje.Controllers
{
    public class ExcelController : Controller
    {
        private readonly IExcelService _excelService;

        public ExcelController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public List<DestinationModel> DestinationList()
        {
            List<DestinationModel> destinationModels = new List<DestinationModel>();
            using (var c = new Context())
            {
                destinationModels = c.Destinations.Select(x => new DestinationModel
                {
                    Capacity = x.Capacity,
                    City = x.DestinationCity,
                    Price = x.Price,
                    DayNight = x.DayNight,

                }).ToList();
            }
            return destinationModels;
        }

        public IActionResult StaticExcelReport()
        {

            return File(_excelService.ExcelList(DestinationList()), "application/vnf.openxmlformats-officedocument.spreadsheetml.sheet","YeniExcel.xlsx");


            //ExcelPackage excel = new ExcelPackage();
            //var workSheet = excel.Workbook.Worksheets.Add("Sayfa1");
            //workSheet.Cells[1, 1].Value = "Rota";
            //workSheet.Cells[1, 2].Value = "Rehber";
            //workSheet.Cells[1, 3].Value = "Kontejan";

            //workSheet.Cells[2, 1].Value = "Gürcistan Batum Turu";
            //workSheet.Cells[2, 2].Value = "Ömer Kocabay";
            //workSheet.Cells[2, 3].Value = "50";
            //workSheet.Cells[3, 1].Value = "Gürcistan Sırbistan Turu";
            //workSheet.Cells[3, 2].Value = "Zeynep Deli";
            //workSheet.Cells[3, 3].Value = "54";

            //var bytes = excel.GetAsByteArray();
            //return File(bytes, "application/vnf.openxmlformats-officedocument.spreadsheetml.sheet", "dosya1.xlsx");
        }


        public IActionResult DestinationExcelReport()
        {
            using (var workbook = new XLWorkbook())
            {
                var workSheet = workbook.Worksheets.Add("Tur Listesi");
                workSheet.Cell(1, 1).Value = "Şehir";
                workSheet.Cell(1, 2).Value = "Konaklama Süresi";
                workSheet.Cell(1, 3).Value = "Fiyat";
                workSheet.Cell(1, 4).Value = "Kapasite";

                int rowCount = 2;
                foreach (var item in DestinationList())
                {
                    workSheet.Cell(rowCount, 1).Value = item.City;
                    workSheet.Cell(rowCount, 1).Value = item.DayNight;
                    workSheet.Cell(rowCount, 1).Value = item.Price;
                    workSheet.Cell(rowCount, 1).Value = item.Capacity;
                    rowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnf.openxmlformats-officedocument.spreadsheetml.sheet", "YeniListe.xlsx");

                }
            }
        }
    }

}
