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

namespace Szavazogep
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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var sr = new StreamReader("szavazatok.txt"))
                while (!sr.EndOfStream)
                    candidates.Add(new Candidate(sr.ReadLine()!));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            var d = Convert.ToInt32(rb.Content);
            lstNames.Items.Clear();
            foreach(var candidate in candidates)
            {
                if (candidate.District == d)
                    lstNames.Items.Add($"{candidate.Name} - {candidate.Party}");
            }

        }

        private void btnVote_Click(object sender, RoutedEventArgs e)
        {
            if (lstNames.SelectedItem != null)
            {
                using(var sw = new StreamWriter("votes.log", true))
                {
                    sw.WriteLine(lstNames.SelectedItem);
                }
                MessageBox.Show("KÖszönjük a szavazatát!");
                this.Close();
            }
        }
    }
}