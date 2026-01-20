using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Konyvek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Book> books = new List<Book>();

        public MainWindow()
        {
            InitializeComponent();

            books = Book.ReadFromTxt();

            lblTask1.Content = $"A kiadott könyvek száma: {books.Count}";

            lblTask2.Content = "";

            int max = books[0].Amount;
            foreach (var book in books)
                if (book.Amount > max)
                    max = book.Amount;

            lblTask3.Content = $"A legnagyobb példányszám: {max}";

            Book? first = null!;
            foreach (var book in books)
            {
                if (book.Amount >= 40000 && !book.Hungarian)
                {
                    if (first == null || first.Year * 10 + first.Quarter > book.Year * 10 + book.Quarter)
                    {
                        first = book;
                    }
                }
            }
            lblTask4.Content = $"{first.Year}/{first.Quarter}. {first.AuthorAndTitle}";

            //Statisztika kiszámolva:
            Dictionary<int, int> stat = new Dictionary<int, int>();
            foreach (var book in books)
            {
                if (book.Hungarian)
                {
                    if (stat.ContainsKey(book.Year))
                        stat[book.Year] += book.Amount;
                    else
                        stat.Add(book.Year, book.Amount);
                }
            }

            lstStat.Items.Clear();
            foreach(var item in stat)
            {
                lstStat.Items.Add($"{item.Key}: {item.Value} példány ");
            }

            using (var sw = new StreamWriter("tabla.html"))
            {
                sw.WriteLine("<table>");
                sw.WriteLine("<tr><th>Év</th><th>Példányszám</th></tr>");
                foreach(var item in stat)
                {
                    sw.WriteLine($"<tr><td>{item.Key}</td><td>{item.Value}</td></tr>");
                }
                sw.WriteLine("</table>");
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach(var book in books)
            {
                if (book.AuthorAndTitle.Contains(txtWriter.Text))
                {
                    count++;
                }
            }
            lblTask2.Content = $"{count} könyvkiadás";
        }

        private void txtWriter_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblTask2.Content = "";
        }
    }
}