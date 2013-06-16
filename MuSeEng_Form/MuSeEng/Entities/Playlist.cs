using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MuSeEng.Entities
{
    public class Playlist
    {
        /**
         * Playlist name
         */
        public String Name;

        /**
         * Track List associated with playlist.
         */
        public List<Entities.Track> Tracks = new List<Entities.Track>();

        public Playlist(String Name)
        {
            this.Name = Name;
        }
    }
}
