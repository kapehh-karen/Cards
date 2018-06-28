using Core.Data.Field;
using Core.Data.Table;
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
    [KnownType(typeof(FontStyle))]
    [KnownType(typeof(Font))]
    [KnownType(typeof(Color))]
    [KnownType(typeof(GraphicsUnit))]
    public class PropertyData : ICloneable
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public object Value { get; set; }

        public object Clone()
        {
            return new PropertyData()
            {
                Name = this.Name,
                Value = this.Value
            };
        }
    }
}
