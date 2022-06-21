using MaterialDesignThemes.Wpf;
using ServiceGnusmas.Class;
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

namespace ServiceGnusmas
{
    /// <summary>
    /// Логика взаимодействия для MenuReceiver.xaml
    /// </summary>
    public partial class MenuReceiver : Window
    {
        public MenuReceiver()
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
        public void DisplayData()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
            {
                try
                {
                    connection.Open();
                    string query = $@"SELECT Technic.id AS id, Technic.Title AS Title, Technic.PersonalCode AS Code, Technic.ClientPhone AS Phone, Technic.TransferDate AS TD, Technic.ClientName AS Name, TechType.Title AS Type, Status.Title AS Status FROM Technic INNER JOIN TechType on Technic.Type = TechType.id INNER JOIN Status on Technic.Status = Status.id  WHERE Status = 1 OR Status = 4";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    DataTable DT = new DataTable("Employee");
                    SqlDataAdapter SDA = new SqlDataAdapter(cmd);
                    SDA.Fill(DT);
                    DGTech.ItemsSource = DT.DefaultView;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }


            }
        }

        private void AddReq_Click(object sender, RoutedEventArgs e)
        {
            RequestWindow request = new RequestWindow();
            request.ShowDialog();
            DisplayData();
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

        private void DGTech_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DGTech.SelectedIndex != -1)
            {
                ChangeStatus change = new ChangeStatus((DataRowView)DGTech.SelectedItem);
                change.Owner = this;
                bool? result = change.ShowDialog();
                switch (result)
                {
                    default:
                        DisplayData();
                        break;
                }
            }
        }
    }
}
