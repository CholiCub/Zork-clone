using System;
using System.Collections.Generic;
using System.Text;
using Zork;
namespace Zork.Common
{
    public class Inventory
    {
        public Item[] Items { get; private set; }

        public int capacity { get { return (Items == null) ? 0 : Items.Length; } }

        public Inventory(int capacity =4)
        {
            SetInventorySize(capacity);
        }
        public void SetInventorySize(int capacity)
        {
            //if(capacity<=0)
            //{
            //    Items = null;
            //}
            //else if (Items == null)
            //{
            //    Items=new int[]
            //}
        }
    }
}
