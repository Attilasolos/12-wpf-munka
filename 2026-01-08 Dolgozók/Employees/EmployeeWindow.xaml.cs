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

namespace Employees
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public Employee Employee { get; private set; }

        public EmployeeWindow(Employee employee = null!)
        {
            InitializeComponent();
            if (employee != null)
            {
                txtName.Text = employee.Name;
                dtpBorn.SelectedDate = employee.Born;
                cboDepartment.Text = employee.Department;
                chbHomeOffice.IsChecked = employee.EnableHomeOffice;
                txtPhotoUrl.Text = employee.PhotoUrl;
                txtSalary.Text = employee.Salary.ToString();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) //  (txtName.Text == null || txtName.Text == "")
            {
                MessageBox.Show("Kérem adja meg a nevet");
                txtName.Focus();
                return;
            }
            //TODO további mezők ellenőrzés

            this.Employee = new Employee()
            {
                Name = txtName.Text,
                Born = dtpBorn.SelectedDate,
                Department = cboDepartment.Text,
                EnableHomeOffice = chbHomeOffice.IsChecked == true,
                PhotoUrl = txtPhotoUrl.Text,
                Salary = decimal.Parse(txtSalary.Text)
            };

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
