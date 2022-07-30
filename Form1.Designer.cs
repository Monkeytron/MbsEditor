
namespace MbsEdit
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openMbs = new System.Windows.Forms.OpenFileDialog();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SaveFile = new System.Windows.Forms.Button();
            this.StencylVersion = new System.Windows.Forms.ComboBox();
            this.MbsVersion = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.WireframeDrawPanel = new System.Windows.Forms.Panel();
            this.saveMbs = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openMbs
            // 
            this.openMbs.FileName = "mbs";
            this.openMbs.Filter = "mbs scene files|scene-*.mbs";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(5, 5);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(384, 367);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            this.propertyGrid1.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.propertyGrid1_SelectedGridItemChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(133, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.73862F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.9898F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.13776F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.31633F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.22959F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.SaveFile, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.StencylVersion, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.MbsVersion, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 34);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // SaveFile
            // 
            this.SaveFile.Location = new System.Drawing.Point(226, 3);
            this.SaveFile.Name = "SaveFile";
            this.SaveFile.Size = new System.Drawing.Size(73, 28);
            this.SaveFile.TabIndex = 2;
            this.SaveFile.Text = "Save file";
            this.SaveFile.UseVisualStyleBackColor = true;
            this.SaveFile.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // StencylVersion
            // 
            this.StencylVersion.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.StencylVersion.FormattingEnabled = true;
            this.StencylVersion.Items.AddRange(new object[] {
            "Newest stencyl version",
            "Dadish/Dadish2/Catbird version"});
            this.StencylVersion.Location = new System.Drawing.Point(328, 3);
            this.StencylVersion.Name = "StencylVersion";
            this.StencylVersion.Size = new System.Drawing.Size(215, 23);
            this.StencylVersion.TabIndex = 3;
            // 
            // MbsVersion
            // 
            this.MbsVersion.FormattingEnabled = true;
            this.MbsVersion.Items.AddRange(new object[] {
            "Mbs v2",
            "Mbs v1"});
            this.MbsVersion.Location = new System.Drawing.Point(549, 3);
            this.MbsVersion.Name = "MbsVersion";
            this.MbsVersion.Size = new System.Drawing.Size(232, 23);
            this.MbsVersion.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.propertyGrid1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.WireframeDrawPanel, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 34);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(784, 377);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // WireframeDrawPanel
            // 
            this.WireframeDrawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WireframeDrawPanel.Location = new System.Drawing.Point(395, 5);
            this.WireframeDrawPanel.Name = "WireframeDrawPanel";
            this.WireframeDrawPanel.Padding = new System.Windows.Forms.Padding(2);
            this.WireframeDrawPanel.Size = new System.Drawing.Size(384, 367);
            this.WireframeDrawPanel.TabIndex = 1;
            this.WireframeDrawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.WireframeDisplay_Paint);
            this.WireframeDrawPanel.Resize += new System.EventHandler(this.WireframeDrawPanel_Resize);
            // 
            // saveMbs
            // 
            this.saveMbs.Filter = "Scene file|scene-*.mbs|Other mbs file|*.mbs";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openMbs;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel WireframeDrawPanel;
        private System.Windows.Forms.Button SaveFile;
        private System.Windows.Forms.SaveFileDialog saveMbs;
        private System.Windows.Forms.ComboBox StencylVersion;
        private System.Windows.Forms.ComboBox MbsVersion;
    }
}

