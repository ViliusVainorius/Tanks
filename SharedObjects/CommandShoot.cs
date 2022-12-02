using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class CommandShoot : CommandMove
    {
        Tank _tank;
        List<Bullet> _bullets;
        private double _myDouble;
        private double _myDouble21;

        public CommandShoot(Tank tank, List<Bullet> bullets)
        {
            this._tank = tank;
            this._bullets = bullets;
        }

        public override void Execute()
        {
            // updting lives

           // StreamWriter writer;

            /*using (writer = new StreamWriter(@"C:\Users\vytau\Documents\KTU\7 pusmetis\Objektinis programų projektavimas\Temporary2.txt"))
            {
                int i = 0;
                writer.WriteLine("Bullets count" + bullets.Count);
                foreach (Bullet b in bullets)
                {
                    writer.WriteLine("Bullet " + i + ": " + b.X);
                    i++;
                }
            }*/
        }
    }
}
