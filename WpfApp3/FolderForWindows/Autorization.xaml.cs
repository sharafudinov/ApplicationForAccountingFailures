using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        shiaDBEntities db = new shiaDBEntities();
        OperationWithBDClass classbdwork = new OperationWithBDClass();
        ForWorkAnyData AND = new ForWorkAnyData();
        public Autorization()
        {
            InitializeComponent();
            popup1.IsOpen = false;
            PpTxt.Visibility = Visibility.Hidden;
        }
        public void testforanyoshib()
        {
            if (Txtbox_Login.Text == "" && PswrdBox_password.Password == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите Логин и пароль";
            }
            else if (Txtbox_Login.Text != "" && PswrdBox_password.Password == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите пароль";
            }
            else if (Txtbox_Login.Text == "" && PswrdBox_password.Password != "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите логин";
            }
            else
            {
                int idLogin = classbdwork.returnidlog(Txtbox_Login.Text);

                if (idLogin > 0)
                {
                    int tempid = classbdwork.returnid(Txtbox_Login.Text, PswrdBox_password.Password.ToString());
                    if (tempid != 0)
                    {
                        AND.idSotr = tempid;
                        MessageBox.Show("" + classbdwork.ReturnlvlAccess(tempid));
                        AND.LvlOfAccess = classbdwork.ReturnlvlAccess(tempid);
                        MainWindow mw = new MainWindow();
                        Hide();
                        mw.Show();
                    }
                    else
                    {
                        PpTxt.Visibility = Visibility.Visible;
                        popup1.IsOpen = true;
                        PpTxt.Text = "Введенный пароль неверен";
                    }
                }
                else
                {
                    PpTxt.Visibility = Visibility.Visible;
                    popup1.IsOpen = true;
                    PpTxt.Text = "Пользователя не существует";
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            testforanyoshib();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
