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
    /// Логика взаимодействия для InfoForRepair.xaml
    /// </summary>
    public partial class InfoForRepair : Window
    {
        shiaDBEntities db = new shiaDBEntities();
        public InfoForRepair()
        {
            InitializeComponent();
        }
        public int idRep;
        public void loadinfo()
        {
            var info = db.Repair.Include(i => i.ApplicationAcceptance).Include(i => i.ApplicationAcceptance.StatusOfApplicationAcceptance).Include(i => i.StatusOfRepair).Include(i => i.EquipmentBreakdown).Include(i => i.ApplicationAcceptance.OrganizationEmployee).Include(i => i.ApplicationAcceptance.OrganizationEmployee.Position).Include(i => i.ApplicationAcceptance.EquipmentCard).Where(w => w.idRepair == idRep).FirstOrDefault();
            LFMname.Text = info.ApplicationAcceptance.OrganizationEmployee.LastName + " " + info.ApplicationAcceptance.OrganizationEmployee.FirstName.Substring(0, 1) + "." + info.ApplicationAcceptance.OrganizationEmployee.LastName.Substring(0, 1);
            NameOfEquip.Text = info.ApplicationAcceptance.EquipmentCard.NameOfEquipment;
            InventNumber.Text = info.ApplicationAcceptance.EquipmentCard.InventoryNumber;
            Breakdown.Text = info.EquipmentBreakdown.NameOfBreakdown;
            StatusTxt.Text = info.StatusOfRepair.StatusOfRepair1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InfoForRepair ifsa = new InfoForRepair();
            this.Hide();
            ifsa.Show();
        }
    }
}
