using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace LongJump
{
    internal class Competitor
    {
        public string Name { get; set; } = "";
        public List<double?> Attempts { get; set; } = new List<double?>();

        public static List<Competitor> LoadFromJson(string fileName = "longjump.json")
        {
            var option = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<List<Competitor>>(json, option) ?? new List<Competitor>();
            }
            else
            {
                return new List<Competitor>();
            }
        }
        public static void SaveToJson(List<Competitor> list, string filenName = "longjump.json")
        {
            var option = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(list, option);
            System.IO.File.WriteAllText(filenName, json);
        }
    }
}
