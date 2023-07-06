using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

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
            propertyGrid1.PropertySort = PropertySort.Categorized;
            scene_select_dropdown.SelectedIndex = 0;
            DoubleBuffered = true;
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, WireframeDrawPanel, new object[] { true });
        }

        private void WireframeDisplay_Paint(object sender, PaintEventArgs e)
        {
            if (propertyGrid1.SelectedObject is SceneInterface && !Log_view.Visible)
            {
                WireframeDrawPanel.SuspendLayout();

                SceneInterface s = (SceneInterface)propertyGrid1.SelectedObject;

                float scalingFactor = MathF.Min((WireframeDrawPanel.Width - 4f) / s.width, (WireframeDrawPanel.Height - 4f) / s.height); //scale so that the entire scene fits into the editor window, with a 2px boundary.

                Pen wireframeP = new Pen(Color.Black, 2.0f); // black for the edges of the wireframe, which is the walls of the level.
                Pen selectP = new Pen(Color.Green, 3.0f);// green for a wireframe which is selected in the grid view
                Brush actorB = new SolidBrush(Color.BlueViolet);// purple for actors, which are just the things in the level (this is usually the top left corner, not taking into account the actor's shape)
                Brush highlightB = new SolidBrush(Color.Orange); //gold for an actor or corner of the wireframe which is currently selected.

                foreach (WireframeInterface w in s.terrain)
                {
                    if (w.corners.Length >= 2)
                    {
                        PointF[] newPoints = w.corners.Select(i => new PointF((i.x + w.position.x) * scalingFactor + 2, (i.y + w.position.y) * scalingFactor + 2)).ToArray();
                        //each point is given relative to the given position of the wireframe, and then must be scaled to fit into the display window (with a 2px boundary)
                        if (isContainedIn(propertyGrid1.SelectedGridItem, w))
                        {
                            e.Graphics.DrawLines(selectP, newPoints);
                            e.Graphics.DrawLine(selectP, newPoints[0], newPoints.Last());
                            for (int i = 0; i < w.corners.Length; i++)
                            {
                                if (isContainedIn(propertyGrid1.SelectedGridItem, w.corners[i]))
                                {
                                    e.Graphics.FillEllipse(highlightB, newPoints[i].X - 3, newPoints[i].Y - 3, 6, 6);//draw dot at selected corner
                                }
                            }
                        }
                        else
                        {
                            e.Graphics.DrawLines(wireframeP, newPoints);
                            e.Graphics.DrawLine(wireframeP, newPoints[0], newPoints.Last());
                        }
                    }
                } //draw the wireframes, this is the base immovable level collision.
                //the wireframes have no graphics in-game; to change how the terrain looks you need to edit the .scn file, not supported here.

                foreach (ActorInterface a in s.actors)
                {
                    if (isContainedIn(propertyGrid1.SelectedGridItem, a))
                    {
                        e.Graphics.FillEllipse(highlightB, a.x * scalingFactor + 2 - 4, a.y * scalingFactor + 2 - 4, 8, 8);
                    }
                    else
                    {
                        e.Graphics.FillEllipse(actorB, a.x * scalingFactor + 2 - 3, a.y * scalingFactor + 2 - 3, 6, 6);
                    }
                }//draw a little dot at the location of each actor.
                //the calculation is multiply by scaling factor, offset by 2 for 2px boundary, offset by -1 so that the dot of radius 1 is centred corrently.

                WireframeDrawPanel.ResumeLayout();
            }

        } //paint the panel on the right-hand side with lines and dots vaguely representing the level layout
        private void WireframeDrawPanel_Resize(object sender, EventArgs e)
        {
            WireframeDrawPanel.Refresh();
        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            unloggedEdits++;
            WireframeDrawPanel.Refresh();
            GlobalData.sceneEdited();
            DropdownRefresh();
        }
        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            WireframeDrawPanel.Refresh();
        }
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (GlobalData.callRefreshEvent())
            {
                propertyGrid1.Refresh();
                GlobalData.propertyRefreshFlag = false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // Methods that handle how the program looks.

        private void refresh_button_Click(object sender, EventArgs e)
        {
            GlobalData.callRefreshEvent();
            propertyGrid1.Refresh();
            WireframeDrawPanel.Refresh();
            Refresh();
        } // Refresh how everything looks
        private void order_actors_button_Click(object sender, EventArgs e)
        {
            if (propertyGrid1.SelectedObject is SceneInterface)
            {
                Dictionary<int, int> maxima = new Dictionary<int, int>();

                ActorInterface[] actors = ((SceneInterface)propertyGrid1.SelectedObject).actors;

                actors = actors.OrderBy(i => i.z).ThenBy(i => i.orderInLayer).ToArray();

                foreach (ActorInterface a in actors)
                {
                    if (maxima.ContainsKey(a.z)) a.orderInLayer = maxima[a.z]++;
                    else
                    {
                        maxima.Add(a.z, 0);
                        a.orderInLayer = maxima[a.z]++;
                    }
                }

                ((SceneInterface)propertyGrid1.SelectedObject).actors = actors;

                writeLog(true, "Ordered actors");
                propertyGrid1.Refresh();
                GlobalData.sceneEdited();
            }
        } // Makes sure each actor has a unique orderInLayer and z-position - if two actors had the same of both then stencyl might crash.
        private void save_as_button_Click(object sender, EventArgs e)
        {
            if (propertyGrid1.SelectedObject is SceneInterface)
            {
                if (saveMbs.ShowDialog() == DialogResult.OK)
                {
                    Scene sc = ((SceneInterface)propertyGrid1.SelectedObject).GetScene();
                    saveScene(sc, saveMbs.FileName);
                    writeLog(true, $"Opened {sc} from {saveMbs.FileName.Split('\\').Last()}");
                    DropdownRefresh();
                }
            }
            else
            {
                MessageBox.Show("You need to have a scene open to do this", "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        } // Save only the currently open scene.
        private void open_file_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (openMbs.ShowDialog() == DialogResult.OK)
                {
                    writeLog(true, $"Opened {openFile(openMbs.FileName)} from {openMbs.FileName.Split('\\').Last()}");
                    DropdownRefresh();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show($"An error occured when trying to open {openMbs.FileName}: \n\n{err}.", "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                writeLog(true, $"An error occured when trying to open {openMbs.FileName}: {err}");
            }
        }

        private void saveScene(Scene scene, string filePath)
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
            //Generally this doesnt change - just making sure the header of the mbs file displays the correct version.

            try
            {
                MbsWriter w = new MbsWriter(saveVersion.Item2, false, saveVersion.Item1);
                MbsScene writeTo = new MbsScene(w);

                writeTo.allocateNew();
                scene.WriteMbs(writeTo, false); //this is the line of code that writes the scene data - the rest is just required stuff.

                w.setRoot(writeTo);

                w.prepareForOutput();

                w.writeToFile(filePath);
                GlobalData.sceneSaved();
            }

            catch (Exception err)
            {
                MessageBox.Show("Error when saving scene" + scene.ToString(false, false, false, false, false) + ": \n" + err.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        } // Writes the scene currently open in propertygrid to a file path - simple!
        private string openFile(string filePath)
        {

            MbsReader reader = new MbsReader(MbsVersionControl.ALLCURRENTVERSIONS, false, true); //initialise a file reader
            (int, int) info = reader.readData(File.ReadAllBytes(filePath)); //read the version and typecode from the file
            dynamic r = reader.getRoot(); //r is the mbs object containing all the data of the file.
            if (r is MbsScene)
            {
                switch (info.Item1)
                {
                    case 1:
                        MbsVersion.SelectedIndex = 1; //mbsv1
                        break;
                    case 2:
                        MbsVersion.SelectedIndex = 0; //mbsv2 added null stuff
                        break;
                }
                switch (info.Item2)
                {
                    case -2033963926:
                        StencylVersion.SelectedIndex = 1; //dadish,catbird
                        break;
                    case -1349349184:
                        StencylVersion.SelectedIndex = 0; //latest version
                        break;
                }
                Scene sc = Scene.FromMbs(r); //turn the mbs object into a scene object, converting the raw binary data (mbs) into useful data.

                GlobalData.scenes.Add((sc, false));
                scene_select_dropdown.Items.Add($"{sc.id} \"{sc.name}\"");
                Refresh();
                return $"Scene {sc.id} \"{sc.name}\"";
            }//scene file = level data

            else if (r is MbsListBase)
            {
                switch (((MbsListBase)r).type.getName())
                {
                    case "MbsSnippetDef":
                        MbsList<MbsObject> snips = (MbsList<MbsObject>)r;
                        GlobalData.behaviors = new Dictionary<int, Behavior>();
                        for (int i = 0; i < snips.length(); i++)
                        {
                            Behavior b = Behavior.FromMbs((MbsSnippetDef)snips.getNextObject());
                            GlobalData.behaviors.Add(b.ID, b);
                        } //reads in the names of all behaviors and attributes (and other data about what sort of attributes each behavior has, but those arent used.)
                        break;
                    default:
                        throw new Exception($"Cannot currently open an mbs file of type MbsList<{((MbsListBase)r).type}>");
                }//behaviors file is in the format of a list of MbsSnippetDef
                propertyGrid1.Refresh();
                Refresh();
                return "Behavior file";
            }

            else
            {
                throw new Exception($"Cannot open an mbs file of type {r.GetType()}");
            }

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
        } // Checks whether child is a sub-property (at any number of layers) of the parent object.

        private void ChangeSelectedScene(int newSceneNum)
        {
            if (newSceneNum == -1)
            {
                propertyGrid1.SelectedObject = null;
                scene_select_dropdown.SelectedIndex = 0;
                GlobalData.currentScene = -1;
                return;
            }
            try
            {
                if (propertyGrid1.SelectedObject is null) propertyGrid1.SelectedObject = new SceneInterface(GlobalData.swapToNewScene(null, newSceneNum));
                else propertyGrid1.SelectedObject = new SceneInterface(GlobalData.swapToNewScene(((SceneInterface)propertyGrid1.SelectedObject).GetScene(), newSceneNum));
                {
                    string name = ((SceneInterface)propertyGrid1.SelectedObject).name;
                    if (Text.Contains(':'))
                    {
                        Text = Text.Split(':')[0] + $": {name}";
                    }
                    else Text = Text + $": {name}";
                }//updating the title of the editor window to match the name of loaded level
                propertyGrid1.Refresh();
                Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured when changing selected scene:" + e.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        // saves currently open scene to GlobalData, changes to the new scene and loads that into the propertygrid.

        private void open_folder_button_Click(object sender, EventArgs e)
        {
            if (Folder_browser.ShowDialog() == DialogResult.OK)
            {
                Log_view.Visible = true;
                string[] files = Directory.GetFiles(Folder_browser.SelectedPath).Where(file => file.EndsWith(".mbs")).ToArray();
                foreach (string file in files)
                {
                    try
                    {
                        writeLog(false, $"Opened {openFile(file)} from {file.Split('\\').Last()}");
                    }
                    catch (Exception err)
                    {
                        writeLog(false, $"{file.Split('\\').Last()} skipped because {err.Message}");
                    }
                }
                writeLog(true);
                DropdownRefresh();
                Log_view.Visible = false;
            }
        }

        private void scene_select_dropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            int selectedScene = scene_select_dropdown.SelectedIndex - 1; //The -1 comes from the fact that the first item in scene_select_dopdown is not a scene, offsetting all other indexes by 1.
            if (selectedScene >= 0)
            {
                ChangeSelectedScene(selectedScene);
                writeLog(true, $"Swapped selected scene to {getSceneText(GlobalData.scenes[GlobalData.currentScene].Item1)}");
            }
        }
        private void scene_select_dropdown_DrawItem(object sender, DrawItemEventArgs e)
        {
        }
        private void DropdownRefresh()
        {
            for (int i = 1; i < scene_select_dropdown.Items.Count; i++)
            {
                if (GlobalData.scenes[i - 1].Item2)
                {
                    string currentSceneText = (string)scene_select_dropdown.Items[i];
                    if (!currentSceneText.EndsWith("(edited)")) scene_select_dropdown.Items[i] = currentSceneText + " (edited)";
                }
            }
            scene_select_dropdown.Refresh();
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            if (GlobalData.currentScene < 0) return;
            if (GlobalData.scenes[GlobalData.currentScene].Item2)
            {
                DialogResult dr = MessageBox.Show("This scene has not been saved since your last edit, close anyway?", "Close without saving?", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.OK)
                {
                    return;
                }
            }
            ChangeSelectedScene(-1);
            writeLog(true, $"Closing {getSceneText(GlobalData.scenes[GlobalData.currentScene].Item1)}...");
            scene_select_dropdown.Items.RemoveAt(GlobalData.currentScene + 1);
            GlobalData.scenes.RemoveAt(GlobalData.currentScene);
            Refresh();
        }
        private void close_all_button_Click(object sender, EventArgs e)
        {
            ChangeSelectedScene(-1);
            if (GlobalData.scenes.Any(i => i.Item2))
            {
                MessageBox.Show("Some scenes have unsaved changes - these scenes will not be closed.", "Unsaved Changes", MessageBoxButtons.OK);
                List<int> okToClose = new List<int>();
                for (int i = 0; i < GlobalData.scenes.Count; i++)
                {
                    if (!GlobalData.scenes[i].Item2) okToClose.Add(i);
                } //Make a list of all the scenes which will be closed.
                for (int j = 0; j < okToClose.Count; j++)
                {
                    int actualIndex = okToClose[j] - j; // After some scenes have been removed, the index of all remaining scenes will be shifted.
                    writeLog(false, $"Closing {getSceneText(GlobalData.scenes[actualIndex].Item1)}...");
                    scene_select_dropdown.Items.RemoveAt(actualIndex + 1);
                    GlobalData.scenes.RemoveAt(actualIndex);
                }
            }
            else
            {
                while (GlobalData.scenes.Count > 0)
                {
                    writeLog(false, $"Closing {getSceneText(GlobalData.scenes[0].Item1)}...");
                    scene_select_dropdown.Items.RemoveAt(1);
                    GlobalData.scenes.RemoveAt(0);
                }
            }
            GlobalData.behaviors = new Dictionary<int, Behavior>();
            writeLog(true, "Closed behaviors file");
            Refresh();
        }
        private void swap_to_log_button_click(object sender, EventArgs e)
        {
            if (Log_view.Visible)
            {
                Log_view.Visible = false;
                swap_to_log_button.Text = "To log view";
            }
            else
            {
                Log_view.Visible = true;
                swap_to_log_button.Text = "To wireframe view";
            }
            WireframeDrawPanel.Refresh();
            Log_view.Refresh();
            Log_view.SelectionStart = Log_view.Text.Length;
            Log_view.ScrollToCaret();
        }

        private int unloggedEdits = 0; //Rather than logging every edit, count them and log them next time a different log is made.
        private void writeLog(bool endWithDivider, params string[] toLog)
        {
            Log_view.SuspendLayout();
            if (unloggedEdits > 0)
            {
                int x = unloggedEdits;
                unloggedEdits = 0;
                writeLog(true, $"{x} edits made");
            }
            if (toLog.Count() == 0 && endWithDivider == false) return; //If no message is logged, this is just an edits check. Don't write any other messages.

            Log_view.Lines = Log_view.Lines.Concat(toLog).ToArray();
            if (endWithDivider)
            {
                Log_view.Lines = Log_view.Lines.Append("-------------------------------------------------").ToArray();
                File.WriteAllLines("LogFile.txt", Log_view.Lines);
            }
            Log_view.SelectionStart = Log_view.Text.Length;
            Log_view.ScrollToCaret();
            Log_view.ResumeLayout();
        }

        private string getSceneText(Scene sc)
        {
            return $"Scene {sc.id} - \"{sc.name}\"";
        }

        private void help_button_Click(object sender, EventArgs e)
        {
            string[] helpText = Resources.HelpText.Split("||");
            int numOfPopups = helpText.Length;

            for (int i = 0; i < numOfPopups; i++)
            {
                DialogResult d = MessageBox.Show(helpText[i].Trim(), $"Help: {i + 1}/{numOfPopups}", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (d != DialogResult.OK) return;
            }
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
            if (refreshEvent is not null) refreshEvent(new object(), new EventArgs()); ;
            return propertyRefreshFlag;
        }

        public static void sceneEdited()
        {
            if (currentScene >= 0 && currentScene < scenes.Count)
            {
                if (!scenes[currentScene].Item2)
                {
                    scenes[currentScene] = (scenes[currentScene].Item1, true);
                }
            }
        } // Updates the flag to signify that the currently selected scene has been edited.
        public static void sceneSaved()
        {
            if (currentScene >= 0 && currentScene < scenes.Count)
            {
                if (scenes[currentScene].Item2)
                {
                    scenes[currentScene] = (scenes[currentScene].Item1, false);
                }
            }
        } // Updates the flag to signify that the currently selected scene has been edited.

        public static bool tryCommitScene(Scene scene)
        {
            if (currentScene >= 0 && currentScene < scenes.Count)
            {
                scenes[currentScene] = (scene, scenes[currentScene].Item2);
                return true;
            }
            else
            {
                return false;
            }
        } // Update globalData with any changes that have been made to the currently selected scene.

        public static Scene swapToNewScene(Scene oldScene, int newSceneNum)
        {
            if (newSceneNum >= 0 && newSceneNum < scenes.Count)
            {
                tryCommitScene(oldScene);
                currentScene = newSceneNum;
                return scenes[currentScene].Item1;
            }
            else
            {
                throw new Exception("New scene index outside of acceptable range");
            }
        }

        public static Dictionary<int, Behavior> behaviors = new Dictionary<int, Behavior>();
        public static List<(Scene, bool)> scenes = new List<(Scene, bool)>(); //bool is a flag as to whether that scene has been edited.
        public static int currentScene = -1; //Keep track of which scene is open at the moment.

    }
}
