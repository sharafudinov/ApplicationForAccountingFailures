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
using System.Data.Entity;

namespace WpfApp3.FolderForWindows
{
    /// <summary>
    /// Логика взаимодействия для WindowOfInfoSotr.xaml
    /// </summary>
    public partial class WindowOfInfoSotr : Window
    {
        shiaDBEntities db = new shiaDBEntities();
        OperationWithBDClass OWBDC = new OperationWithBDClass();
        int CurrentLvlOfDostup;
        String CurrentPositionOfSotr;
        public WindowOfInfoSotr()
        {
            InitializeComponent();
            popup1.IsOpen = false;
            PpTxt.Visibility = Visibility.Hidden;
        }
        public int idSotr;
        public void ConstructForm(int a)
        {
            if (a == 1)
            {
                btn_back.Margin = new Thickness(93, 60, 0, 0);
                btn_Update.Margin = new Thickness(10, 61, 0, 0);
                btn_Update.Visibility = Visibility.Visible;
                btn_Update.IsEnabled = true;
            }
            else
            {
                btn_back.Margin = new Thickness(10, 61, 0, 0);
                btn_Update.Visibility = Visibility.Hidden;
                btn_Update.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InfoAboutUsersSystem ads = new InfoAboutUsersSystem();
            this.Hide();
            ads.Show();
        }
        public void Fill()
        {
            var InfoOfSotrToFill = db.OrganizationEmployee.Include(i => i.Position).Where(w => w.idEmployee == idSotr).FirstOrDefault();
            FName.Text = InfoOfSotrToFill.FirstName;
            LName.Text = InfoOfSotrToFill.LastName;
            MName.Text = InfoOfSotrToFill.MiddleName;
            Phon.Text = InfoOfSotrToFill.PhoneNumber;
            Pos.Text = InfoOfSotrToFill.Position.NameOfPosition;
            LelelOfDostup.Text = InfoOfSotrToFill.Position.LevelOfAccess.ToString();
            CurrentPositionOfSotr = InfoOfSotrToFill.Position.NameOfPosition;
            CurrentLvlOfDostup = InfoOfSotrToFill.Position.LevelOfAccess;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int idps = OWBDC.ReturnidPosition(Pos.Text, Convert.ToInt32(LelelOfDostup.Text));
            if (FName.Text == "" && LName.Text == "" && MName.Text == "" && Phon.Text == "" && Pos.Text == "" && LelelOfDostup.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Все поля пусты";
            }
            else if (FName.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле имя не заполнено";
            }
            else if (LName.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле фамилия не заполнено";
            }
            else if (MName.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле отчество не заполнено";
            }
            else if (Phon.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле номер телефона не заполнено";
            }
            else if (Pos.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле должность не заполнено";
            }
            else if (LelelOfDostup.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле уровень доступа не заполнено";
            }
            else if (idps == 0)
            {
                Position PosAdd = new Position()
                {
                    NameOfPosition = Pos.Text,
                    LevelOfAccess = Convert.ToInt32(LelelOfDostup.Text)
                };
                db.Position.Add(PosAdd);
                db.SaveChanges();
                int idps2 = OWBDC.ReturnidPosition(Pos.Text, Convert.ToInt32(LelelOfDostup.Text));
                int inf = db.OrganizationEmployee.Where(w => w.idEmployee == idSotr).Select(s => s.idEmployee).FirstOrDefault();
                var myupdate = db.OrganizationEmployee.Where(w => w.idEmployee == inf).FirstOrDefault();
                myupdate.LastName = LName.Text;
                myupdate.FirstName = FName.Text;
                myupdate.MiddleName = MName.Text;
                myupdate.PhoneNumber = Phon.Text;
                myupdate.idPosition = idps2;
                db.SaveChanges();

            }
            else
            {
                int inf = db.OrganizationEmployee.Where(w => w.idEmployee == idSotr).Select(s => s.idEmployee).FirstOrDefault();
                var myupdate = db.OrganizationEmployee.Where(w => w.idEmployee == inf).FirstOrDefault();
                myupdate.LastName = LName.Text;
                myupdate.FirstName = FName.Text;
                myupdate.MiddleName = MName.Text;
                myupdate.PhoneNumber = Phon.Text;
                db.SaveChanges();
            }
        }
    }
}
