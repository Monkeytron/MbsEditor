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
    public enum LayerTypes
    {
        NotChosen = 0,
        ColorBackground = 1,
        ImageBackground = 2,
        InteractiveLayer = 3
    }

    public class LayerTypeConverter : StringConverter
    {
        public static string[] layerTypes = { "Please choose a layer type", "Color background", "Image background", "Interactive layer" };


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
            return new StandardValuesCollection(layerTypes);
        }
    }

    [TypeConverter(typeof(NiceObjectConverter))]
    public class LayerInterface : ICustomTypeDescriptor
    {
        [TypeConverter(typeof(LayerTypeConverter)), DisplayName("Layer Type"), Description("Choose which sort of layer this is"), RefreshProperties(RefreshProperties.All), Category("Control")]
        public string layerType { get; set; }

        [DisplayName("ID"), Category("General layer data")]
        public int id { get; set; }

        [DisplayName("Name"), Category("General layer data")]
        public string name { get; set; }
        [DisplayName("Order"), Category("General layer data")]
        public int order { get; set; }
        [DisplayName("Opacity"), Category("General layer data")]
        public int opacity { get; set; }
        [DisplayName("Blendmode"), Category("General layer data")]
        public string blendmode { get; set; }
        [DisplayName("Scroll factor X"), Category("General layer data")]
        public float scrollFactorX { get; set; }
        [DisplayName("Scroll factor Y"), Category("General layer data")]
        public float scrollFactorY { get; set; }
        [DisplayName("Visible"), Category("General layer data")]
        public bool visible { get; set; }
        [DisplayName("Locked"), Category("General layer data")]
        public bool locked { get; set; }
        [DisplayName("Color"), Category("Specific to this layer type")]
        public int color { get; set; }
        [DisplayName("Resource ID"), Category("Specific to this layer type")]
        public int resourceID { get; set; }
        [DisplayName("Custom scroll"), Category("Specific to this layer type")]
        public bool customScroll { get; set; }


        public LayerInterface(dynamic d)
        {
            if(d is ColorBackground)
            {
                layerType = LayerTypeConverter.layerTypes[(int)LayerTypes.ColorBackground];
                color = d.color;
            }
            else if(d is ImageBackground)
            {
                layerType = LayerTypeConverter.layerTypes[(int)LayerTypes.ImageBackground];
                id = d.id;
                name = d.name;
                order = d.order;
                opacity = d.opacity;
                blendmode = d.blendmode;
                scrollFactorX = d.scrollFactorX;
                scrollFactorY = d.scrollFactorY;
                visible = d.visible;
                locked = d.locked;

                resourceID = d.resourceID;
                customScroll = d.customScroll;
            }
            else if(d is InteractiveLayer)
            {
                layerType = LayerTypeConverter.layerTypes[(int)LayerTypes.InteractiveLayer];
                id = d.id;
                name = d.name;
                order = d.order;
                opacity = d.opacity;
                blendmode = d.blendmode;
                scrollFactorX = d.scrollFactorX;
                scrollFactorY = d.scrollFactorY;
                visible = d.visible;
                locked = d.locked;

                color = d.color;
            }
            else
            {
                layerType = LayerTypeConverter.layerTypes[(int)LayerTypes.NotChosen];
            }
        }

        public LayerInterface()
        {
            layerType = LayerTypeConverter.layerTypes[(int)LayerTypes.NotChosen];
        }

        public dynamic GetLayer()
        {
            if (layerType == LayerTypeConverter.layerTypes[(int)LayerTypes.ColorBackground])
            {
                return new ColorBackground(color);
            }
            else if (layerType == LayerTypeConverter.layerTypes[(int)LayerTypes.ImageBackground])
            {
                return new ImageBackground(id, name, order, opacity, blendmode, scrollFactorX, scrollFactorY, visible, locked, resourceID, customScroll);
            }
            else if (layerType == LayerTypeConverter.layerTypes[(int)LayerTypes.InteractiveLayer])
            {
                return new InteractiveLayer(id, name, order, opacity, blendmode, scrollFactorX, scrollFactorY, visible, locked, color);
            }
            else
            {
                return null;
            }
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

        public PropertyDescriptorCollection GetProperties()
        {
            // Create a new collection object
            PropertyDescriptorCollection thispds = TypeDescriptor.GetProperties(this, true);
            PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

            foreach(PropertyDescriptor p in thispds)
            {
                switch (p.Name)
                {
                    case "id":
                    case "name":
                    case "order":
                    case "opacity":
                    case "blendmode":
                    case "scrollFactorX":
                    case "scrollFactorY":
                    case "visible":
                    case "locked":
                        if (layerType == LayerTypeConverter.layerTypes[(int)LayerTypes.ImageBackground] || layerType == LayerTypeConverter.layerTypes[(int)LayerTypes.InteractiveLayer]) pds.Add(p);
                        break;
                    case "color":
                        if (layerType == LayerTypeConverter.layerTypes[(int)LayerTypes.InteractiveLayer] || layerType == LayerTypeConverter.layerTypes[(int)LayerTypes.ColorBackground]) pds.Add(p);
                        break;
                    case "resourceID":
                    case "customScroll":
                        if (layerType == LayerTypeConverter.layerTypes[(int)LayerTypes.ImageBackground]) pds.Add(p);
                        break;
                    case "layerType":
                        pds.Add(p);
                        break;
                    case "default":
                        throw new Exception("idk " + p.Name);
                }
            }
            return pds;
        }

        public override string ToString()
        {
            return layerType;
        }
    }

    public class LayerCollection : CollectionBase, ICustomTypeDescriptor
    {
        public LayerCollection(ICollection<LayerInterface> layers)
        {
            foreach(LayerInterface l in layers)
            {
                Add(l);
            }
        }
        public LayerCollection(IEnumerable<LayerInterface> layers)
        {
            foreach (LayerInterface l in layers)
            {
                Add(l);
            }
        }

        public void Add(LayerInterface li)
        {
            this.List.Add(li);
        }
        public void Remove(LayerInterface li)
        {
            this.List.Remove(li);
        }
        public LayerInterface this[int index]
        {
            get
            {
                return (LayerInterface)this.List[index];
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
                LayerCollectionPropertyDescriptor pd = new LayerCollectionPropertyDescriptor(this, i);
                pds.Add(pd);
            }
            return pds;
        }

        public override string ToString()
        {
            return $"{Count} layers";
        }
    }

    public class LayerCollectionPropertyDescriptor : PropertyDescriptor
    {
        private LayerCollection collection = null;
        private int index = -1;

        public LayerCollectionPropertyDescriptor(LayerCollection coll, int idx) : base("Layer " + idx.ToString(), null)
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
                return "Layer " + index.ToString();
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
