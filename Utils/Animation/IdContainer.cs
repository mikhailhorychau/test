using System.Collections.Generic;

namespace UIScripts.Utils.Animation
{
    public class IdContainer
    {
        private Queue<int> _empty = new Queue<int>();
        private int _count;

        public IdContainer()
        {
            _count = 0;
        }

        public int GetID() => _empty.Count > 0 ? _empty.Dequeue() : _count++;
        public void Release(int id) => _empty.Enqueue(id);
    }
}