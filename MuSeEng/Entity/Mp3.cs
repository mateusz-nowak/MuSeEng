using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuSeEng.Entity
{
    class Mp3
    {
        /**
         * Mp3 Name
         */
        public string Name
        {
            set;
            get;
        }
        
        /**
         * Download Uri
         */
        public string Uri
        {
            set;
            get;
        }

        public Mp3(string Name, string Uri)
        {
            this.Name = Name;
            this.Uri = Uri;
        }
    }
}
