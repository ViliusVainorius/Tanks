using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections;

namespace SharedObjects
{
    public class Bullet : GameObject
    {
        public int speed;
        public int bulletId;
        public FacingSide side;

        public Bullet(int x, int y, int width, int height, int speed, int bulletId, FacingSide side = FacingSide.Right) : base(x, y, width, height)
        {
            this.speed = speed;
            this.bulletId = bulletId;
            this.side = side;
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void GetDimensions()
        {
            throw new NotImplementedException();
        }

        public override Rectangle GetNewRectangle()
        {
            Rectangle rec = new Rectangle()
            {
                Width = this.Width,
                Height = this.Height,
                Fill = Brushes.Green,
                Stroke = Brushes.Purple,
                StrokeThickness = 2,
                Tag = "Bullet",
                RadiusX = 50,
                RadiusY = 50,
            };

            return rec;
        }

        public void Move()
        {
            switch (side)
            {
                case FacingSide.Up:
                    Y -= speed;
                    break;
                case FacingSide.Down:
                    Y += speed;
                    break;
                case FacingSide.Left:
                    X -= speed;
                    break;
                case FacingSide.Right:
                    X += speed;
                    break;
            }

            List<Bullet> bullets = GameSession.Instance.GameObjectContainer.Bullets.ToList();

            GameObject gameObject = CheckCollision(GameSession.Instance.GameObjectContainer.Tanks);
            Tank[] tanks = GameSession.Instance.GameObjectContainer.Tanks;

            if(gameObject != null)
            {
                for(int i = 0; i < tanks.Length; i++)
                {
                    if(gameObject == tanks[i])
                    {
                        tanks[i].lives--;
                        bullets.Remove(this);
                        GameSession.Instance.GameObjectContainer.Bullets = bullets.ToArray();
                        return;
                    }
                }
            }

            gameObject = CheckCollision(GameSession.Instance.GameObjectContainer.Walls);
            Wall[] walls = GameSession.Instance.GameObjectContainer.Walls;

            if (gameObject != null)
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    if (gameObject == walls[i])
                    {
                        bullets.Remove(this);
                        GameSession.Instance.GameObjectContainer.Bullets = bullets.ToArray();
                        return;
                    }
                }
            }
        }
    }
}
