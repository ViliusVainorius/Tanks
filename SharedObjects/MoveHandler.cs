using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class MoveHandler : Handler
    {
        public override void HandleRequest(PlayerAction action, ref Tank tank)
        {
            if (action.type == ActionType.Move)
            {
                ActionController controller = new ActionController();

                if (action.side == FacingSide.Right)
                {
                    controller.SetCommand(new CommandMoveRight(tank));
                    tank.Rotation = 90;
                }
                else if (action.side == FacingSide.Left)
                {
                    controller.SetCommand(new CommandMoveLeft(tank));
                    tank.Rotation = -90;
                }
                else if (action.side == FacingSide.Up)
                {
                    controller.SetCommand(new CommandMoveUp(tank));
                    tank.Rotation = 0;
                }
                else if (action.side == FacingSide.Down)
                {
                    controller.SetCommand(new CommandMoveDown(tank));
                    tank.Rotation = -180;
                }

                controller.Execute();
            }
            else
            {
                successor.HandleRequest(action, ref tank);
            }
        }
    }
}
