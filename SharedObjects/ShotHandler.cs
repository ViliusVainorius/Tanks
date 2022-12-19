using SharedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class ShotHandler : Handler
    {
        private int _bulletId = 0;

        public override void HandleRequest(PlayerAction action, ref Tank tank)
        {
            if (action.type == ActionType.Shoot)
            {
                CreateBullet(tank);
            }
            else
            {
                successor.HandleRequest(action, ref tank);
            }
        }

        private void CreateBullet(Tank t)
        {
            List<Bullet> bulletList = GameSession.Instance.GameObjectContainer.Bullets.ToList();
            int x = t.X;
            int y = t.Y;
            bool triple = t.hasTripleShoot;

            int width = t.Width / 3;
            int height = t.Height / 3;

            GetBulletCoordinates(t, ref x, ref y, width, height);

            List<Bullet> newbulletList;
            BulletContext context;
            if (triple)
            {
                TripleBullet tripleBullet = new TripleBullet(x, y, width, height, t.speed, _bulletId, t.side,
                    t.tripleshootstartime);
                context = new BulletContext(tripleBullet);
            }
            else
            {
                SimpleBullet simpleBullet = new SimpleBullet(x, y, width, height, t.speed, _bulletId, t.side);
                context = new BulletContext(simpleBullet);
            }

            _bulletId += context.GetBulletsCount();
            newbulletList = context.RequestShoot(ref t);

            foreach (Bullet b in newbulletList)
            {
                bulletList.Add(b);
            }

            GameSession.Instance.GameObjectContainer.Bullets = bulletList.ToArray();
        }

        private void GetBulletCoordinates(Tank t, ref int x, ref int y, int width, int height)
        {
            if (t.side == FacingSide.Left)
            {
                x -= (t.Width) - width;
                y += (t.Height / 2) - height / 2;
            }
            else if (t.side == FacingSide.Right)
            {
                x += (t.Width) + width;
                y += (t.Height / 2) - height / 2;
            }
            else if (t.side == FacingSide.Down)
            {
                y += (t.Height) + 1;
                x += t.Width / 2 - width / 2;
            }
            else if (t.side == FacingSide.Up)
            {
                y -= (height);
                x += t.Width / 2 - width / 2;
            }
        }
    }
}
