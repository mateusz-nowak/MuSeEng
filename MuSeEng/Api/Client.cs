using MuSeEng.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace MuSeEng.Api
{
    class Client
    {
        /**
         * Handles the Request Uri
         */
        private string baseUri;

        public Client(string baseUri)
        {
            this.baseUri = baseUri;
        }

        public List<Mp3> search(string phrase)
        {
            WebRequest request = WebRequest.Create(baseUri + "?search=" + phrase);

            List<Mp3> mp3s = new List<Mp3>();
            
            return mp3s;
        }
    }
}
