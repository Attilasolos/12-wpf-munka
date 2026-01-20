using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konyvek
{
    public class Book
    {
        public int Year { get; set; }
        public int Quarter { get; set; }
        public bool Hungarian { get; set; }
        public string AuthorAndTitle { get; set; } = "";
        public int Amount { get; set; }

        public static List<Book> ReadFromTxt(string fileName = "kiadas.txt")
        {
            var list = new List<Book>();
            using (var sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    var row = sr.ReadLine()!.Split(';');
                    var book = new Book()
                    {
                        Year = int.Parse(row[0]),
                        Quarter = int.Parse(row[1]),
                        Hungarian = row[2] == "ma",
                        AuthorAndTitle = row[3],
                        Amount = int.Parse(row[4])
                    };
                    list.Add(book);
                }
            }
            return list;
        }
    }
}
