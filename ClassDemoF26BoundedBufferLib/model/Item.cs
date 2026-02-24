using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoF26BoundedBufferLib.model
{
    public class Item
    {
        private static int nextId = 1;

        public int Id{ get; }
        public int Value { get; set; }

        public Item(int value)
        {
            Id = nextId++;
            Value = value;
        }
        public Item():this(-1)
        {     
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Value)}={Value.ToString()}}}";
        }
    }
}
