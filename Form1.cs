using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using haxe_mbs_translate.src.mbs.core;
using haxe_mbs_translate.src.mbs.io;
using haxe_mbs_translate.src.stencyl.behavior;
using haxe_mbs_translate.src.stencyl.io.mbs;
using haxe_mbs_translate.src.stencyl.io.mbs.scene;
using haxe_mbs_translate.src.stencyl.io.mbs.snippet;
using haxe_mbs_translate.src.stencyl.models;
using haxe_mbs_translate.src.stencyl.models.scene;

using MbsEdit.Interfaces.SceneInterfaces;

namespace MbsEdit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openMbs.ShowDialog() == DialogResult.OK)
            {
               // try
                {


                    MbsReader reader = new MbsReader(MbsVersionControl.ALLCURRENTVERSIONS, false, true);

                    (int, int) info = reader.readData(File.ReadAllBytes(openMbs.FileName));

                    dynamic r = reader.getRoot();
                    if (r is MbsScene)
                    {
                        switch (info.Item1)
                        {
                            case 1:
                                MbsVersion.SelectedIndex = 1;
                                break;
                            case 2:
                                MbsVersion.SelectedIndex = 0;
                                break;
                        }
                        switch (info.Item2)
                        {
                            case -2033963926:
                                StencylVersion.SelectedIndex = 1;
                                break;
                            case -1349349184:
                                StencylVersion.SelectedIndex = 0;
                                break;
                        }
                        propertyGrid1.PropertySort = PropertySort.Categorized;
                        propertyGrid1.SelectedObject = new SceneInterface(Scene.FromMbs(r));

                        propertyGrid1.CollapseAllGridItems();
                    }

                    else if (r is MbsListBase)
                    {
                        switch (((MbsListBase)r).type.getName())
                        {
                            case "MbsSnippetDef":
                                MbsList<MbsObject> snips = (MbsList<MbsObject>)r;
                                GlobalData.behaviors = new Dictionary<int, Behavior>();
                                for(int i = 0; i < snips.length(); i++)
                                {
                                    Behavior b = Behavior.FromMbs((MbsSnippetDef)snips.getNextObject());
                                    GlobalData.behaviors.Add(b.ID, b);
                                }
                                break;
                            default:
                                throw new Exception($"Cannot currently open an mbs file of type MbsList<{((MbsListBase)r).type}>");
                        }
                    }

                    else
                    {
                        throw new Exception($"Cannot open an mbs file of type {r.GetType()}");
                    }
                    Refresh();
                }
                //catch(Exception err)
                {
                   // MessageBox.Show($"An error occured when trying to open the file: \n\t{err}.", "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void WireframeDisplay_Paint(object sender, PaintEventArgs e)
        {
            if(propertyGrid1.SelectedObject is SceneInterface)
            {
                SceneInterface s = (SceneInterface)propertyGrid1.SelectedObject;

                Pen wireframeP = new Pen(Color.Black, 2.0f);
                Pen ActorP = new Pen(Color.BlueViolet, 3.0f);
                Pen selectP = new Pen(Color.Green, 3.0f);
                Pen highlightP = new Pen(Color.Orange, 6.0f);

                WireframeDrawPanel.SuspendLayout();

                foreach (WireframeInterface w in s.terrain)
                {
                    PointF[] newPoints = w.corners.Select(i => new PointF((i.x+w.position.x)*(WireframeDrawPanel.Width-4)/s.width+2,(i.y+w.position.y)*(WireframeDrawPanel.Height-4)/s.height+2)).ToArray();
                    if (isContainedIn(propertyGrid1.SelectedGridItem,w))
                    {
                        e.Graphics.DrawLines(selectP, newPoints);
                        e.Graphics.DrawLine(selectP, newPoints[0], newPoints.Last());
                        for(int i = 0; i < w.corners.Length; i++)
                        {
                            if(isContainedIn(propertyGrid1.SelectedGridItem, w.corners[i]))
                            {
                                e.Graphics.DrawEllipse(highlightP, newPoints[i].X-2, newPoints[i].Y-2, 4, 4);
                            }
                        }
                    }
                    else
                    {
                        e.Graphics.DrawLines(wireframeP, newPoints);
                        e.Graphics.DrawLine(wireframeP, newPoints[0], newPoints.Last());
                    }
                }

                foreach(ActorInterface a in s.actors)
                {
                    if (isContainedIn(propertyGrid1.SelectedGridItem, a))
                    {
                        e.Graphics.DrawEllipse(highlightP, a.x * (WireframeDrawPanel.Width - 4) / s.width + 2, a.y * (WireframeDrawPanel.Height - 4) / s.height + 2, 4, 4);
                    }
                    else
                    {
                        e.Graphics.DrawEllipse(ActorP, a.x * (WireframeDrawPanel.Width - 4) / s.width + 2, a.y * (WireframeDrawPanel.Height - 4) / s.height + 2, 4, 4);
                    }
                }

                WireframeDrawPanel.ResumeLayout();
            }
            
        }

        private void WireframeDrawPanel_Resize(object sender, EventArgs e)
        {
            WireframeDrawPanel.Refresh();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            WireframeDrawPanel.Refresh();
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            
            if (saveMbs.ShowDialog() == DialogResult.OK)
            {
                if (propertyGrid1.SelectedObject is SceneInterface)
                {
                    (int, int) saveVersion = (0, 0);
                    switch (MbsVersion.SelectedIndex)
                    {
                        case 0:
                            saveVersion.Item1 = 2;
                            break;
                        case 1:
                            saveVersion.Item1 = 1;
                            break;
                    }

                    switch (StencylVersion.SelectedIndex)
                    {
                        case 0:
                            saveVersion.Item2 = MbsVersionControl.LATESTVERSION.Item2;
                            break;
                        case 1:
                            saveVersion.Item2 = MbsVersionControl.DADISH.Item2;
                            break;
                    }

                    try
                    {
                        MbsWriter w = new MbsWriter(saveVersion.Item2, false, saveVersion.Item1);
                        MbsScene writeTo = new MbsScene(w);

                        writeTo.allocateNew();
                        ((SceneInterface)propertyGrid1.SelectedObject).GetScene().WriteMbs(writeTo, false); //

                        w.setRoot(writeTo);

                        w.prepareForOutput();

                        w.writeToFile(saveMbs.FileName);
                    }

                    catch(Exception err)
                    {
                        MessageBox.Show(err.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            
        }

        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            WireframeDrawPanel.Refresh();
        }


        public bool isContainedIn(GridItem child, object parent)
        {
            if (ReferenceEquals(child.Value, parent))
            {
                return true;
            }

            GridItem nextGen = child.Parent;

            while (nextGen is not null)
            {
                if (ReferenceEquals(nextGen.Value, parent)) return true;
                nextGen = nextGen.Parent;
            }

            return false;
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (GlobalData.callRefreshEvent())
            {
                propertyGrid1.Refresh();
                GlobalData.propertyRefreshFlag = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(propertyGrid1.SelectedObject is SceneInterface)
            {
                Dictionary<int, int> maxima = new Dictionary<int, int>();

                ActorInterface[] actors = ((SceneInterface)propertyGrid1.SelectedObject).actors;

                actors = actors.OrderBy(i => i.z).ThenBy(i => i.orderInLayer).ToArray();

                foreach(ActorInterface a in actors)
                {
                    if (maxima.ContainsKey(a.z)) a.orderInLayer = maxima[a.z]++;
                    else
                    {
                        maxima.Add(a.z, 0);
                        a.orderInLayer = maxima[a.z]++;
                    }
                }

                ((SceneInterface)propertyGrid1.SelectedObject).actors = actors;

                propertyGrid1.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GlobalData.callRefreshEvent();
            propertyGrid1.Refresh();
            WireframeDrawPanel.Refresh();
            Refresh();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public static class GlobalData
    {
        public static bool propertyRefreshFlag = false;
        public static SceneInterface prevScene;

        public static event EventHandler refreshEvent;

        public static bool callRefreshEvent()
        {
            if (propertyRefreshFlag) return true;
            if(refreshEvent is not null) refreshEvent(new object(), new EventArgs()); ;
            return propertyRefreshFlag;
        }

        public static Dictionary<int, Behavior> behaviors = new Dictionary<int, Behavior>();
    }
}
