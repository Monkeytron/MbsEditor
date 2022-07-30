using System;
using System.ComponentModel;
using System.Globalization;

using System.Linq;
using System.Reflection;

using System.Collections.Generic;

using haxe_mbs_translate.src.stencyl.behavior;
using haxe_mbs_translate.src.stencyl.models;
using haxe_mbs_translate.src.stencyl.models.scene;
using System.Collections;
using System.Drawing;

namespace MbsEdit.Interfaces.SceneInterfaces
{
    [TypeConverter(typeof(NiceObjectConverter))]
    public class AttributeInterface : DynamicInterface
    {

        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Type")]
        public string type { get; set; }

        public AttributeInterface(StencylAttribute sa)
        {
            id = sa.ID;
            type = sa.type;

            if(sa.value is bool)
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Bool];
                boolValue = sa.value;
            }
            else if(sa.value is float)
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Float];
                floatValue = sa.value;
            }
            else if(sa.value is int)
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Int];
                intValue = sa.value;
            }
            else if(sa.value is string)
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.String];
                stringValue = sa.value;
            }
            else if(sa.value is dynamic[])
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.List];
                listValue = new DynamicList(((dynamic[])sa.value).Select(i => new DynamicInterface(i)));
            }
            else if(sa.value is Dictionary<string, dynamic>)
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Map];
                mapValue = new DynamicMap(sa.value);
            }
            else
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Null];
                listValue = new DynamicList();
                mapValue = new DynamicMap();
            }
        }

        public AttributeInterface()
        {
            valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Null];
            listValue = new DynamicList();
            mapValue = new DynamicMap();
        }

        // Implementation of ICustomTypeDescriptor:
        public override PropertyDescriptorCollection GetProperties()
        {
            // Create a new collection object
            PropertyDescriptorCollection thispds = TypeDescriptor.GetProperties(this, true);
            PropertyDescriptorCollection pds = base.GetProperties();

            foreach (PropertyDescriptor p in thispds)
            {
                switch (p.Name)
                {
                    case "id":
                    case "type":
                        pds.Add(p);
                        break;
                }
            }
            return pds;
        }

        public override string ToString()
        {
            return $"Attribute {id} : {GetValue().ToString()}";
        }

        public StencylAttribute GetStencylAttribute()
        {
            return new StencylAttribute(id, type, GetMbsFriendlyDynamValue());
        }
    }


    [TypeConverter(typeof(NiceObjectConverter))]
    public class Snippet : CollectionBase, ICustomTypeDescriptor
    {
        [DisplayName("\tEnabled")]
        public bool enabled { get; set; }

        [DisplayName("\tID")]
        public int id { get; set; }

        [DisplayName("\tNo. of Attributes")]
        public int count { get { return Count; } }

        public Snippet(BehaviorInstance behavior)
        {
            foreach (StencylAttribute sa in behavior.properties)
            {
                Add(new AttributeInterface(sa));
            }
            enabled = behavior.enabled;
            id = behavior.id;
        }

        public Snippet()
        {
            enabled = false;
            id = -1;
        }

        public void Add(AttributeInterface ai)
        {
            this.List.Add(ai);
        }
        public void Remove(AttributeInterface ai)
        {
            this.List.Remove(ai);
        }
        public AttributeInterface this[int index]
        {
            get
            {
                return (AttributeInterface)this.List[index];
            }
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection thispds = TypeDescriptor.GetProperties(this, true);
            PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

            foreach(PropertyDescriptor pd in thispds)
            {
                if(pd.Name == "id"||pd.Name == "enabled"||pd.Name == "count")pds.Add(pd);
            }

            for (int i = 0; i < this.List.Count; i++)
            {
                SnippetPropertyDescriptor pd = new SnippetPropertyDescriptor(this, i);
                pds.Add(pd);
            }



            return pds;
        }

        public override string ToString()
        {
            return $"Snippet {id}";
        }

        public BehaviorInstance GetBehaviorInstance()
        {
            StencylAttribute[] atts = new StencylAttribute[Count];
            for(int i = 0; i < count; i++)
            {
                atts[i] = this[i].GetStencylAttribute();
            }

            return new BehaviorInstance(enabled, id, atts);
        }
    }

    public class SnippetPropertyDescriptor : PropertyDescriptor
    {
        private Snippet collection = null;
        private int index = -1;

        public SnippetPropertyDescriptor(Snippet coll, int idx) : base("Attribute #" + idx.ToString(), null)
        {
            collection = coll;
            index = idx;
        }

        public override AttributeCollection Attributes
        {
            get
            {
                return new AttributeCollection();
            }
        }
        public override bool CanResetValue(object component)
        {
            return true;
        }
        public override Type ComponentType
        {
            get
            {
                return this.collection.GetType();
            }
        }
        public override string DisplayName
        {
            get
            {
                return "Attribute #" + index.ToString();
            }
        }

        public override object GetValue(object component)
        {
            return this.collection[index];
        }
        public override bool IsReadOnly
        {
            get { return true; }
        }
        public override string Name
        {
            get { return "#" + index.ToString(); }
        }
        public override Type PropertyType
        {
            get { return this.collection[index].GetType(); }
        }
        public override void ResetValue(object component) { }
        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
        public override void SetValue(object component, object value)
        {
            // this.collection[index] = value;
        }
    }
}
