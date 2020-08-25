using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp3.FolderForWindows
{
    /// <summary>
    /// Логика взаимодействия для AddNewUser.xaml
    /// </summary>
    public partial class AddNewUser : Window
    {
        shiaDBEntities db = new shiaDBEntities();
        OperationWithBDClass OwBDC = new OperationWithBDClass();
        public AddNewUser()
        {
            InitializeComponent();
            Register();
            PpTxt.Visibility = Visibility.Hidden;
            popup1.IsOpen = false;
        }
        public int RandomToPassword()
        {
            Random rand = new Random();
            return rand.Next(100, 999);
        }

        public void Register()
        {
            ObservableCollection<Position> ItemsForRoles = new ObservableCollection<Position>();

            var query = db.Position.ToList();

            foreach (var roles in query)
            {
                ItemsForRoles.Add(
                    new Position { idPosition = roles.idPosition, NameOfPosition = roles.NameOfPosition }
                    );
                CmbBox_RoleOfOrganization.ItemsSource = ItemsForRoles;
            }
            CmbBox_RoleOfOrganization.ItemsSource = ItemsForRoles;
        }

        public bool AddUser(string fnm, string lnm, string mnm, string phn, string orgempl)
        {
            string LoginForAddUser = TxtBox_LName.Text.Substring(0, 2) + TxtBox_FName.Text.Substring(0, 2) + TxtBox_MName.Text.Substring(0, 2) + TxtBox_Phone.Text.Substring(TxtBox_Phone.Text.Length - 2)
                , PasswordForAddUser = TxtBox_Phone.Text.Substring(0, 2) + RandomToPassword() + TxtBox_Phone.Text.Substring(TxtBox_Phone.Text.Length - 2);
            int posit = OwBDC.ReturnidPositionForRegistration(orgempl);
            if (OwBDC.ReturnidForFullInfo(fnm, lnm, mnm, phn) == 0)
            {
                OrganizationEmployee organizationEmployee = new OrganizationEmployee()
                {
                    FirstName = fnm,
                    LastName = lnm,
                    MiddleName = mnm,
                    PhoneNumber = phn,
                    Login = LoginForAddUser.ToLower(),
                    Password = PasswordForAddUser,
                    idPosition = posit
                };
                db.OrganizationEmployee.Add(organizationEmployee);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TxtBox_FName.Text == "" && TxtBox_LName.Text == "" && TxtBox_MName.Text == "" && TxtBox_Phone.Text == "" && CmbBox_RoleOfOrganization.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Все поля пусты";
            }
            else if (TxtBox_FName.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите имя";
            }
            else if (TxtBox_LName.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите фамилию";
            }
            else if (TxtBox_MName.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите отчество";
            }
            else if (TxtBox_Phone.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите номер телефона";
            }
            else if (CmbBox_RoleOfOrganization.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Выбирите должность";
            }
            else
            {
                if (AddUser(TxtBox_FName.Text, TxtBox_LName.Text, TxtBox_MName.Text, TxtBox_Phone.Text, CmbBox_RoleOfOrganization.Text) == true)
                {
                    MessageBox.Show("Пользователь добавлен");
                }
                else
                {
                    PpTxt.Visibility = Visibility.Visible;
                    popup1.IsOpen = true;
                    PpTxt.Text = "Пользователь уже существует";
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
