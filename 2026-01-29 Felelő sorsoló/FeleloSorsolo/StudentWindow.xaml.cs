using Microsoft.Win32;
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

namespace FeleloSorsolo
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public Student Student { get; private set; }

        public StudentWindow(Student student)
        {
            InitializeComponent();
            txtName.Text = student.Name;
            txtPhotoPath.Text = student.PhotoUrl;
        }

        private void imgBrowse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                CheckFileExists = true,
                FileName = txtPhotoPath.Text
            };
            if (dlg.ShowDialog() == true)
            {
                txtPhotoPath.Text = dlg.FileName;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtPhotoPath.Text == "")
                return;

            this.Student = new Student()
            {
                Name = txtName.Text,
                PhotoUrl = txtPhotoPath.Text,
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
