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
    /// Логика взаимодействия для RequestWindow.xaml
    /// </summary>
    public partial class RequestWindow : Window
    {
        DataTable dt1 = new DataTable("Type");
        public RequestWindow()
        {
            InitializeComponent();
            CbTypes();
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
        private void BackForm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void CbTypes()
        {
            dt1.Clear();
            using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
            {
                try
                {
                    connection.Open();
                    string query1 = $@"SELECT * FROM TechType";
                    SqlCommand cmd1 = new SqlCommand(query1, connection);
                    SqlDataAdapter SDA1 = new SqlDataAdapter(cmd1);
                    SDA1.Fill(dt1);
                    cbTechType.ItemsSource = dt1.DefaultView;
                    cbTechType.DisplayMemberPath = "Title";
                    cbTechType.SelectedValuePath = "id";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CreateReq_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtTitle.Text) || String.IsNullOrEmpty(txtClientPhone.Text) || String.IsNullOrEmpty(txtClientName.Text) || String.IsNullOrEmpty(txtDescription.Text) || cbTechType.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
                {
                    try
                    {
                        connection.Open();
                        string Title = txtTitle.Text;
                        string ClientName = txtClientName.Text;
                        string ClientPhone = txtClientPhone.Text;
                        string Description = txtDescription.Text;
                        string DTransfer = DateTime.Now.ToString("d");
                        string query = $@"SELECT COUNT(*) FROM Technic WHERE TransferDate='{DateTime.Now.ToString("d")}'";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        object result = cmd.ExecuteScalar();
                        int count = Convert.ToInt32(result) + 1;
                        int idType;
                        bool PostName = int.TryParse(cbTechType.SelectedValue.ToString(), out idType);
                        if(idType == 1)
                        {
                            string Code = "SM" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + count;
                            SqlCommand cmd2 = new SqlCommand($@"INSERT INTO Technic (Title, ClientName, ClientPhone, ProblemDescription, Type, TransferDate, PersonalCode, Status) values ('{Title}','{ClientName}','{ClientPhone}','{Description}','{idType}','{DTransfer}','{Code}', '1')", connection);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Заявка успешно сформирована!");
                        }
                        else if(idType == 2)
                        {

                         string Code = "TA" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + count;
                            SqlCommand cmd2 = new SqlCommand($@"INSERT INTO Technic (Title, ClientName, ClientPhone, ProblemDescription, Type, TransferDate, PersonalCode, Status) values ('{Title}','{ClientName}','{ClientPhone}','{Description}','{idType}','{DTransfer}','{Code}', '1')", connection);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Заявка успешно сформирована!");
                        }
                        else if (idType == 3)
                        {
                         string Code = "LT" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + count;
                            SqlCommand cmd2 = new SqlCommand($@"INSERT INTO Technic (Title, ClientName, ClientPhone, ProblemDescription, Type, TransferDate, PersonalCode, Status) values ('{Title}','{ClientName}','{ClientPhone}','{Description}','{idType}','{DTransfer}','{Code}', '1')", connection);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Заявка успешно сформирована!");
                        }
                        else if (idType == 4)
                        {
                         string Code = "FR" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + count;
                            SqlCommand cmd2 = new SqlCommand($@"INSERT INTO Technic (Title, ClientName, ClientPhone, ProblemDescription, Type, TransferDate, PersonalCode, Status) values ('{Title}','{ClientName}','{ClientPhone}','{Description}','{idType}','{DTransfer}','{Code}', '1')", connection);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Заявка успешно сформирована!");
                        }
                        else if (idType == 5)
                        {
                         string Code = "TV" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + count;
                            SqlCommand cmd2 = new SqlCommand($@"INSERT INTO Technic (Title, ClientName, ClientPhone, ProblemDescription, Type, TransferDate, PersonalCode, Status) values ('{Title}','{ClientName}','{ClientPhone}','{Description}','{idType}','{DTransfer}','{Code}', '1')", connection);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Заявка успешно сформирована!");
                        }
                        else if (idType == 6)
                        {
                         string Code = "MO" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + count;
                            SqlCommand cmd2 = new SqlCommand($@"INSERT INTO Technic (Title, ClientName, ClientPhone, ProblemDescription, Type, TransferDate, PersonalCode, Status) values ('{Title}','{ClientName}','{ClientPhone}','{Description}','{idType}','{DTransfer}','{Code}', '1')", connection);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Заявка успешно сформирована!");
                        }
                        else if (idType == 7)
                        {
                         string Code = "AU" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + count;
                            SqlCommand cmd2 = new SqlCommand($@"INSERT INTO Technic (Title, ClientName, ClientPhone, ProblemDescription, Type, TransferDate, PersonalCode, Status) values ('{Title}','{ClientName}','{ClientPhone}','{Description}','{idType}','{DTransfer}','{Code}', '1')", connection);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Заявка успешно сформирована!");
                        }
                        else if (idType == 8)
                        {
                         string Code = "WA" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + count;
                            SqlCommand cmd2 = new SqlCommand($@"INSERT INTO Technic (Title, ClientName, ClientPhone, ProblemDescription, Type, TransferDate, PersonalCode, Status) values ('{Title}','{ClientName}','{ClientPhone}','{Description}','{idType}','{DTransfer}','{Code}', '1')", connection);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Заявка успешно сформирована!");
                        }
                        
                        
                    }

                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ошибка" + ex);
                    }
                    finally
                    {
                        connection.Close();
                        this.Close();
                    }
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
            txtClientPhone.Text = Phone;
        }
        private string replacenumber()
        {
            string num = Regex.Replace(txtClientPhone.Text, @"[^0-9]", "");
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
                txtClientPhone.CaretIndex = Phone.Length + 2;
            }
            else if (Phone.Length <= 7)
            {
                txtClientPhone.CaretIndex = Phone.Length + 3;
            }
            else if (Phone.Length <= 9)
            {
                txtClientPhone.CaretIndex = Phone.Length + 4;
            }
            else if (Phone.Length <= 11)
            {
                txtClientPhone.CaretIndex = Phone.Length + 5;
            }
        }
        private void txtClientPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            changeCaretIndex(replacenumber());
        }

        private void txtClientPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            changeCaretIndex(replacenumber() + e.Text);
            e.Handled = true;
        }

        private void txtClientPhone_GotFocus(object sender, RoutedEventArgs e)
        {
            changeCaretIndex(replacenumber());
        }
    }
}
