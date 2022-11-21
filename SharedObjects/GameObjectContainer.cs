using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SharedObjects
{
    public class GameObjectContainer : IXmlSerializable
    {
        public Tank[] Tanks;
        public Wall[] Walls;
        public Powerup[] Powerups;

        public void Update(GameObjectContainer gameObjectContainer)
        {
            for (int i = 0; i < Tanks.Count(); i++)
            {
                Tanks[i].X = gameObjectContainer.Tanks[i].X;
                Tanks[i].Y = gameObjectContainer.Tanks[i].Y;
                Tanks[i].Rotation = gameObjectContainer.Tanks[i].Rotation;
                Tanks[i].lives = gameObjectContainer.Tanks[i].lives;
                Tanks[i].speed = gameObjectContainer.Tanks[i].speed;
                Tanks[i].hasTripleShoot = gameObjectContainer.Tanks[i].hasTripleShoot;
            }

            for(int i = 0; i < Powerups.Count(); i++)
            {
                UsedPowerup powerup = new UsedPowerup();
                if (gameObjectContainer.Powerups[i].GetType() == powerup.GetType())
                {
                    powerup._rectangle = Powerups[i].Rectangle;
                    Powerups[i] = powerup;
                }
            }
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            XmlNamespaceManager manager = new XmlNamespaceManager(new NameTable());
            List<Tank> tanks = new List<Tank>();
            List<Wall> walls = new List<Wall>();
            List<Powerup> powerups = new List<Powerup>();

            while (reader.Read())
            {
                if(reader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                switch(reader.Name)
                {
                    case "Wall":
                        walls.Add(new Wall(int.Parse(reader.GetAttribute("X")), int.Parse(reader.GetAttribute("Y")), int.Parse(reader.GetAttribute("Width")), int.Parse(reader.GetAttribute("Height"))));
                        break;
                    case "Tank":
                        Tank tank = new Tank(int.Parse(reader.GetAttribute("X")), int.Parse(reader.GetAttribute("Y")), int.Parse(reader.GetAttribute("Width")), int.Parse(reader.GetAttribute("Height")), int.Parse(reader.GetAttribute("Speed")));
                        tank.Rotation = int.Parse(reader.GetAttribute("Rotation"));
                        tanks.Add(tank);
                        break;
                    case "HealthPowerup":
                        powerups.Add(new HealthPowerup(int.Parse(reader.GetAttribute("X")), int.Parse(reader.GetAttribute("Y")), int.Parse(reader.GetAttribute("Width")), int.Parse(reader.GetAttribute("Height"))));
                        break;
                    case "SpeedPowerup":
                        powerups.Add(new SpeedPowerup(int.Parse(reader.GetAttribute("X")), int.Parse(reader.GetAttribute("Y")), int.Parse(reader.GetAttribute("Width")), int.Parse(reader.GetAttribute("Height"))));
                        break;
                    case "TripleShotPowerup":
                        powerups.Add(new TripleShotPowerup(int.Parse(reader.GetAttribute("X")), int.Parse(reader.GetAttribute("Y")), int.Parse(reader.GetAttribute("Width")), int.Parse(reader.GetAttribute("Height"))));
                        break;
                    case "UsedPowerup":
                        powerups.Add(new UsedPowerup());
                        break;
                }
            }

            Tanks = tanks.ToArray();
            Walls = walls.ToArray();
            Powerups = powerups.ToArray();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("GameObjectContainer");

            writer.WriteStartElement("Walls");
            foreach(Wall wall in Walls)
            {
                writer.WriteStartElement("Wall");
                writer.WriteAttributeString("Height", wall.Height.ToString());
                writer.WriteAttributeString("Width", wall.Width.ToString());
                writer.WriteAttributeString("X", wall.X.ToString());
                writer.WriteAttributeString("Y", wall.Y.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Tanks");
            foreach (Tank tank in Tanks)
            {
                writer.WriteStartElement("Tank");
                writer.WriteAttributeString("Height", tank.Height.ToString());
                writer.WriteAttributeString("Width", tank.Width.ToString());
                writer.WriteAttributeString("X", tank.X.ToString());
                writer.WriteAttributeString("Y", tank.Y.ToString());
                writer.WriteAttributeString("Speed", tank.speed.ToString());
                writer.WriteAttributeString("Rotation", tank.Rotation.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Powerups");
            foreach (Powerup powerup in Powerups)
            {
                writer.WriteStartElement(powerup.GetType().Name);
                writer.WriteAttributeString("Height", powerup.Height.ToString());
                writer.WriteAttributeString("Width", powerup.Width.ToString());
                writer.WriteAttributeString("X", powerup.X.ToString());
                writer.WriteAttributeString("Y", powerup.Y.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
