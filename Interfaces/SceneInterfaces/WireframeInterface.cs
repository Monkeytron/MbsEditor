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
    public class WireframeInterface
    {
        [DisplayName("Position"), Description("This gets added to all the corners in the shape in order to get their actual position within the scene.")]
        public Point position { get; set; }

        [DisplayName("Corners"), Description("A set of points which, when connected together in order, define the outline of a shape.")]
        public Point[] corners { get; set; }

        public WireframeInterface(Wireframe w)
        {
            position = w.position;

            corners = w.points.Select(i => (Point)i).ToArray();
        }

        public WireframeInterface()
        {
            position = Point.Zero;
            corners = new Point[0];
        }

        public Wireframe GetWireframe()
        {
            return new Wireframe(position, corners.Select(i => (PointF)i).ToArray());
        }

        public override string ToString()
        {
            return $"Wireframe at {position} : {corners.Length} corners";
        }
    }

    public class Terrain : CollectionBase, ICustomTypeDescriptor
    {
        public Terrain(ICollection<WireframeInterface> terrain)
        {
            foreach (WireframeInterface w in terrain)
            {
                Add(w);
            }
        }
        public Terrain(IEnumerable<WireframeInterface> terrain)
        {
            foreach (WireframeInterface w in terrain)
            {
                Add(w);
            }
        }

        public void Add(WireframeInterface w)
        {
            this.List.Add(w);
        }
        public void Remove(WireframeInterface w)
        {
            this.List.Remove(w);
        }
        public WireframeInterface this[int index]
        {
            get
            {
                return (WireframeInterface)this.List[index];
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
                TerrainItemPropertyDescriptor pd = new TerrainItemPropertyDescriptor(this, i);
                pds.Add(pd);
            }
            return pds;
        }

        public override string ToString()
        {
            return $"{Count} wireframes";
        }
    }

    public class TerrainItemPropertyDescriptor : PropertyDescriptor
    {
        private Terrain collection = null;
        private int index = -1;

        public TerrainItemPropertyDescriptor(Terrain coll, int idx) : base("Wireframe " + idx.ToString(), null)
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
                return "Wireframe " + index.ToString();
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
    public class Point
    {
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Point()
        {
            x = 0; y = 0;
        }

        public float x { get; set; }
        public float y { get; set; }

        public override string ToString()
        {
            return $"({x},{y})";
        }

        public static implicit operator Point(PointF p)
        {
            return new Point(p.X, p.Y);
        }
        public static implicit operator PointF(Point p)
        {
            return new PointF(p.x, p.y);
        }

        public static Point Zero = new Point(0, 0);
    }
    
}
