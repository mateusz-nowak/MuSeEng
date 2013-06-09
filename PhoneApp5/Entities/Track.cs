using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneApp5.Entities
{
    public class Track
    {
        /**
         * Handles the track Id
         */
        public int id;
        
        /**
         * Handles the Track Title
         */
        public String title;
        
        /**
         * Handles the remote key
         */
        public String remote;
        
        /**
         * Handles the slugified title
         */
        public String slug;
        
        /**
         * Handles the relative uri to the web page
         * Where you can download track
         */
        public String show;

        /**
         * Handles the relative download Uri
         */
        public String download;
    }
}
