using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
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
        public Difficulty difficulty {get; set;}

        [XmlAttribute]
        public int lives;
        public int self;
        public GameObjectContainer GameObjectContainer;
        public static string xmlFileName;
        
        private static GameSession instance = null;

        public static GameSession Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = DeserializeGameSession();
                }
                return instance;
            }
        }

        private GameSession()
        {
            start_time = DateTime.Now;
        }

        private static GameSession DeserializeGameSession()
        {
            GameSession gameSession = new GameSession();

            XmlSerializer ser = new XmlSerializer(typeof(GameSession));
            using (StringReader sr = new StringReader(File.ReadAllText(xmlFileName)))
            {
                using (XmlReader reader = XmlReader.Create(sr))
                {
                    gameSession = (GameSession)ser.Deserialize(reader);
                }
            }

            return gameSession;
        }

        public void DeserializeContainer()
        {
            GameObjectContainer = new GameObjectContainer();

            XmlSerializer ser = new XmlSerializer(typeof(GameSession));
            using (XmlReader reader = XmlReader.Create(File.ReadAllText(xmlFileName)))
            {
                GameObjectContainer = (GameObjectContainer)ser.Deserialize(reader);
            }
        }

        public void GenerateMap()
        {
            //throw new NotImplementedException();
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
