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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using ServiceGnusmas.Class;

namespace ServiceGnusmas
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if(IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
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

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsr.Text) || String.IsNullOrEmpty(txtPass.Password))
            {
                MessageBox.Show("Заполните поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txtUsr.BorderBrush = Brushes.Red;
                txtPass.BorderBrush = Brushes.Red; 
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
                    try
                    {

                        connection.Open();
                        string Login = txtUsr.Text.ToLower();
                        var Pass = SimpleCommand.GetHash(txtPass.Password);

                        using (SqlCommand cmd1 = new SqlCommand($@"SELECT  COUNT(*) FROM Employee WHERE Login='{Login}' AND Password=@binaryValue", connection))
                        {

                            cmd1.Parameters.Add("@binaryValue", SqlDbType.VarBinary).Value = Pass;
                            int count = Convert.ToInt32(cmd1.ExecuteScalar());
                            if (count == 1)
                            {
                                string query3 = $@"SELECT Post FROM Employee WHERE Login='{Login}'";
                                SqlCommand cmd3 = new SqlCommand(query3, connection);
                                int postID = Convert.ToInt32(cmd3.ExecuteScalar());
                                if (postID == 1)
                                {
                                    Saver.idPost = 1;
                                    string query2 = $@"SELECT id FROM Employee WHERE Login='{Login}' AND Post ='1'";
                                    SqlCommand cmd2 = new SqlCommand(query2, connection);
                                    int ID = Convert.ToInt32(cmd2.ExecuteScalar());
                                    Saver.idEmpl = ID;
                                    string query4 = $@"SELECT (LastName + ' ' + FirstName) AS FIO FROM Employee WHERE id = '{ID}'";
                                    SqlCommand cmd4 = new SqlCommand(query4, connection);
                                    string FIO = Convert.ToString(cmd4.ExecuteScalar());
                                    MessageBox.Show("Добро пожаловать " + $@"{FIO}" + "!");
                                    ManagerWindow menu = new ManagerWindow();
                                    menu.Show();
                                    this.Close();
                                }
                                else if(postID == 2)
                                {
                                    Saver.idPost = 2;
                                    string query2 = $@"SELECT id FROM Employee WHERE Login='{Login}' AND Post ='2'";
                                    SqlCommand cmd2 = new SqlCommand(query2, connection);
                                    int ID = Convert.ToInt32(cmd2.ExecuteScalar());
                                    Saver.idEmpl = ID;
                                    string query4 = $@"SELECT (LastName + ' ' + FirstName) AS FIO FROM Employee WHERE id = '{ID}'";
                                    SqlCommand cmd4 = new SqlCommand(query4, connection);
                                    string FIO = Convert.ToString(cmd4.ExecuteScalar());
                                    MessageBox.Show("Добро пожаловать " + $@"{FIO}" + "!");
                                    MenuReceiver receiver = new MenuReceiver();
                                    receiver.Show();
                                    this.Close();
                                }
                                else if (postID == 3)
                                {
                                    Saver.idPost = 3;
                                    string query2 = $@"SELECT id FROM Employee WHERE Login='{Login}' AND Post ='3'";
                                    SqlCommand cmd2 = new SqlCommand(query2, connection);
                                    int ID = Convert.ToInt32(cmd2.ExecuteScalar());
                                    Saver.idEmpl = ID;
                                    string query4 = $@"SELECT (LastName + ' ' + FirstName) AS FIO FROM Employee WHERE id = '{ID}'";
                                    SqlCommand cmd4 = new SqlCommand(query4, connection);
                                    string FIO = Convert.ToString(cmd4.ExecuteScalar());
                                    MessageBox.Show("Добро пожаловать " + $@"{FIO}" + "!");
                                    WarehouseWindow whouse = new WarehouseWindow();
                                    whouse.Show();
                                    this.Close();
                                }

                            }
                            else if(count == 0)
                            {
                                var Pass1 = SimpleCommand.GetHash(txtPass.Password);
                                string Login1 = txtUsr.Text.ToLower();
                                using (SqlCommand cmd2 = new SqlCommand($@"SELECT  COUNT(*) FROM Master WHERE Login='{Login1}' AND Password=@binaryValue1", connection))
                                {
                                    cmd2.Parameters.Add("@binaryValue1", SqlDbType.VarBinary).Value = Pass1;
                                    int count1 = Convert.ToInt32(cmd2.ExecuteScalar());
                                    if (count1 == 1)
                                    {
                                        string query3 = $@"SELECT Post FROM Master WHERE Login='{Login1}'";
                                        SqlCommand cmd3 = new SqlCommand(query3, connection);
                                        int postID = Convert.ToInt32(cmd3.ExecuteScalar());
                                        if (postID == 4)
                                        {
                                            Saver.idPost = 4;
                                            string query2 = $@"SELECT id FROM Master WHERE Login='{Login1}' AND Post ='4'";
                                            SqlCommand cmd5 = new SqlCommand(query2, connection);
                                            int ID = Convert.ToInt32(cmd5.ExecuteScalar());
                                            Saver.idMaster = ID;
                                            string query4 = $@"SELECT (LastName + ' ' + FirstName) AS FIO FROM Master WHERE id = '{ID}'";
                                            SqlCommand cmd4 = new SqlCommand(query4, connection);
                                            string FIO = Convert.ToString(cmd4.ExecuteScalar());
                                            MessageBox.Show("Добро пожаловать " + $@"{FIO}" + "!");
                                            MasterWindow menu = new MasterWindow();
                                            menu.Show();
                                            this.Close();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Неверное имя пользователя или пароль");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неверное имя пользователя или пароль");
                            }
                        }

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
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
