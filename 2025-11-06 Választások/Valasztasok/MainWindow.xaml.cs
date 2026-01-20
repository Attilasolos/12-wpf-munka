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

namespace Valasztasok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Candidate> candidates = new List<Candidate>();

        public MainWindow()
        {
            InitializeComponent();
            lbl3.Content = "";
            lbl7.Content = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var sr = new StreamReader("szavazatok.txt"))
                while (!sr.EndOfStream)
                    candidates.Add(new Candidate(sr.ReadLine()!));

            lbl2.Content = $"2. feladat: A helyhatósági választáson {candidates.Count} képviselőjelölt indult.";

            var sumOfVotes = 0;
            foreach(var candidate in candidates)
                sumOfVotes += candidate.Votes;
            lbl4.Content = $"4. feladat: A választáson {sumOfVotes} szavazó, " +
                           $"a jogosultak {(100.0 * sumOfVotes) / 12345:f2}%-a vett részt.";

            var stat = new Dictionary<string, int>();
            foreach(var candidate in candidates)
            {
                if (stat.ContainsKey(candidate.Party))
                    stat[candidate.Party] += candidate.Votes;
                else
                    //stat.Add(candidate.Party, candidate.Votes);
                    stat[candidate.Party] = candidate.Votes;
            }
            foreach(var item in stat)
            {
                lst6.Items.Add($"{item.Key}: {(100.0 * item.Value) / sumOfVotes:f2}%");

                cboParty.Items.Add(item.Key);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var i = 0;
            while (i < candidates.Count && candidates[i].Name.ToLower() != txtName.Text.ToLower())
                i++;
            if (i < candidates.Count)
            {
                lbl3.Content = $"A jelölt a {candidates[i].District}-s számú körzetben indult\n" +
                               $"A szavazatok száma: {candidates[i].Votes}";
            }
            else
            {
                lbl3.Content = "Ilyen nevű képviselőjelölt\nnem szerepel a nyilvántartásban!";
            }
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl3.Content = "";
        }

        private void cboParty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string party = cboParty.SelectedItem.ToString() ?? "";
            Candidate best = null!;
            foreach (var candidate in candidates)
            {
                if (candidate.Party == party)
                {
                    if (best == null || best.Votes < candidate.Votes)
                    {
                        best = candidate;
                    }
                }
            }
            lbl7.Content = $"A párt legjobban szereplő jelöltje: {best.Name}";

        }
    }
}