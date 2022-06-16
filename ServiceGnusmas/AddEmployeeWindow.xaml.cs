using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ServiceGnusmas.Class;
using MaterialDesignThemes.Wpf;

namespace ServiceGnusmas
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        DataTable dt1 = new DataTable("Post");
        public AddEmployeeWindow()
        {
            InitializeComponent();
            CbPostFill();
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
        public void CbPostFill()
        {
            dt1.Clear();
            using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
            {
                try
                {
                    connection.Open();
                    string query1 = $@"SELECT * FROM Post";
                    SqlCommand cmd1 = new SqlCommand(query1, connection);
                    SqlDataAdapter SDA1 = new SqlDataAdapter(cmd1);
                    SDA1.Fill(dt1);
                    cbPost.ItemsSource = dt1.DefaultView;
                    cbPost.DisplayMemberPath = "Title";
                    cbPost.SelectedValuePath = "id";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AddEmpl_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsr.Text) || String.IsNullOrEmpty(txtPass.Password) || String.IsNullOrEmpty(txtLN.Text) || String.IsNullOrEmpty(txtFN.Text) || String.IsNullOrEmpty(txtPhone.Text) || cbPost.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtUsr.Text.Length < 3)
            {
                MessageBox.Show(" Логин должен быть больше 2", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtPass.Password.Length < 3)
            {
                MessageBox.Show(" Пароль должен быть больше 2", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
                {

                    connection.Open();
                    int idPost1;
                    bool PostName1 = int.TryParse(cbPost.SelectedValue.ToString(), out idPost1);
                    if (idPost1 == 1 || idPost1 == 2 || idPost1 == 3)
                    {
                        string Login = txtUsr.Text.ToLower();
                        string query = $@"SELECT COUNT(*) FROM Employee WHERE Login = '{Login}'";
                        string query1 = $@"SELECT COUNT(*) FROM Master WHERE Login = '{Login}'";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        SqlCommand cmd1 = new SqlCommand(query1, connection);
                        object result = cmd.ExecuteScalar();
                        object result1 = cmd1.ExecuteScalar();
                        int a = Convert.ToInt32(result);
                        int b = Convert.ToInt32(result1);
                        if (a != 0 || b!=0)
                        {
                            MessageBox.Show("Такой пользователь уже существует!");
                        }
                        else
                        {
                            try
                            {
                                var Pass = SimpleCommand.GetHash(txtPass.Password);
                                string LN = txtLN.Text;
                                string FN = txtFN.Text;
                                string Patr = txtPatr.Text;
                                string Phone = txtPhone.Text;
                                int idPost;
                                bool PostName = int.TryParse(cbPost.SelectedValue.ToString(), out idPost);
                                using (SqlCommand cmd2 = new SqlCommand($@"INSERT INTO Employee (Login, LastName, FirstName, Patronymic, Password,Post, Phone) values ('{Login}','{LN}','{FN}','{Patr}', @binaryValue,'{idPost}','{Phone}')", connection))
                                {

                                    cmd2.Parameters.Add("@binaryValue", SqlDbType.VarBinary).Value = Pass;
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Успешная регистрация!");
                                    this.Close();
                                }
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show("Ошибка" + ex);
                            }
                        }
                    }
                    else
                    {
                        string Login = txtUsr.Text.ToLower();
                        string query = $@"SELECT COUNT(*) FROM Master WHERE Login = '{Login}'";
                        string query1 = $@"SELECT COUNT(*) FROM Employee WHERE Login = '{Login}'";
                        SqlCommand cmd1 = new SqlCommand(query1, connection); 
                        SqlCommand cmd = new SqlCommand(query, connection);
                        object result = cmd.ExecuteScalar();
                        object result1 = cmd1.ExecuteScalar();
                        int a = Convert.ToInt32(result);
                        int b = Convert.ToInt32(result1);
                        if (a != 0 || b!=0)
                        {
                            MessageBox.Show("Такой пользователь уже существует!");
                        }
                        else
                        {
                                try
                                {
                                    var Pass = SimpleCommand.GetHash(txtPass.Password);
                                    string LN = txtLN.Text;
                                    string FN = txtFN.Text;
                                    string Patr = txtPatr.Text;
                                    string Phone = txtPhone.Text;
                                    int idPost;
                                    bool PostName = int.TryParse(cbPost.SelectedValue.ToString(), out idPost);
                                    using (SqlCommand cmd2 = new SqlCommand($@"INSERT INTO Master (Login, LastName, FirstName, Patronymic, Password,Post, Phone) values ('{Login}','{LN}','{FN}','{Patr}', @binaryValue,'{idPost}','{Phone}')", connection))
                                    {

                                        cmd2.Parameters.Add("@binaryValue", SqlDbType.VarBinary).Value = Pass;
                                        cmd2.ExecuteNonQuery();
                                        MessageBox.Show("Успешная регистрация!");
                                        this.Close();
                                    }
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show("Ошибка" + ex);
                                }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BackForm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void PhoneMask(string Phone)
        {
            var newVal = Phone;
            Phone = string.Empty;
            switch (newVal.Length)
            {
                case 1:
                    Phone = Regex.Replace(newVal, @"(\d{1})", "+7(___)___-__-__");
                    break;
                case 2:
                    Phone = Regex.Replace(newVal, @"(\d{1})(\d{0,3})", "+7($2__)___-__-__");
                    break;
                case 3:
                    Phone = Regex.Replace(newVal, @"(\d{1})(\d{0,3})", "+7($2_)___-__-__");
                    break;
                case 4:
                    Phone = Regex.Replace(newVal, @"(\d{1})(\d{0,3})", "+7($2)___-__-__");
                    break;
                case 5:
                    Phone = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})", "+7($2)$3__-__-__");
                    break;
                case 6:
                    Phone = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})", "+7($2)$3_-__-__");
                    break;
                case 7:
                    Phone = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})", "+7($2)$3-__-__");
                    break;
                case 8:
                    Phone = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})", "+7($2)$3-$4_-__");
                    break;
                case 9:
                    Phone = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})", "+7($2)$3-$4-__");
                    break;
                case 10:
                    Phone = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})(\d{0,2})", "+7($2)$3-$4-$5_");
                    break;
                case 11:
                    Phone = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})(\d{0,2})", "+7($2)$3-$4-$5");
                    break;
            }
            txtPhone.Text = Phone;
        }
        private string replacenumber()
        {
            string num = Regex.Replace(txtPhone.Text, @"[^0-9]","");
            return num;
        }
        private void changeCaretIndex(string Phone)
        {
            if (Phone.Length<=11)
            {
                PhoneMask(Phone);
            }
            if (Phone.Length <= 4)
            {
                txtPhone.CaretIndex = Phone.Length + 2;
            }
            else if (Phone.Length <= 7)
            {
                txtPhone.CaretIndex = Phone.Length + 3;
            }
            else if (Phone.Length <= 9)
            {
                txtPhone.CaretIndex = Phone.Length + 4;
            }
            else if (Phone.Length <= 11)
            {
                txtPhone.CaretIndex = Phone.Length + 5;
            }
        }
        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            changeCaretIndex(replacenumber());
        }

        private void txtPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            changeCaretIndex(replacenumber() + e.Text);
            e.Handled = true;
        }

        private void txtPhone_GotFocus(object sender, RoutedEventArgs e)
        {
            changeCaretIndex(replacenumber());
        }


    }
    }
