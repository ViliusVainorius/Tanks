using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SharedObjects
{
    public class FireBulletVisitor : Visitor
    {
        Tank t;

        public FireBulletVisitor(Tank tank)
        {
            this.t = tank; 
        }
        public Tank AddPng(SingleBulletElement png) // single bullet fire png
        {
            Tank t1;
            //...
            if (!t.hasTripleShoot) // Single bullet method
            {
                if(t.side == FacingSide.Up)
                {
                    t1 = new Tank(t.X +(t.Width / 2 )/2, t.Y - (t.Height / 2), t.Width / 2, t.Height / 2, 0, ""); // Width="60" Height="75" / rectangle width = 30 height = 37.5
                }                                                                                                 // WCenter=30 HCenter=37.5 /          WCenter=15 HCenter=18.75
                else if(t.side == FacingSide.Down)
                {
                    t1 = new Tank(t.X + (t.Width / 2)/2, t.Y + (t.Height), t.Width / 2, t.Height / 2, 0, "");
                }
                else if (t.side == FacingSide.Right)
                {
                    t1 = new Tank(t.X + t.Height-2, t.Y + (t.Width/2-10), t.Width / 2, t.Height / 2, 0, "");
                }
                else if (t.side == FacingSide.Left)
                {
                    t1 = new Tank(t.X- (t.Width / 2)-8, t.Y + ((t.Height/2)/2), t.Width / 2, t.Height / 2, 0, "");
                }
                else
                {
                    t1 = new Tank(t.X, t.Y, t.Width / 2, t.Height / 2, 0, "");
                }
                t1.Skin = "pack://application:,,,/images/explosion.png"; // skin name png...
            }
            else
            {
                t1 = new Tank(t.X, t.Y, t.Width / 2, t.Height / 2, 0, "");
            }

            return t1;
        }

        public Tank AddPng(TripleBulletElement png) // triple bullet fire png
        {
            Tank t1;
            //...
            if (t.hasTripleShoot) // Triple bullet method
            {
                if (t.side == FacingSide.Up)
                {
                    t1 = new Tank(t.X + (t.Width / 2) / 2, t.Y - (t.Height / 2), t.Width / 2, t.Height / 2, 0, "");
                }
                else if (t.side == FacingSide.Down)
                {
                    t1 = new Tank(t.X + (t.Width / 2) / 2, t.Y + (t.Height), t.Width / 2, t.Height / 2, 0, "");
                }
                else if (t.side == FacingSide.Right)
                {
                    t1 = new Tank(t.X + t.Height - 2, t.Y + (t.Width / 2 - 10), t.Width / 2, t.Height / 2, 0, "");
                }
                else if (t.side == FacingSide.Left)
                {
                    t1 = new Tank(t.X - (t.Width / 2) - 8, t.Y + ((t.Height / 2) / 2), t.Width / 2, t.Height / 2, 0, "");
                }
                else
                {
                    t1 = new Tank(t.X, t.Y, t.Width / 2, t.Height / 2, 0, "");
                }
                t1.Skin = "pack://application:,,,/images/explosion.png"; // skin name png...
            }
            else
            {
                t1 = new Tank(t.X, t.Y, t.Width / 2, t.Height / 2, 0, "");
            }

            return t1;
        }
    }
}
