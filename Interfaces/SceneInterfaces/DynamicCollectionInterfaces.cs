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

namespace MbsEdit.Interfaces.SceneInterfaces
{
    public enum DynamicTypes
    {
        Null = 0,
        Bool = 1,
        Float = 2,
        Int = 3,
        String = 4,
        List = 5,
        Map = 6
    }
    public class DynamicTypeConverter : StringConverter
    {
        public static string[] dynamicTypes = { "Null", "Boolean", "Float", "Integer", "String", "List", "Map" };


        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(dynamicTypes);
        }
    }


    [TypeConverter(typeof(NiceObjectConverter))]
    public class DynamicInterface : ICustomTypeDescriptor
    {
        [TypeConverter(typeof(DynamicTypeConverter)), DisplayName("Value Type"), Description("Choose what type the value is"), RefreshProperties(RefreshProperties.All)]
        public string valueType { get; set; }

        [DisplayName("Value")]
        public bool boolValue { get; set; }
        [DisplayName("Value")]
        public float floatValue { get; set; }
        [DisplayName("Value")]
        public int intValue { get; set; }
        [DisplayName("Value")]
        public string stringValue { get; set; }
        [DisplayName("Value"), TypeConverter(typeof(NiceObjectConverter))]
        public DynamicList listValue { get; set; }
        [DisplayName("Value"), TypeConverter(typeof(NiceObjectConverter))]
        public DynamicMap mapValue { get; set; }

        public DynamicInterface(dynamic d)
        {
            if (d is bool)
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Bool];
                boolValue = d;
            }
            else if (d is float)
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Float];
                floatValue = d;
            }
            else if (d is int)
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Int];
                intValue = d;
            }
            else if (d is string)
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.String];
                stringValue = d;
            }
            else if (d is dynamic[])
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.List];
                listValue = new DynamicList(((dynamic[])d).Select(i => new DynamicInterface(i)));
            }
            else if (d is Dictionary<string, dynamic>)
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Map];
                mapValue = new DynamicMap(d);
            }
            else
            {
                valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Null];
                listValue = new DynamicList();
                mapValue = new DynamicMap();
            }
        }

        public DynamicInterface()
        {
            valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Null];
            listValue = new DynamicList();
            mapValue = new DynamicMap();
        }

        // Implementation of ICustomTypeDescriptor:
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

        public virtual PropertyDescriptorCollection GetProperties()
        {
            // Create a new collection object
            PropertyDescriptorCollection thispds = TypeDescriptor.GetProperties(this, true);
            PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

            foreach (PropertyDescriptor p in thispds)
            {
                switch (p.Name)
                {
                    case "valueType":
                        pds.Add(p);
                        break;
                    case "boolValue":
                        if (valueType == DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Bool]) pds.Add(p);
                        break;
                    case "floatValue":
                        if (valueType == DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Float]) pds.Add(p);
                        break;
                    case "intValue":
                        if (valueType == DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Int]) pds.Add(p);
                        break;
                    case "stringValue":
                        if (valueType == DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.String]) pds.Add(p);
                        break;
                    case "listValue":
                        if (valueType == DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.List]) pds.Add(p);
                        break;
                    case "mapValue":
                        if (valueType == DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Map]) pds.Add(p);
                        break;
                    default:
                        break;
                }
            }
            return pds;
        }

        public override string ToString()
        {
            if (valueType is "Null") return "NULL";
            else return Convert.ToString(GetValue());
        }

        public dynamic GetValue()
        {
            switch (valueType)
            {
                case "Null":
                    return null;
                case "Boolean":
                    return boolValue;
                case "String":
                    return stringValue;
                case "Float":
                    return floatValue;
                case "Integer":
                    return intValue;
                case "List":
                    return listValue;
                case "Map":
                    return mapValue;
                default:
                    throw new Exception("idk " + valueType);
            }
        }

        public virtual dynamic GetMbsFriendlyDynamValue()
        {
            switch (valueType)
            {
                case "Null":
                    return null;
                case "Boolean":
                    return boolValue;
                case "String":
                    return stringValue;
                case "Float":
                    return floatValue;
                case "Integer":
                    return intValue;
                case "List":
                    dynamic[] listvalues = new dynamic[listValue.Count];
                    for(int i = 0; i < listValue.Count; i++)
                    {
                        listvalues[i] = listValue[i].GetMbsFriendlyDynamValue();
                    }
                    return listvalues;
                case "Map":
                    Dictionary<string, dynamic> mapvalues = new Dictionary<string, dynamic>();
                    foreach(DynamicMapElement dme in mapValue)
                    {
                        mapvalues.Add(dme.key, dme.value.GetMbsFriendlyDynamValue());
                    }
                    return mapvalues;
                default:
                    throw new Exception("idk " + valueType);
            }
        }
    }

    public class DynamicList : CollectionBase, ICustomTypeDescriptor
    {
        private int prevCount;
        public DynamicList(ICollection<DynamicInterface> values)
        {
            foreach (DynamicInterface d in values)
            {
                Add(d);
            }
            prevCount = Count;
            GlobalData.refreshEvent += OnRefreshEvent;
        }
        public DynamicList(IEnumerable<DynamicInterface> values)
        {
            foreach (DynamicInterface d in values)
            {
                Add(d);
            }
            prevCount = Count;
            GlobalData.refreshEvent += OnRefreshEvent;
        }

        private void OnRefreshEvent(object? sender, EventArgs e)
        {
            if (prevCount != Count)
            {
                GlobalData.propertyRefreshFlag = true;
                prevCount = Count;
            }
        }

        public DynamicList()
        {
            prevCount = Count;
            GlobalData.refreshEvent += OnRefreshEvent;
        }

        public void Add(DynamicInterface di)
        {
            this.List.Add(di);
        }
        public void Remove(DynamicInterface di)
        {
            this.List.Remove(di);
        }
        public DynamicInterface this[int index]
        {
            get
            {
                try
                {
                    return (DynamicInterface)this.List[index];
                }
                catch
                {
                    GlobalData.propertyRefreshFlag = true;
                    return new DynamicInterface();
                }
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
            PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

            for (int i = 0; i < this.List.Count; i++)
            {
                DynamicListPropertyDescriptor pd = new DynamicListPropertyDescriptor(this, i);
                pds.Add(pd);
            }
            return pds;
        }

        public override string ToString()
        {
            return $"List of {Count} values";
        }
    }

    public class DynamicListPropertyDescriptor : PropertyDescriptor
    {
        private DynamicList collection = null;
        private int index = -1;

        public DynamicListPropertyDescriptor(DynamicList coll, int idx) : base("Value " + idx.ToString(), null)
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
                return "Value " + index.ToString();
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


    public class DynamicMap : CollectionBase, ICustomTypeDescriptor
    {

        private int prevCount;
        public DynamicMap(Dictionary<string,dynamic> values)
        {
            foreach (string s in values.Keys)
            {
                Add(new DynamicMapElement(s, new DynamicInterface(values[s])));
            }
            prevCount = Count;
            GlobalData.refreshEvent += OnRefreshEvent;
        }
        public DynamicMap()
        {
            prevCount = Count;
            GlobalData.refreshEvent += OnRefreshEvent;
        }

        private void OnRefreshEvent(object? sender, EventArgs e)
        {
            if (prevCount != Count)
            {
                GlobalData.propertyRefreshFlag = true;
                prevCount = Count;
            }
        }

        public void Add(DynamicMapElement dme)
        {
            this.List.Add(dme);
        }
        public void Remove(DynamicMapElement dme)
        {
            this.List.Remove(dme);
        }
        public DynamicMapElement this[int index]
        {
            get
            {
                try
                {
                    return (DynamicMapElement)this.List[index];
                }
                catch
                {
                    GlobalData.propertyRefreshFlag = true;
                    return new DynamicMapElement();
                }
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
            PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

            for (int i = 0; i < this.List.Count; i++)
            {
                DynamicMapPropertyDescriptor pd = new DynamicMapPropertyDescriptor(this, i);
                pds.Add(pd);
            }
            return pds;
        }

        public override string ToString()
        {
            return $"Map of {Count} elements";
        }
    }

    public class DynamicMapPropertyDescriptor : PropertyDescriptor
    {
        private DynamicMap collection = null;
        private int index = -1;

        public DynamicMapPropertyDescriptor(DynamicMap coll, int idx) : base("Element " + idx.ToString(), null)
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
                return "Element " + index.ToString();
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


    [TypeConverter(typeof(NiceObjectConverter))]
    public class DynamicMapElement
    {
        [DisplayName("Key")]
        public string key { get; set; }

        [DisplayName("Value"), TypeConverter(typeof(ExpandableObjectConverter))]
        public DynamicInterface value { get; set; }


        public DynamicMapElement(string key, DynamicInterface value)
        {
            this.key = key;
            this.value = value;
        }

        public DynamicMapElement()
        {
            key = "";
            value = new DynamicInterface();
        }


        public override string ToString()
        {
            return $"{key} : {value.ToString()}";
        }
    }
}
