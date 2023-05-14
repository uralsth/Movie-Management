using System;
using System.Collections.Generic;
using System.Text;

namespace SQLHelper
{
    public class KeyValue
    {
        public KeyValue() { }
        public KeyValue(string key,object value) {
            Key = key;
            Value = value;
        }

       public string Key { get; set; }
       public object Value { get; set; }
    }
}
