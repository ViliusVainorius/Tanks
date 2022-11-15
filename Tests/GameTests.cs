using Xunit;
using SharedObjects;
using System;
using Tanks;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Tests
{
    public class GameTests
    {
        [Fact]
        public void GetPowerupTest()
        {
            Tank t = new Tank();
            int initialLives = t.lives;
            Powerup powerup = new Powerup(1, 1, 1, 1, Powerup.PowerupType.Live);
            t.PickPowerup(powerup);

            Assert.Equal(initialLives + 1, t.lives);
        }

        public void GetShotTest()
        {
            Tank t = new Tank();
            int initialLives = t.lives;
            t.GetShot();

            Assert.Equal(initialLives - 1, t.lives);
        }

        [Fact]
        public void HitObstacle()
        {
            Wall wall = new Wall(1, 1, 100, 200);
            Tank tank = new Tank(50, 50, 100, 200);

            Assert.True(tank.Intersect(wall));
        }

        // TO DO
        public void TwoTanksCollide()
        {
            Tank t1 = new Tank(10, 10, 10, 10);
            Tank t2 = new Tank(10, 10, 10, 10);

            Assert.True(t1.Intersect(t2));
        }
        // TO DO
        public void MaxItemsSpawned()
        {
            Tank t = new Tank();
            List<Powerup> powerups = new List<Powerup>();
            for (int i = 0; i < 15; i++)
            {
                t.MaxSpawnedItems(powerups);
            }

            Assert.Equal(10, powerups.Count);

        }
        // TO DO
        public void StepOnMine()
        {
            bool damageDelt = false;
            Tank t1 = new Tank(10, 10, 10, 10);
            Powerup powerup = new Powerup(1, 1, 1, 1, Powerup.PowerupType.Mine);


            if (t1.Intersect(powerup))
            {
                t1.MineDamage(damageDelt);
            }

            Assert.True(damageDelt);
        }
    }
}