using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuSeEng
{
    class User : IUser
    {
        public string Name
        {
            get { return Name; }
            set 
            { 
                Name = value; 
            }
        }
        public string Password
        {
            set
            {
                Password = value;
            }
        }
        public User(string username, string password)
        {
            this.Name = username;
            this.Password = password;
        }
        public List<PlayList> playlists
        {   
            get;
            set;
        }
    }
}
