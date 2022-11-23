using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class PowerupsMaker
    {
        private Powerup tripleShootP;
        private Powerup speedP;
        private const int defaultVal = 100;

        // randomize location
        Random random = new Random();
            
        public PowerupsMaker()
        {
        }

        public SpeedPowerup CreateSpeedPowerup()
        {
            int x = random.Next(5);
            int y = random.Next(5);
            speedP = new SpeedPowerup(defaultVal, defaultVal, defaultVal / 2, defaultVal / 2);
            return (speedP as SpeedPowerup);
        }
        public TripleShotPowerup CreateTripleShootPowerup()
        {
            tripleShootP = new TripleShotPowerup(defaultVal + 10, defaultVal + 10, defaultVal / 4 , defaultVal / 4);
            return (tripleShootP as TripleShotPowerup);
        }


    }
}
