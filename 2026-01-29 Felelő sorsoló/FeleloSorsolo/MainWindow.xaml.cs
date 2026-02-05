using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FeleloSorsolo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Student> students = new List<Student>();
        public MainWindow()
        {
            InitializeComponent();
            lblName.Content = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.students = Student.LoadFromJson();
            lstNames.ItemsSource = students;
            lstNames.DisplayMemberPath = "Name";
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new StudentWindow(new Student());
            if (wnd.ShowDialog() == true)
            {
                students.Add(wnd.Student);
                Student.SaveToJson(students);
                lstNames.Items.Refresh();
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            if (lstNames.SelectedItem != null)
            {
                var student = (Student)lstNames.SelectedItem;
                var wnd = new StudentWindow(student);
                if (wnd.ShowDialog() == true)
                {
                    var index = students.IndexOf(student);
                    students[index] = wnd.Student;
                    Student.SaveToJson(students);
                    lstNames.Items.Refresh();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstNames.SelectedItem != null)
            {
                var student = (Student)lstNames.SelectedItem;
                students.Remove(student);
                Student.SaveToJson(students);
                lstNames.Items.Refresh();
            }
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            if (students.Count > 0)
            {
                var rand = new Random();
                var index = rand.Next(students.Count);
                
                lblName.Content = students[index].Name;
                imgPhoto.Source = new BitmapImage(new Uri(students[index].PhotoUrl));                
            }
        }
    }
}