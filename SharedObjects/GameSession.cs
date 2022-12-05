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
        public DateTime StartTime {get; set;}
        public DateTime FinishTime {get; set;}
        public DifficultyEnum Difficulty {get; set;}

        [XmlAttribute]
        public int lives;
        public int self;
        public GameObjectContainer GameObjectContainer;
        public static string xmlFileName;
        public bool gameStarted = false;
        
        private static GameSession _instance;

        public static GameSession Instance => _instance ?? (_instance = DeserializeGameSession());

        private GameSession()
        {
            StartTime = DateTime.Now;
            gameStarted = true;
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

        // this method will be called when game is over (one player looses or wins)
        public void FinishGame()
        {
            FinishTime = DateTime.Now;
        }
        public enum DifficultyEnum
        {
            Easy, Medium, High
        }
        public int GetPlayerIndex(Player player)
        {
            if (GameSession.Instance.GameObjectContainer.Tanks[0].player.EndPoint == player.EndPoint)
                return 0;
            return 1;
        }
    }
}
