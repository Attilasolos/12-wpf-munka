using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LongJump
{
    /// <summary>
    /// Interaction logic for Entrieswindow.xaml
    /// </summary>
    public partial class Entrieswindow : Window
    {
        private List<Competitor> names = new List<Competitor>();
        public Entrieswindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            names = Competitor.LoadFromJson();
            lstNames.ItemsSource = names;
            lstNames.DisplayMemberPath = "Name";
        }

        private void LabelOk_Click(object sender, MouseButtonEventArgs e)
        {
            if(lstNames.SelectedItem == null)
            {
                names.Add(new Competitor() { Name = txtName.Text});
               
            }
            else
            {
                Competitor selected = (Competitor)lstNames.SelectedItem;
                var index = names.IndexOf(selected);
                names[index].Name = txtName.Text;
                
            }
            Competitor.SaveToJson(names);
            lstNames.Items.Refresh();
            PanelButtons.Visibility = Visibility.Visible;
            panelEdit.Visibility = Visibility.Collapsed;
        }

        private void LabelCancel_Click(object sender, MouseButtonEventArgs e)
        {
            PanelButtons.Visibility = Visibility.Visible;
            panelEdit.Visibility = Visibility.Collapsed;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            PanelButtons.Visibility = Visibility.Collapsed;
            panelEdit.Visibility = Visibility.Visible;
            txtName.Text = "";
            lstNames.SelectedItem = null;
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            if(lstNames.SelectedItem != null)
            {
                Competitor selected = (Competitor)lstNames.SelectedItem;
                PanelButtons.Visibility = Visibility.Hidden;
                panelEdit.Visibility = Visibility.Visible;
                txtName.Text = selected.Name;
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if(lstNames.SelectedItem != null)
            {
                Competitor selected = (Competitor)lstNames.SelectedItem;
                var msg = $"Biztosan szeretnéd {selected}-t kitörölni?";
                if (MessageBox.Show(msg, "Kérdés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) 
                {
                    names.Remove(selected);
                    Competitor.SaveToJson(names);
                    lstNames.Items.Refresh();
                }
            }
        }
    }
}
