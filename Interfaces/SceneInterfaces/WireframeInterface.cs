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
