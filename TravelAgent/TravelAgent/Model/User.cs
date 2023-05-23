using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgent.Model
{
    public class User
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public Role role { get; set; }

        public User(long id, String name, String surname, String email, String password, Role role)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Password = password;
            this.role = role;

        }

    
    }
    public enum Role
    {
        USER, AGENT
    }
}
