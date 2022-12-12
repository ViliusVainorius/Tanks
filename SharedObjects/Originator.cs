using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Originator
    {
        private string _state;
        private Tank _tank;

        public Originator(string state, Tank tank)
        {
            _state = state;
            _tank = tank;
        }

        public Tank SetState(string tankState, Tank tank, int i)
        {
            this._state = tankState;
            this._tank = tank;
            if(_state == "Shot")
            {
                if(i == 0)
                {
                    this._tank.X = 135;
                    this._tank.Y = 267;
                    this._tank.speed = 1;
                }
                else if(i == 1)
                {
                    this._tank.X = 635;
                    this._tank.Y = 167;
                    this._tank.speed = 1;
                }
                
            }
            else if (_state == "Broken")
            {
                this._tank.speed = 0;
            }
            else if (_state == "Healthy")
            {
                if (i == 0)
                    _tank.speed = 3;
                if (i == 1)
                    _tank.speed = 5;
            }
            else
            {
                this._tank = tank;
            }
            return this._tank;
        }

        public IMemento Save()
        {
            return new ConcreteMemento(this._state,this._tank);
        }

        public void Restore(IMemento memento)
        {
            if(!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            this._state = memento.GetState();
        }
    }
}
