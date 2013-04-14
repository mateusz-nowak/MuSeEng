using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuSeEng
{
    class MP3 : IMp3
    {
        private string Name;
        public MP3(string name)
        {
            Name = name;
        }
    }
}
