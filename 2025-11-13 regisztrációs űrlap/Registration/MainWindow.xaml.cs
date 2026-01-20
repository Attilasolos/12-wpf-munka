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

namespace Registration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            var errorMsg = "";
            if (txtName.Text.Trim() == "")
            {
                errorMsg += "Kérem adja meg a nevét! ";
                txtName.BorderBrush = Brushes.Red;
            }
            if (pwdPassword.Password.Trim() == "")
            {
                errorMsg += "Kérem adja meg a jelszavát! ";
            }
            if (dtpBorn.SelectedDate == null)
            {
                errorMsg += "Kérem adja meg a születési dátumát! ";
            }
            else
            {
                var diff = DateTime.Today.Year - ((DateTime)dtpBorn.SelectedDate).Year;
                if (diff < 18)
                {
                    errorMsg += "Csak nagykorúak regisztrálhatnak";
                }
                else if (diff > 120)
                {
                    errorMsg += "Hibás születési dátum";
                }
            }
            if (rbMale.IsChecked == false && rbFemale.IsChecked == false)
            {
                errorMsg += "Kérem válassza ki a nemét!";
            }

            if (errorMsg == "")
            {
                //TODO: mentés
                MessageBox.Show("Sikeres regisztráció");
                txtName.Text = "";
                pwdPassword.Password = "";
                dtpBorn.SelectedDate = null;
                rbFemale.IsChecked = false;
                rbMale.IsChecked = false;
                lblErrorMsg.Content = "";
            }
            else
            {
                lblErrorMsg.Content = errorMsg;
            }

        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtName.BorderBrush = Brushes.Gray;
            lblErrorMsg.Content = "";
        }
    }
}