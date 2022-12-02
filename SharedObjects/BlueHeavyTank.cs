namespace SharedObjects
{
    public class BlueHeavyTank : Tank
    {
        const int DefaultSpeed = 2;
        new const string Skin = "pack://application:,,,/images/tankBlue.png";
        public BlueHeavyTank(int x, int y, int width, int height) : base(x, y, width, height, DefaultSpeed,Skin)
        {
            this.lives = lives + 5;
        }
    }
}
