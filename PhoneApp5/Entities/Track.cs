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
        public int Id;
        
        /**
         * Handles the Track Title
         */
        public String Title;
        
        /**
         * Handles the remote key
         */
        public String Remote;
        
        /**
         * Handles the slugified title
         */
        public String Slug;
        
        /**
         * Handles the relative uri to the web page
         * Where you can download track
         */
        public String Show;

        /**
         * Handles the relative download Uri
         */
        public String Download;
    }
}
