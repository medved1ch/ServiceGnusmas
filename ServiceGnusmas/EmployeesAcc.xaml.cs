using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
using MaterialDesignThemes.Wpf;
using ServiceGnusmas.Class;

namespace ServiceGnusmas
{
    /// <summary>
    /// Логика взаимодействия для EmployeesAcc.xaml
    /// </summary>
    public partial class EmployeesAcc : Window
    {
        public EmployeesAcc()
        {
            InitializeComponent();
            DisplayData();
        }
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }
        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            try
            {
                base.OnMouseLeftButtonDown(e);
                DragMove();
            }
            catch
            {

            }
        }

        private void AddReq_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackForm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void DisplayData()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
            {
                try
                {
                    connection.Open();
                    string query = $@"SELECT Employee.id, Employee.LastName AS LN,  Employee.FirstName AS FN,  
                    Employee.Patronymic AS Patr, Post.Title AS Post, Employee.Phone AS Phone FROM Employee INNER JOIN Post on Employee.Post = Post.id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    DataTable DT = new DataTable("Employee");
                    SqlDataAdapter SDA = new SqlDataAdapter(cmd);
                    SDA.Fill(DT);
                    DGEmpl.ItemsSource = DT.DefaultView;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }


            }
        }

        private void AccMastbtn_Click(object sender, RoutedEventArgs e)
        {
            MastersAcc masters = new MastersAcc();
            masters.Show();
            this.Hide();
        }

        private void AddEmpl_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployee = new AddEmployeeWindow();
            addEmployee.ShowDialog();
        }

        private void DGEmpl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGEmpl.SelectedIndex != -1)
            {
                EditEmpl editEmpl = new EditEmpl((DataRowView)DGEmpl.SelectedItem);
                editEmpl.Owner = this;
                bool? result = editEmpl.ShowDialog();
                switch (result)
                {
                    default:
                        DisplayData();
                        break;
                }
            }
        }
        private void Helpbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string relPath = @"ServiceHelper.chm";
                string fullPath = System.IO.Path.Combine(path, relPath);
                System.Diagnostics.Process.Start($@"{fullPath}");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

    }
}
