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
    /// The 'State' abstract class
    public class Bullet : GameObject
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int bulletId { get; set; }
        public int speed { get; set; }
        public FacingSide side { get; set; }
        public int bulletsToCreate { get; set; }

        protected BulletContext _context;
        public void SetContext(BulletContext context)
        {
            this._context = context;
        }

        public Bullet(int x, int y, int width, int height, int speed, int bulletId, FacingSide side = FacingSide.Right)
            : base(x, y, width, height)
        {
            this.speed = speed;
            this.bulletId = bulletId;
            this.side = side;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            bulletsToCreate = 1;
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

        // hande abstract class for State
        public virtual List<Bullet> Shoot(ref Tank t)
        {
            return new List<Bullet>();
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

    /// <summary>
        /// A 'ConcreteStateA' class
        /// </summary>
        public class SimpleBullet : Bullet
        {
            public SimpleBullet(int x, int y, int width, int height, int speed, int bulletId, FacingSide side) 
                : base(x, y, width, height, speed, bulletId, side)
            {
                bulletsToCreate = 1;
            }

            public override List<Bullet> Shoot(ref Tank t)
            {
                List<Bullet> bullets = new List<Bullet>();
                if (t.hasTripleShoot)
                {
                    this._context.ChangeState(new TripleBullet(x, y, width, height, speed, bulletId, side, t.tripleshootstartime));
                    this.Shoot(ref t); // calls triple shoot bullet instead. There wont be recursion, because type changes
                }
                else
                {
                    Bullet b = new Bullet(x, y, width,
                        height, speed, bulletId++, side);
                    bullets.Add(b);
                }
                return bullets;
            }
        }

        /// <summary>
        /// A 'ConcreteStateB' class
        /// </summary>
        public class TripleBullet : Bullet
        {
            public DateTime tripleshootstartime { get; set; }
            public TripleBullet(int x, int y, int width, int height, int speed, int bulletId, FacingSide side, DateTime startTime)
                    : base(x, y, width, height, speed, bulletId, side)
                {
                    bulletsToCreate = 3;
                    this.tripleshootstartime = startTime;
            }

            public override List<Bullet> Shoot(ref Tank t)
            {
                List<Bullet> bullets = new List<Bullet>();
                // context change
                // if 10 seconds from triple shoot powerup has passed, state changes to SimpleBullet state
                if ((DateTime.Now - tripleshootstartime).Seconds > 10)
                {
                    this._context.ChangeState(new SimpleBullet(x, y, width, height, speed, bulletId, side));
                    t.hasTripleShoot = false;
                    this.Shoot(ref t); // calls simple shoot bullet instead. There wont be recursion, because type changes
                }
                else
                {
                    int x_current = this.x;
                    int y_current = this.y;
                    for (int i = 0; i < bulletsToCreate; i++)
                    {
                        GetOffsetBulletCoordinates(side, ref x_current, ref y_current, i * 12);
                        Bullet b = new Bullet(x_current, y_current, width,
                            height, speed, bulletId++, side);
                        bullets.Add(b);
                    }
                }

                return bullets;
            }

            // offset - distance between two bullets, if triple shot
            private void GetOffsetBulletCoordinates(FacingSide side, ref int x, ref int y, int offset = 0)
            {

                if (side == FacingSide.Left)
                {
                    x -= offset;
                }
                else if (side == FacingSide.Right)
                {
                    x += offset;
                }
                else if (side == FacingSide.Down)
                {
                    y += offset;
                }
                else if (side == FacingSide.Up)
                {
                    y -= offset;
                }
            }
        }

        /// <summary>
        /// The 'Context' class
        /// </summary>
        public class BulletContext
        {
            private Bullet _state = null;

            // Constructor
            public BulletContext(Bullet state)
            {
                this.ChangeState(state);
            }

            public List<Bullet> RequestShoot(ref Tank t)
            {
                return _state.Shoot(ref t); // handle method for state
            }

            public void ChangeState(Bullet st)
            {
                this._state = st;
                this._state.SetContext(this);
            }
        }
    }


