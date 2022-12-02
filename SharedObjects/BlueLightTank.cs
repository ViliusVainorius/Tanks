namespace SharedObjects
{
    public class BlueLightTank : Tank
    {
        const int DefaultSpeed = 2;
        new const string Skin = "pack://application:,,,/images/tankBlue.png";
        public BlueLightTank(int x, int y, int width, int height) : base(x, y, width, height, DefaultSpeed, Skin)
        {
            this.speed = speed + 3;
            this.lives = lives - 1;
        }
    }
}
