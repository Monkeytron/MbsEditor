using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using haxe_mbs_translate.src.stencyl.behavior;
using haxe_mbs_translate.src.stencyl.models;
using haxe_mbs_translate.src.stencyl.models.scene;


namespace MbsEdit.Interfaces.SceneInterfaces
{
    public class SceneInterface
    {
        [DisplayName("Retain atlases"), Category("Other data")]
        public bool retainAtlases { get; set; }
        [DisplayName("Depth"), Category("Dimensions")]
        public int depth { get; set; }
        [DisplayName("Description"), Category("General info")]
        public string description { get; set; }
        [DisplayName("Event snippet ID"), Category("Other data")]
        public int eventSnippetID { get; set; }
        [DisplayName("Extended height"), Category("Dimensions")]
        public int extendedHeight { get; set; }
        [DisplayName("Extended width"), Category("Dimensions")]
        public int extendedWidth { get; set; }
        [DisplayName("Extended X"), Category("Dimensions")]
        public int extendedX { get; set; }
        [DisplayName("Extended Y"), Category("Dimensions")]
        public int extendedY { get; set; }
        [DisplayName("Format"), Category("General info")]
        public string format { get; set; }
        [DisplayName("Gravity"), Category("Other data")]
        public Point gravity { get; set; }
        [DisplayName("Height"), Category("Dimensions")]
        public int height { get; set; }
        [DisplayName("Scene ID"),Category("General info")]
        public int id { get; set; }
        [DisplayName("Name"), Category("General info")]
        public string name { get; set; }
        [DisplayName("Revision"), Category("General info")]
        public string revision { get; set; }
        [DisplayName("Save count"), Category("Other data")]
        public int savecount { get; set; }
        [DisplayName("Tile depth"), Category("Dimensions")]
        public int tileDepth { get; set; }
        [DisplayName("Tile height"), Category("Dimensions")]
        public int tileHeight { get; set; }
        [DisplayName("Tile width"), Category("Dimensions")]
        public int tileWidth { get; set; }
        [DisplayName("Type"), Category("General info")]
        public string type { get; set; }
        [DisplayName("Width"), Category("Dimensions")]
        public int width { get; set; }


        [DisplayName("Snippets"), Category("Lists")]
        public Snippet[] snippets { get; set; }

        [DisplayName("Actors"), Category("Lists")]
        public ActorInterface[] actors { get; set; }

        [DisplayName("Atlas members"), Category("Lists")]
        public int[] atlasMembers { get; set; }

        [TypeConverter(typeof(NiceObjectConverter)), DisplayName("Layers"), Category("Lists"), RefreshProperties(RefreshProperties.All)]
        public LayerCollection layerCollection { get; set; }

        [TypeConverter(typeof(NiceObjectConverter)), DisplayName("Terrain"), Category("Lists"), RefreshProperties(RefreshProperties.All)]
        public Terrain terrain { get; set; }

        public SceneInterface(Scene s)
        {
            retainAtlases = s.retainAtlases;
            depth = s.depth;
            description = s.description;
            eventSnippetID = s.eventSnippetID;
            extendedWidth = s.extendedWidth;
            extendedHeight = s.extendedHeight;
            extendedX = s.extendedX;
            extendedY = s.extendedY;
            format = s.format;
            gravity = new Point(s.gravityX, s.gravityY);
            height = s.height;
            id = s.id;
            name = s.name;
            revision = s.revision;
            savecount = s.savecount;
            tileDepth = s.tileDepth;
            tileHeight = s.tileHeight;
            tileWidth = s.tileWidth;
            type = s.type;
            width = s.width;

            snippets = s.snippets.Select(i => new Snippet(i)).ToArray();

            actors = s.actorInstances.Select(i => new ActorInterface(i)).ToArray();

            atlasMembers = s.atlasMembers;

            layerCollection = new LayerCollection(s.layers.Select(i => new LayerInterface(i)));

            terrain = new Terrain(s.terrain.Select(i => new WireframeInterface(i)));
        }


        public Scene GetScene()
        {
            Wireframe[] newTerrain = new Wireframe[terrain.Count]; for (int i = 0; i < terrain.Count; i++) newTerrain[i] = terrain[i].GetWireframe();
            dynamic[] newLayers = new dynamic[layerCollection.Count]; for (int i = 0; i < layerCollection.Count; i++) newLayers[i] = layerCollection[i].GetLayer();

            return new Scene(retainAtlases, depth, description, eventSnippetID, extendedWidth, extendedHeight, extendedX, extendedY, format, gravity.x, gravity.y, height, id,
                name, revision, savecount, tileDepth, tileHeight, tileWidth, type, width, actors.Select(i => i.GetActorInstance()).ToArray(), atlasMembers, newLayers, new dynamic[0], new dynamic[0], snippets.Select(i => i.GetBehaviorInstance()).ToArray(),newTerrain, new dynamic[0]);
        }
    }
}
