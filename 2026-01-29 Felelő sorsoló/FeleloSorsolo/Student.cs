using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace FeleloSorsolo
{
    public class Student
    {
        public string Name { get; set; } = "";

        public string PhotoUrl { get; set; } = "";

        public static void SaveToJson(List<Student> students, string fileName = "students.json")
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize(students, options);
            File.WriteAllText(fileName, json);
        }

        public static List<Student> LoadFromJson(string fileName = "students.json")
        {
            if (!File.Exists(fileName))
                return new List<Student>();

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<Student>>(json, options) ?? new List<Student>();

        }
    }
}
