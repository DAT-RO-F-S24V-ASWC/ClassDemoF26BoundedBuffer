using ClassDemoF26BoundedBufferLib.model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoF26BoundedBufferLib.threadSafe
{
    public class BoundedBuffer<T>
    {
        private readonly Queue<T> _buffer;
        private readonly Semaphore _full;
        private readonly Semaphore _empty;
        private readonly object _lockObj;

        public BoundedBuffer(int maxSize)
        {
            _buffer = new Queue<T>();
            _lockObj = new object();

            _full = new Semaphore(maxSize, maxSize);
            _empty = new Semaphore(0, maxSize);
        }

        public void Insert(T item)
        {
            _full.WaitOne();
            lock (_lockObj)
            {
                _buffer.Enqueue(item);
            }
            _empty.Release();
        }

        public T Take()
        {
            T item;
            _empty.WaitOne();
            lock (_lockObj)
            {
                item = _buffer.Dequeue();
            }
            _full.Release();
            return item;

        }
    }
}
