using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuSeEng
{
    class User : IUser
    {
        private string UserName;
        private string Pass;
        public User(string username, string password)
        {
            UserName = username;
            Pass = password;
        }
        public string Name
        {
            get { return UserName; }

        }
        private string Password
        {
            set 
            {
                Pass = value;
            }
        }
        public List<PlayList> playlists
        {
            get;
            set;
        }
    }
}
