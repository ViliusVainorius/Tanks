using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace SharedObjects
{
    public class GameSession
    {
        public DateTime start_time {get; set;}
        public DateTime finish_time {get; set;}
        public int players_count {get; set;}
        public Difficulty difficulty {get; set;}
        
        public Tank[] Tanks;
        public Wall[] Walls;
        public int self;
        
        private static GameSession instance;

        public static GameSession Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = DeserializeXml(@"C:\Users\razma\source\repos\ViliusVainorius\Tanks\SharedObjects\Maps\Map1.xml");
                }
                return instance;
            }
        }

        private GameSession()
        {
            start_time = DateTime.Now;
        }

        private static GameSession DeserializeXml(string fileName)
        {
            GameSession gameSession = new GameSession();

            XmlSerializer ser = new XmlSerializer(typeof(GameSession));
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                gameSession = (GameSession)ser.Deserialize(reader);
            }

            return gameSession;
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
        public enum Difficulty
        {
            Easy, Medium, High
        }
    }
}
