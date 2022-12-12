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

        public Originator(string state)
        {
            _state = state;
        }

        public Tank SetState(string tankState, Tank tank)
        {
            this._state = tankState;
            if(_state == "Shot")
            {
                tank.speed = 1;

            }
            return tank;
        }

        public IMemento Save()
        {
            return new ConcreteMemento(this._state);
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
