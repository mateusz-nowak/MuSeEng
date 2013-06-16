using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MuSeEng.Entities
{
    public class Result
    {
        /**
         * Checks whether is next page of the resutls
         * 1 when true
         * 0 when false
         */
        public int Next;

        /**
         * List of found tracks for current page
         */
        public List<Track> Tracks = new List<Track>();
    }
}
