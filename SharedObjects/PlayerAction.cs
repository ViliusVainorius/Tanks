using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class PlayerAction
    {
        public ActionType type;
        public FacingSide side;

        public PlayerAction(ActionType type, FacingSide side)
        {
            this.type = type;
            this.side = side;
        }
    }

    public enum FacingSide
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum ActionType
    {
        Move,
        Shoot
    }
}
