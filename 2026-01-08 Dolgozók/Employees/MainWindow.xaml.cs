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
using System.Text.Json;
using System.ComponentModel;
using Microsoft.Win32;
using System.Reflection;

namespace Employees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<Employee> employees = new BindingList<Employee>();
        private string fileName;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                CheckFileExists = true,
                DefaultExt = "json",
                Title = "Json fájl megnyitása",
                DefaultDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            };
            if (dlg.ShowDialog() == true ) 
            {
                this.fileName = dlg.FileName;

                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };
                var json = File.ReadAllText(this.fileName);
                var list = JsonSerializer.Deserialize<List<Employee>>(json, options) ?? new List<Employee>();
                this.employees = new BindingList<Employee>(list);
                /* Newtonsoft
                 
                    if (JsonSerializer.Deserialize<List<Employee>>(json) == null) 
                        this.employees = new List<Employee>();
                    else 
                        this.employees = JsonSerializer.Deserialize<List<Employee>>(json);
                */
            }
            else
            {
                this.Close();
            }
            dgEmployees.ItemsSource = employees;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            var employeeWindow = new EmployeeWindow();
            if (employeeWindow.ShowDialog() == true)
            {
                this.employees.Add(employeeWindow.Employee);
                SaveToJson();
            }
            ;
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedValue != null)
            {
                var employee = (Employee)dgEmployees.SelectedValue;
                var employeeWindow = new EmployeeWindow(employee);
                if (employeeWindow.ShowDialog() == true)
                {
                    var index = employees.IndexOf(employee);
                    employees[index] = employeeWindow.Employee;
                    SaveToJson();
                }
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedValue != null)
            {
                var employee = (Employee)dgEmployees.SelectedValue;
                var msg = $"Biztos törölni szeretné a(z) {employee.Name} (szül.: {employee.Born:yyyy-MM-dd})nevű dolgozót?";
                if (MessageBox.Show(msg, "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    employees.Remove(employee);
                    SaveToJson();
                }
            }
        }

        private void SaveToJson()
        {
            var json = JsonSerializer.Serialize(this.employees);
            File.WriteAllText(this.fileName, json);
        }


    }
}