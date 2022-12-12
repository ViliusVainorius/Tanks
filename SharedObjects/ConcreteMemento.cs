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

        public ConcreteMemento(string state)
        {
            _state = state;
        }

        public string GetState()
        {
            return this._state;
        }
    }
}
