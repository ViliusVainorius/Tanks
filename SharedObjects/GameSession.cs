using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class GameSession
    {
        public enum Difficulty
        {
            Easy, Medium, High
        }
        public DateTime start_time {get; set;}
        public DateTime finish_time {get; set;}
        public int players_count {get; set;}
        public Difficulty difficulty {get; set;}
        public Wall [] map {get; set;}

        public GameSession(string difficulty, int players_count = 2)
        {
            start_time= DateTime.Now; 
        }

        public void GenerateMap()
        {
            throw new NotImplementedException();
        }

        // this method will be called when game is over (one player looses or wins)
        public void FinishGame()
        {
            finish_time = DateTime.Now;
        }

    }
}
