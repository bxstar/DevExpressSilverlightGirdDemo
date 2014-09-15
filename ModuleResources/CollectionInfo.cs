using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridDemo
{
    public class CollectionInfo
    {
        public CollectionInfo(IEnumerable collection, string description)
        {
            this.Collection = collection;
            this.Description = description;
        }
        public IEnumerable Collection { get; private set; }
        public string Description { get; private set; }
    }
}
