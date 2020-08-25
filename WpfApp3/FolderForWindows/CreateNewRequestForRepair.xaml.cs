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
using System.Data.Entity;
using System.Windows.Shapes;

namespace WpfApp3.FolderForWindows
{
    /// <summary>
    /// Логика взаимодействия для CreateNewRequestForRepair.xaml
    /// </summary>
    public partial class CreateNewRequestForRepair : Window
    {
        ForWorkAnyData fw = new ForWorkAnyData();
        shiaDBEntities db = new shiaDBEntities();
        public int ideq;
        public CreateNewRequestForRepair()
        {
            InitializeComponent();
            DatePick_Start.SelectedDate = DateTime.Now;
            DatePick_End.SelectedDate = DateTime.Now.AddDays(14);
        }
        public void fill()
        {
            var Filling = db.EquipmentCard.Include(i => i.StatusOfEquipment).Where(w => w.IdEquipment == ideq).FirstOrDefault();
            Eqiup.Text = Filling.NameOfEquipment;
            Inventnum.Text = Filling.InventoryNumber;
            Stat.Text = Filling.StatusOfEquipment.StatusOfEquipment1;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApplicationAcceptance addAA = new ApplicationAcceptance()
            {
                idEquipment = ideq,
                ResponsiblePerson = fw.idSotr,
                DateOfAdoption = Convert.ToDateTime(DatePick_Start.SelectedDate),
                FinishDateRepair = Convert.ToDateTime(DatePick_End.SelectedDate)
            };
            db.ApplicationAcceptance.Add(addAA);
            db.SaveChanges();
            MessageBox.Show("Заявка добавлена");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RequestWindow wr = new RequestWindow();
            this.Hide();
            wr.Show();
        }
    }
}
