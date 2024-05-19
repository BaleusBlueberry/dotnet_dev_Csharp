using System.Xml;

namespace json_converter;

using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        string htmlData = File.ReadAllText("../../../data.txt");

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(htmlData);

        var rows = htmlDocument.DocumentNode.SelectNodes("//table/tbody/tr");

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

        foreach (var row in rows)
        {
            var cells = row.SelectNodes("td");
            if (cells != null && cells.Count == 8)
            {
                var rowData = new Dictionary<string, string>
                {
                    { "Level", cells[0].InnerText.Trim() },
                    { "DPS", cells[1].InnerText.Trim() },
                    { "DPH", cells[2].InnerText.Trim() },
                    { "HP", cells[3].InnerText.Trim() },
                    { "Cost", cells[4].InnerText.Trim() },
                    { "Time", cells[5].InnerText.Trim() },
                    { "Experience", cells[6].InnerText.Trim() },
                    { "Laboratory Level Required", cells[7].InnerText.Trim() },
                    {"requiredLevel", 2},
                    { "picture", "" }
                };
                data.Add(rowData);
            }
        }

        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        Console.WriteLine(jsonData);
    }
}
