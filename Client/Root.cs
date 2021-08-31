using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public class Root
    {
        public Branch Branch { get; set; }
        public bool Configured { get; set; }
        public LastBuild LastBuild { get; set; }
        public string Trigger { get; set; }
    }
}
