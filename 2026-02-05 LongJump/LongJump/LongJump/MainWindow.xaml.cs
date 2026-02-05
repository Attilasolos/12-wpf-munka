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

namespace LongJump
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Competitor> names = new List<Competitor>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                var wnd = new Entrieswindow();
                wnd.ShowDialog();
                names = Competitor.LoadFromJson();
                grdResults.ItemsSource = names;
                cboCompetitor.ItemsSource = names;
                cboCompetitor.Items.Refresh();


            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            names = Competitor.LoadFromJson();
            grdResults.ItemsSource = names;
            cboCompetitor.ItemsSource = names;
            cboCompetitor.DisplayMemberPath = "Name";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(cboCompetitor.SelectedItem != null)
            {
                var selected = (Competitor)cboCompetitor.SelectedItem;
                try
                {
                    selected.Attempts.Add(Convert.ToDouble(txtdistance.Text));
                }
                catch 
                {
                    selected.Attempts.Add(null);
                }
                Competitor.SaveToJson(names);
                grdResults.Items.Refresh();

                txtdistance.Text = "";
                LblnextAttempt.Content = "";
                cboCompetitor.SelectedIndex = -1;
            }
        }

        private void cboCompetitor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cboCompetitor.SelectedItem != null)
            {
                var selected = (Competitor)cboCompetitor.SelectedItem;
                LblnextAttempt.Content = $"{selected.Attempts.Count + 1}";
            }
        }
    }
}