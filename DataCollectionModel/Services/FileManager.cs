using DataCollectionModel.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DataCollectionModel.Services
{
    public class FileManager
    {
        public FileManager()
        {

        }

        string _filename;
        public FileManager(string filename)
        {
            _filename = filename;
        }

        public void Save(List<Employee> employes)
        {
            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Employee>));
            using (StreamWriter WriteFileStream = new StreamWriter(_filename))
            {
                SerializerObj.Serialize(WriteFileStream, employes);
            }
        }

        public void Save(List<Department> departments)
        {
            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Department>));
            string path = Directory.GetParent(_filename).FullName + "\\departments.xml";
            using (StreamWriter WriteFileStream = new StreamWriter(path))
            {
                SerializerObj.Serialize(WriteFileStream, departments);
            }
        }

        public List<Employee> LoadEmployees()
        {
            List<Employee> list = new List<Employee>();
            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Employee>));
            using (StreamReader fileStream = new StreamReader(_filename))
            {
                list = SerializerObj.Deserialize(fileStream) as List<Employee>;
            }

            return list;
        }

        public List<Department> LoadDepartments()
        {
            List<Department> list = new List<Department>();
            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Department>));
            string path = Directory.GetParent(_filename).FullName + "\\departments.xml";

            using (StreamReader fileStream = new StreamReader(path))
            {
                list = SerializerObj.Deserialize(fileStream) as List<Department>;
            }
            return list;
        }

        public string CreateExcelFileFromData(string folder)
        {
            string fileName = folder + "\\sample.xlsx";
            var dbManager = new DbManager();
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet ws = excelPackage.Workbook.Worksheets.Add("Demo");
                ws.Cells["A1"].LoadFromText("Имя,Город,Телефон,Отдел,Пол, Награды");
                var coll = dbManager.GetEmployees().Select(x =>
                new
                {
                    Name = x.Name,
                    Address = x.Address,
                    Phone = x.Phone,
                    Department = x.Department.Name,
                    Gender = x.Gender == Gender.Male ? "Муж." : "Жен.",
                    Reward = GetRewardsString(x.Rewards)
                });
                using (ExcelRange rng = ws.Cells["A1:F1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                    rng.Style.Font.Color.SetColor(Color.White);
                }
                ws.Column(6).Width = 100;
                ws.Row(1).Height = 30;
                ws.Column(6).Style.WrapText = true;
                ws.Cells["A2"].LoadFromCollection(coll);
                excelPackage.SaveAs(new FileInfo(fileName));
                return fileName;
            }
        }

        private string GetRewardsString(IEnumerable<Reward> rewards)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var r in rewards)
            {
                builder.Append($"{r.Year}. {r.Name}({r.Description})");
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
