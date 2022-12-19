using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Collection
    {
        List<GameObject> gameObjects;

        public Collection()
        {
            gameObjects = new List<GameObject>();
        }

        public Collection(GameObject[] gameObjects)
        {
            this.gameObjects = gameObjects.ToList();
        }

        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }
        // Gets item count
        public int Count
        {
            get { return gameObjects.Count; }
        }
        // Indexer
        public GameObject this[int index]
        {
            get { return gameObjects[index]; }
            set { gameObjects.Add(value); }
        }
    }
}
