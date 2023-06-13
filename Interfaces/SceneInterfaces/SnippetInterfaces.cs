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
        [Browsable(false)]
        private int snipNum;
        public void SetSnipNum(int num)
        {
            snipNum = num;
        }

        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Type")]
        public string type { get; set; }

        [DisplayName("Type")]
        public string mapType { get; }

        [DisplayName("Type")]

        public string listType { get; }

        public AttributeInterface(StencylAttribute sa, int snipnum = -1)
        {
            this.snipNum = snipnum;
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

            listType = "list";
            mapType = "map";
        }

        public AttributeInterface()
        {
            stringValue = "";
            type = "";
            valueType = DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Null];
            listValue = new DynamicList();
            mapValue = new DynamicMap();

            listType = "list";
            mapType = "map";
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
                        pds.Add(p);
                        break;
                    case "listType":
                        if (valueType == DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.List]) pds.Add(p);
                        break;
                    case "mapType":
                        if (valueType == DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Map]) pds.Add(p);
                        break;
                    case "type":
                        if (valueType != DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.List] && valueType != DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Map]) pds.Add(p);
                        break;
                }
            }
            return pds;
        }

        public override string ToString()
        {
            try
            {
                return $"{GlobalData.behaviors[snipNum].attributes.First(i => i.id == id).fullname}: {GetValue()}";
            }
            catch
            {
            }
            return $"Attribute {id} : {GetValue()}";
        }

        public StencylAttribute GetStencylAttribute()
        {
            string thisType = type;
            if (valueType == DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.List]) thisType = listType;
            if (valueType == DynamicTypeConverter.dynamicTypes[(int)DynamicTypes.Map]) thisType = mapType;
            try
            {
                return new StencylAttribute(id, thisType, GetMbsFriendlyDynamValue());
            }
            catch(Exception e)
            {
                throw new Exception(e.Message + $" in attribute {id}");
            }
        }
    }


    [TypeConverter(typeof(NiceObjectConverter))]
    public class Snippet
    {
        [DisplayName("Enabled")]
        public bool enabled { get; set; }

        [DisplayName("ID")]
        public int id { get; set; }


        [DisplayName("Attributes")]
        public AttributeInterface[] attributes { get; set; }

        public Snippet(BehaviorInstance behavior)
        {
            enabled = behavior.enabled;
            id = behavior.id;
            attributes = behavior.properties.Select(i => new AttributeInterface(i,id)).ToArray();

            GlobalData.refreshEvent += OnRefresh;
        }

        public Snippet()
        {
            enabled = true;
            id = -1;
            attributes = new AttributeInterface[0];

            GlobalData.refreshEvent += OnRefresh;
        }
        public override string ToString()
        {
            try
            {
                return GlobalData.behaviors[id].name;
            }
            catch
            {

            }
            return $"Snippet {id}";
        }

        public BehaviorInstance GetBehaviorInstance()
        {
            try
            {
                return new BehaviorInstance(enabled, id, attributes.Select(i => i.GetStencylAttribute()).ToArray());
            }
            catch(Exception e)
            {
                throw new Exception(e.Message + $" in snippet {id}");
            }
        }

        public void OnRefresh(object sender, EventArgs e)
        {
            foreach(AttributeInterface ai in attributes)
            {
                ai.SetSnipNum(id);
            }
        }
    }
}
