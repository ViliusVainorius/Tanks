using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Iterator
    {
        Collection collection;
        int current = 0;
        int step = 1;

        public Iterator(Collection collection)
        {
            this.collection = collection;
        }
        // Gets first item
        public GameObject First()
        {
            current = 0;
            return collection[current] as GameObject;
        }
        // Gets next item
        public GameObject Next()
        {
            current += step;
            while(!IsDone)
            {
                if (collection[current] != null)
                    return collection[current] as GameObject;

                current += step;
            }
            
            return null;
        }

        // Gets or sets stepsize
        public int Step
        {
            get { return step; }
            set { step = value; }
        }
        // Gets current iterator item
        public GameObject CurrentItem
        {
            get { return collection[current] as GameObject; }
        }
        // Gets whether iteration is complete
        public bool IsDone
        {
            get { return current >= collection.Count; }
        }
    }
}
