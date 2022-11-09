using Xunit;
using SharedObjects;

namespace Tests
{
    public class GameTests
    {
        [Fact]
        public void GetPowerup()
        {
            Player p = new Player(null);

            int initialLives = GameSession.Instance.lives;
            Powerup powerup = new Powerup(1, 1, 1, 1, Powerup.PowerupType.Live);
            p.PickPowerup(powerup);

            Assert.Equal(initialLives + 1, GameSession.Instance.lives);
        }
    }
}