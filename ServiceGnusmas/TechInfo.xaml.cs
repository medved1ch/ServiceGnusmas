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
    /// Логика взаимодействия для TechInfo.xaml
    /// </summary>
    public partial class TechInfo : Window
    {
        string id;
        DataTable dt1 = new DataTable("Type");
        DataTable dt2 = new DataTable("Status");
        DataTable dt3 = new DataTable("Master");
        
        public TechInfo(DataRowView drv)
        {

            InitializeComponent();
            CbTypes();
            CbMasters();
            CbStat();
            txtTitle.Text = drv["Title"].ToString();
            txtClientName.Text = drv["Name"].ToString();
            txtTransferDate.Text = drv["TD"].ToString();
            txtClientPhone.Text = drv["Phone"].ToString();
            cbMaster.Text = drv["Master"].ToString();
            cbTechType.Text = drv["Type"].ToString();
            cbStatus.Text = drv["Status"].ToString();
            txtBdType.Text = drv["BreakDownType"].ToString();
            txtPC.Text = drv["Code"].ToString();
            txtDescription.Text = drv["PD"].ToString();
            txtWT.Text = drv["WT"].ToString();
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
        private void BackForm(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        public void Update()
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(ConnectionDB.conn))
                {
                    connection.Open();
                    int IdMaster, IdStatus;
                    bool NameMaster = int.TryParse(cbMaster.SelectedValue.ToString(), out IdMaster);
                    bool NameStatus = int.TryParse(cbStatus.SelectedValue.ToString(), out IdStatus);
                    string query = $@"UPDATE Technic SET BreakdownType=@BD, WorkTime=@WT, Master='{IdMaster}', Status='{IdStatus}' WHERE id='{id}' ";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    try
                    {
                        cmd.Parameters.AddWithValue("@BD", txtBdType.Text);
                        cmd.Parameters.AddWithValue("@WT", txtWT.Text);
                        cmd.ExecuteNonQuery();

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            catch
            {

            }
        } 
        private void Okbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Update();
                MessageBox.Show("Данные успешно изменены");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
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
        public void CbStat()
        {
            dt2.Clear();
            using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
            {
                try
                {
                    connection.Open();
                    string query2 = $@"SELECT * FROM Status";
                    SqlCommand cmd2 = new SqlCommand(query2, connection);
                    SqlDataAdapter SDA1 = new SqlDataAdapter(cmd2);
                    SDA1.Fill(dt2);
                    cbStatus.ItemsSource = dt2.DefaultView;
                    cbStatus.DisplayMemberPath = "Title";
                    cbStatus.SelectedValuePath = "id";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void CbMasters()
        {
            dt3.Clear();
            using (SqlConnection connection = new SqlConnection(ConnectionDB.conn))
            {
                try
                {
                    connection.Open();
                    string query1 = $@"SELECT id, (LastName + ' ' + FirstName) AS FIO FROM Master";
                    SqlCommand cmd1 = new SqlCommand(query1, connection);
                    SqlDataAdapter SDA1 = new SqlDataAdapter(cmd1);
                    SDA1.Fill(dt3);
                    cbMaster.ItemsSource = dt3.DefaultView;
                    cbMaster.DisplayMemberPath = "FIO";
                    cbMaster.SelectedValuePath = "id";
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
