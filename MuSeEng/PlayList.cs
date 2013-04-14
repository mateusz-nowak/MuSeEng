using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuSeEng
{
    class PlayList : IPlaylist
    {
        private string Name;
        
        public PlayList(string name)
        {
            Name = name;
            
        }
        public List<MP3> MP3s
        {
            get;
            set;
        }

    }
}
