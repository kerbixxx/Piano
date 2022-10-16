using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piano
{
    public class Song : Player
    {
        public string SongName { get; set; }
        public Song(string text, int tact) : base(text, tact)
        {

        }
    }
}
