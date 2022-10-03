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
        public int varied;

        public PlayerAction(ActionType type, int varied)
        {
            this.type = type;
            this.varied = varied;
        }
    }

    public enum MoveSide
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum ActionType
    {
        move
    }
}
