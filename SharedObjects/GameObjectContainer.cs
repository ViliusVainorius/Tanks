using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        public Bullet[] Bullets;
        public Bullet[] remove;

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
                Tanks[i].tripleshootstartime = gameObjectContainer.Tanks[i].tripleshootstartime;
            }

            for(int i = 0; i < Powerups.Count(); i++)
            {
                UsedPowerup powerup = new UsedPowerup();
                if (gameObjectContainer.Powerups[i].GetType() == powerup.GetType())
                {
                    powerup.rectangle = Powerups[i].Rectangle;
                    Powerups[i] = powerup;
                }
            }

            // if no bullets exists, create them
            if (Bullets.Length == 0)
            {
                Bullets = gameObjectContainer.Bullets;
                remove = new Bullet[0];
            }
            // go trough all bullets and check whether any of them is deleted (collided or else) 
            else
            {
                List<Bullet> bullets = new List<Bullet>();
                List<Bullet> removeList = new List<Bullet>();
 
                foreach (var b in Bullets)
                {
                    bool found = false;
                    foreach (Bullet bullet in gameObjectContainer.Bullets)
                    {
                        if (bullet.bulletId == b.bulletId)
                        {
                            found = true;
                            b.X = bullet.X;
                            b.Y = bullet.Y;
                            bullets.Add(b);
                            break;
                        }
                    }
                    // deletes bullet if needed
                    if (!found)
                    {
                        removeList.Add(b);
                    }
                }
                // check which of bullets are new and draws them
                foreach(Bullet bullet in gameObjectContainer.Bullets)
                {
                    bool found = false;
                    foreach (var b in Bullets)
                    {
                        if (bullet.bulletId == b.bulletId)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        bullets.Add(bullet);
                    }
                }

                Bullets = bullets.ToArray();
                this.remove = removeList.ToArray();
            }
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            List<Tank> tanks = new List<Tank>();
            List<Wall> walls = new List<Wall>();
            List<Powerup> powerups = new List<Powerup>();
            List<Bullet> bullets = new List<Bullet>();

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
                        Creator ctr = new TankCreator();
                        string color = "R";
                        // X = 135 Pirmas player
                        // X = 635 Antras player
                        //----------------------------------------------
                        if (int.Parse(reader.GetAttribute("X")) == 135)
                            color = "B";
                        else if(int.Parse(reader.GetAttribute("X")) == 635)
                            color = "R";
                        //----------------------------------------------
                        Team myTeam = ctr.FactoryMethod(color);// creates a Blue tank (blue skin for a player1 tank "B" - blue skin/ "R"- red skin)

                        AbstractFactory unitFactory = myTeam.GetAbstractFactory(); // initializing abstract factory

                        Tank myTank = null;
                        string userInput = "Heavy"; // "Heavy - creates a heavy tank/ "Light" - creates a light tank"
                        // X = 135 Pirmas player
                        // X = 635 Antras player
                        //----------------------------------------------
                        if (int.Parse(reader.GetAttribute("X")) == 135)
                            userInput = "Heavy";
                        else if (int.Parse(reader.GetAttribute("X")) == 635)
                            userInput = "Light";
                        //----------------------------------------------

                        if (userInput.Equals("Heavy"))
                        {
                            myTank = unitFactory.CreateHeavyTank(int.Parse(reader.GetAttribute("X")), int.Parse(reader.GetAttribute("Y")), 
                                int.Parse(reader.GetAttribute("Width")), int.Parse(reader.GetAttribute("Height")));
                        }
                        if (userInput.Equals("Light"))
                        {
                            myTank = unitFactory.CreateLightTank(int.Parse(reader.GetAttribute("X")), int.Parse(reader.GetAttribute("Y")),
                                int.Parse(reader.GetAttribute("Width")), int.Parse(reader.GetAttribute("Height")));
                        }

                        //playerImages[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/tankRed.png"));



                        //Tank tank = new Tank(int.Parse(reader.GetAttribute("X")), int.Parse(reader.GetAttribute("Y")), int.Parse(reader.GetAttribute("Width")), int.Parse(reader.GetAttribute("Height")), int.Parse(reader.GetAttribute("Speed")));
                        myTank.Rotation = int.Parse(reader.GetAttribute("Rotation"));
                        myTank.lives = int.Parse(reader.GetAttribute("lives"));
                        //--------------------- Memento pattern
                        myTank.State = "Healthy"; // states - "Healthy" "Shot" "Broken"
                        //---------------------
                        tanks.Add(myTank);
                        break;
                    case "Bullet":
                        bullets.Add(new Bullet(int.Parse(reader.GetAttribute("X")), int.Parse(reader.GetAttribute("Y")), int.Parse(reader.GetAttribute("Width")), int.Parse(reader.GetAttribute("Height")), int.Parse(reader.GetAttribute("speed")), int.Parse(reader.GetAttribute("bulletId"))));
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

            // hardcoded powerup adding to map
            PowerupsMaker powerupsMaker = new PowerupsMaker();
            powerups.Add(powerupsMaker.CreateHealthPowerup());
            powerups.Add(powerupsMaker.CreateTripleShootPowerup());
            powerups.Add(powerupsMaker.CreateSpeedPowerup());

            Tanks = tanks.ToArray();
            Walls = walls.ToArray();
            Powerups = powerups.ToArray();
            Bullets = bullets.ToArray();
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
                writer.WriteAttributeString("speed", tank.speed.ToString());
                writer.WriteAttributeString("Rotation", tank.Rotation.ToString());
                writer.WriteAttributeString("lives", tank.lives.ToString());
                writer.WriteAttributeString("tripleshootstartime", tank.tripleshootstartime.ToString());
                int side = (int)tank.side;
                writer.WriteAttributeString("side", side.ToString());
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

            writer.WriteStartElement("Bullets");
            foreach (Bullet bullet in Bullets)
            {
                writer.WriteStartElement(bullet.GetType().Name);
                writer.WriteAttributeString("Height", bullet.Height.ToString());
                writer.WriteAttributeString("Width", bullet.Width.ToString());
                writer.WriteAttributeString("X", bullet.X.ToString());
                writer.WriteAttributeString("Y", bullet.Y.ToString());
                writer.WriteAttributeString("speed", bullet.speed.ToString());
                writer.WriteAttributeString("bulletId", bullet.bulletId.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
