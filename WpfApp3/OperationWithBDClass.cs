using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace WpfApp3
{
    class OperationWithBDClass
    {
        shiaDBEntities db = new shiaDBEntities();

        public int returnid(string login, string password)
        {
            return db.OrganizationEmployee.Where(w => w.Login == login && w.Password == password).Select(s => s.idEmployee).FirstOrDefault();
        }
        public int returnidlog(string login)
        {
            return db.OrganizationEmployee.Where(w => w.Login == login).Select(s => s.idEmployee).FirstOrDefault();
        }
        public int ReturnlvlAccess(int idUser)
        {
            return db.OrganizationEmployee.Include(i => i.Position).Where(w => w.idEmployee == idUser).Select(s => s.Position.LevelOfAccess).FirstOrDefault();
        }
        public int ReturnidPosition(string pos, int lvl)
        {
            return db.Position.Where(w => w.NameOfPosition == pos && w.LevelOfAccess == lvl).Select(s => s.idPosition).FirstOrDefault();
        }
        public int ReturnidPositionForRegistration(string pos)
        {
            return db.Position.Where(w => w.NameOfPosition == pos).Select(s => s.idPosition).FirstOrDefault();
        }

        public int ReturnidForFullInfo(string fnm, string lnm, string mnm, string phn)
        {
            return db.OrganizationEmployee.Where(w => w.FirstName == fnm && w.LastName == lnm && w.MiddleName == mnm && w.PhoneNumber == phn).Select(s => s.idEmployee).FirstOrDefault();
        }
    }
}
