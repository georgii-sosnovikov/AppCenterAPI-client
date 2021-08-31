using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public class Branch
    {
        public string Name { get; set; }
        public Commit Commit { get; set; }
        public bool @protected { get; set; }
    }
}
