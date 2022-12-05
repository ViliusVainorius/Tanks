using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class PowerupsMaker
    {
        private Powerup _tripleShootP;
        private Powerup _speedP;
        private Powerup _healthP;
        private const int DefaultVal = 100;

        // randomize location
        Random _random = new Random();
            
        public SpeedPowerup CreateSpeedPowerup()
        {
            int x = _random.Next(5);
            int y = _random.Next(5);
            _speedP = new SpeedPowerup(DefaultVal, DefaultVal, DefaultVal / 2, DefaultVal / 2);
            return (_speedP as SpeedPowerup);
        }
        public TripleShotPowerup CreateTripleShootPowerup()
        {
            _tripleShootP = new TripleShotPowerup(DefaultVal + 200, DefaultVal + 40, DefaultVal / 3, DefaultVal / 3);
            return (_tripleShootP as TripleShotPowerup);
        }
        public HealthPowerup CreateHealthPowerup()
        {
            _healthP = new HealthPowerup(DefaultVal + 30, DefaultVal + 30, DefaultVal / 2, DefaultVal / 2);
            return (_healthP as HealthPowerup);
        }


    }
}
