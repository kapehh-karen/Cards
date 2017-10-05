using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Data.Design.InternalData
{
    [DataContract]
    [KnownType(typeof(Size))]
    [KnownType(typeof(Point))]
    public class PropertyData
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public object Value { get; set; }
    }
}
