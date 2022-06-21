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
    /// Логика взаимодействия для ChangeStatus.xaml
    /// </summary>
    public partial class ChangeStatus : Window
    {
        DataTable dt1 = new DataTable("Status");
        string id;
        public ChangeStatus(DataRowView drv)
        {
            InitializeComponent();
            CbStatFill();
            cbStatus.Text = drv["Status"].ToString();
            id = drv["id"].ToString();

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
        private void BackForm(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        public void CbStatFill()
        {
            dt1.Clear();
            using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
            {
                if (Saver.idPost == 2)
                {
                    try
                    {
                        connection.Open();
                        string query1 = $@"SELECT * FROM Status WHERE id=1 OR id=2";
                        SqlCommand cmd1 = new SqlCommand(query1, connection);
                        SqlDataAdapter SDA1 = new SqlDataAdapter(cmd1);
                        SDA1.Fill(dt1);
                        cbStatus.ItemsSource = dt1.DefaultView;
                        cbStatus.DisplayMemberPath = "Title";
                        cbStatus.SelectedValuePath = "id";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if(Saver.idPost == 3)
                {
                    try
                    {
                        connection.Open();
                        string query1 = $@"SELECT * FROM Status WHERE id=2 OR id=3";
                        SqlCommand cmd1 = new SqlCommand(query1, connection);
                        SqlDataAdapter SDA1 = new SqlDataAdapter(cmd1);
                        SDA1.Fill(dt1);
                        cbStatus.ItemsSource = dt1.DefaultView;
                        cbStatus.DisplayMemberPath = "Title";
                        cbStatus.SelectedValuePath = "id";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
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

        private void Changebtn_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
            {
                connection.Open();
                int IdStatus;
                bool NamePost = int.TryParse(cbStatus.SelectedValue.ToString(), out IdStatus);
               
                try
                {
                    string query = $@"UPDATE Technic SET Status='{IdStatus}' WHERE id='{id}' ";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Статус изменен успешно!");
                    this.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
