using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    public class KeyVal<Key, Val>
    {
        public Key Id { get; set; }
        public Val Text { get; set; }

        public KeyVal() { }

        public KeyVal(Key key, Val val)
        {
            this.Id = key;
            this.Text = val;
        }
    }
}
