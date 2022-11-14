using Xunit;
using SharedObjects;
using System;
using Tanks;
using System.Collections.Generic;


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
         // TO DO
        public void HitObstacle()
        {

        }
        // TO DO
        public void TwoTanksCollide()
        {
            Tank t1 = new Tank();
            Tank t2 = new Tank();

            t1.X = 10;      t1.Y = 10;
            t1.Width = 10;  t1.Height = 10;

            t2.X = 10;      t2.Y = 10;
            t2.Width = 10;  t2.Height = 10;

            t1.TankCollision(t2);


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

        }
    }
}