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
        /*public void GetShotTest()
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

        [Fact]
        public void TwoTanksCollide()
        {
            Tank t1 = new Tank(10, 10, 10, 10);
            Tank t2 = new Tank(10, 10, 10, 10);

            Assert.True(t1.Intersect(t2));
        }


        [Fact]
        public void StepOnMine()
        {
            bool damageDelt = false;
            Tank t1 = new Tank(10, 10, 10, 10);
            Powerup powerup = new Powerup(10, 10, 10, 10, Powerup.PowerupType.Mine);


            if (t1.Intersect(powerup))
            {
                damageDelt = t1.MineDamage();
            }

            Assert.True(damageDelt);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(15)]
        [InlineData(1)]
        public void MaxItemsSpawned(int n)
        {
            Tank t = new Tank();
            List<Powerup> powerups = new List<Powerup>();
            if (n >= 10)
                n = 10;

            for (int i = 0; i < n; i++)
            {
                t.MaxSpawnedItems(powerups);
            }

            Assert.Equal(n, powerups.Count);
        }

        [Theory]
        [InlineData(Powerup.PowerupType.Live)]
        [InlineData(Powerup.PowerupType.Mine)]
        [InlineData(Powerup.PowerupType.TripleShoot)]
        public void GetPowerupTest(Powerup.PowerupType type)
        {
            Tank t = new Tank();
            int initialLives = t.lives;
            Powerup powerup = new Powerup(1, 1, 1, 1, type);
            t.PickPowerup(powerup);

            if (type == Powerup.PowerupType.Live)
            {
                Assert.Equal(initialLives + 1, t.lives);
            }
            else
            {
                Assert.Equal(initialLives, t.lives);
            }

        }*/
    }
}