using Xunit;
using SharedObjects;

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

        }
        // TO DO
        public void MaxItemsSpawned()
        {

        }
        // TO DO
        public void StepOnMine()
        {

        }
    }
}