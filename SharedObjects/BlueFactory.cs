namespace SharedObjects
{
    public class BlueFactory : AbstractFactory
    {
        override
            public Tank CreateHeavyTank(int x, int y, int width, int height)
        {
            return new BlueHeavyTank(x, y, width, height);
        }

        override
            public Tank CreateLightTank(int x, int y, int width, int height)
        {
            return new BlueLightTank(x,y,width,height);
        }
    }
}
