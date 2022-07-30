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
    public class ActorInterface
    {
        [DisplayName("Angle"), Category("Position")]
        public float angle { get; set; }
        [DisplayName("Actor ID"), Category("General data"), Description("A unique number for each actor in the scene")]
        public int aid { get; set; }
        [DisplayName("Customized"), Category("General data")]
        public bool customized { get; set; }
        [DisplayName("Group ID"), Category("General data")]
        public int groupID { get; set; }
        [DisplayName("Actor type"), Category("Gemeral data"), Description("The type of actor this is")]
        public int id { get; set; }
        [DisplayName("Name"), Category("General data")]
        public string name { get; set; }
        [DisplayName("Scale X"), Category("Position")]
        public float scaleX { get; set; }
        [DisplayName("Scale Y"), Category("Position")]
        public float scaleY { get; set; }
        [DisplayName("X"), Category("Position"), Description("The X-position of the top left corner")]
        public int x { get; set; }
        [DisplayName("Y"), Category("Position"), Description("The Y-position of the top left corner")]
        public int y { get; set; }
        [DisplayName("Z"), Category("Position")]
        public int z { get; set; }
        [DisplayName("Order In Layer"), Category("Position")]
        public int orderInLayer { get; set; }
        [DisplayName("Snippets"), Category("General data"), Description("AKA behaviors")]
        public Snippet[] snippets { get; set; }

        public ActorInterface(ActorInstance actor)
        {
            angle = actor.angle;
            aid = actor.aid;
            customized = actor.customized;
            groupID = actor.groupID;
            id = actor.id;
            name = actor.name;
            scaleX = actor.scaleX;
            scaleY = actor.scaleY;
            x = actor.x;
            y = actor.y;
            z = actor.z;
            orderInLayer = actor.orderInLayer;
            snippets = actor.behaviors.Select(i => new Snippet(i)).ToArray();
        }

        public ActorInterface()
        {
            snippets = new Snippet[0];
        }

        public override string ToString()
        {
            return $"Actor {aid} \"{name}\"";
        }

        public ActorInstance GetActorInstance()
        {
            return new ActorInstance(angle, aid, customized, groupID, id, name, scaleX, scaleY, x, y, z, orderInLayer, snippets.Select(i => i.GetBehaviorInstance()).ToArray());
        }
    }
}
