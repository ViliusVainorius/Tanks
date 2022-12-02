namespace SharedObjects
{
    public abstract class AbstractFactory
    {
        public abstract Tank CreateHeavyTank(int x, int y, int width, int height);
        public abstract Tank CreateLightTank(int x, int y, int width, int height);
    }
}
