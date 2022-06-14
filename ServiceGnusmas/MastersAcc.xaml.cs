using ServiceGnusmas.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using MaterialDesignThemes.Wpf;

namespace ServiceGnusmas
{
    /// <summary>
    /// Логика взаимодействия для MastersAcc.xaml
    /// </summary>
    public partial class MastersAcc : Window
    {
        public MastersAcc()
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
                    string query = $@"SELECT Master.id, Master.LastName AS LN,  Master.FirstName AS FN,  Master.Patronymic AS Patr, Post.Title AS Post, Master.Phone AS Phone FROM Master INNER JOIN Post on Master.Post = Post.id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    DataTable DT = new DataTable("Master");
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


    }
}
