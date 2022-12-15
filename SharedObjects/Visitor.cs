namespace SharedObjects
{
    public interface Visitor
    {
        Tank AddPng(SingleBulletElement png);
        Tank AddPng(TripleBulletElement png);
    }
}