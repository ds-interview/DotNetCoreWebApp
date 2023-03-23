using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Classes
{
    public class Authentication
    {
        public static bool AuthenticateToken(string ticket, out User user)
        {
            using (AppDbContext db = new AppDbContext())
            {
                UserTicket userTicket = db.UserTickets.Where(a => a.Ticket == ticket).FirstOrDefault();
                if (userTicket == null)
                { user = null; return false; }

                user = db.Users.Find(userTicket.UserId);
                if (user == null) return false; else return true;
            }
        }

    }
}
