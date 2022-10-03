using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class GameSession
    {
        public DateTime start_time {get; set;}
        public DateTime finish_time {get; set;}
        public int players_count {get; set;}
        public string difficulty {get; set;}
        public Wall [] map {get; set;}


    }
}
