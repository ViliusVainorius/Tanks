namespace SharedObjects
{
    public interface Visitor
    {
        double AddPng(SingleBulletElement png);
        double AddPng(TripleBulletElement png);
    }
}