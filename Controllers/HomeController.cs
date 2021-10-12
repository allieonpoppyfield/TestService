using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TestService.Models;
using TestService.Service;

namespace TestService.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInitialData data;

        public HomeController(IInitialData _data)
        {
            data = _data;
        }

        public async Task<IActionResult> GetBalances(Request request)
        {
            if (!ModelState.IsValid) return View("Index");
            var conductanceList = await data.GetConductantList(request.AccountId.Value, request.PeriodType.Value);

            var jsonObject = new { Conductance = conductanceList };
            var json = JsonConvert.SerializeObject(jsonObject);

            if (HttpContext.Request.ContentType?.ToLower() == "application/csv")
            {
                return Ok(ToCsv(json));
            }
            else if (HttpContext.Request.ContentType?.ToLower() == "application/xml")
            {
                XNode node = JsonConvert.DeserializeXNode(json, "Result");
                return Ok(node.ToString());
            }

            return Ok(json);
        }

        public IActionResult Index() => View();

        private List<string> ToCsv(string jsonContent)
        {
            XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + jsonContent + "}}");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml.InnerXml);
            XmlReader xmlReader = new XmlNodeReader(xml);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlReader);
            var dataTable = dataSet.Tables[1];

            var lines = new List<string>();
            string[] columnNames = dataTable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            var header = string.Join(",", columnNames);
            lines.Add(header);
            var valueLines = dataTable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);
            return lines;
        }
    }
}
