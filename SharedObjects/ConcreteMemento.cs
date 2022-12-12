using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    class ConcreteMemento : IMemento
    {
        private string _state;
        private Tank _tank;

        public ConcreteMemento(string state, Tank tank)
        {
            _state = state;
            _tank = tank;
        }

        public string GetState()
        {
            return this._state;
        }
    }
}
