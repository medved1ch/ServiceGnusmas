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
    /// Логика взаимодействия для EditEmpl.xaml
    /// </summary>
    public partial class EditEmpl : Window
    {
        DataTable dt1 = new DataTable("Post");
        string id;
        public EditEmpl(DataRowView drv)
        {
            InitializeComponent();
            CbPostFill();
            txtLN.Text = drv["LN"].ToString();
            txtFN.Text = drv["FN"].ToString();
            txtPatr.Text = drv["Patr"].ToString();
            txtPhone.Text = drv["Phone"].ToString();
            cbPost.Text = drv["Post"].ToString();
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

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
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
            string num = Regex.Replace(txtPhone.Text, @"[^0-9]", "");
            return num;
        }
        private void changeCaretIndex(string Phone)
        {
            if (Phone.Length <= 11)
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
        public void UpdateEmployee()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
            {
                connection.Open();
                if (String.IsNullOrEmpty(txtLN.Text) || String.IsNullOrEmpty(txtFN.Text) || String.IsNullOrEmpty(txtPhone.Text))
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    int IdPost;
                    bool NamePost = int.TryParse(cbPost.SelectedValue.ToString(), out IdPost);
                    if (IdPost == 1 || IdPost == 2 || IdPost == 3)
                    {
                        string query = $@"UPDATE Employee SET FirstName=@FN, LastName=@LN, Patronymic=@Patr, Phone=@Phone WHERE id='{id}' ";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        try
                        {
                            cmd.Parameters.AddWithValue("@LN", txtLN.Text);
                            cmd.Parameters.AddWithValue("@FN", txtFN.Text);
                            cmd.Parameters.AddWithValue("@Patr", txtPatr.Text);
                            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                            cmd.ExecuteNonQuery();

                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        string query = $@"UPDATE Master SET FirstName=@FN, LastName=@LN, Patronymic=@Patr, Phone=@Phone WHERE id='{id}' ";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        try
                        {
                            cmd.Parameters.AddWithValue("@LN", txtLN.Text);
                            cmd.Parameters.AddWithValue("@FN", txtFN.Text);
                            cmd.Parameters.AddWithValue("@Patr", txtPatr.Text);
                            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                            cmd.ExecuteNonQuery();

                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void Editbtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateEmployee();
            this.Close();
        }
    }
}
