
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
            components = new System.ComponentModel.Container();
            openMbs = new System.Windows.Forms.OpenFileDialog();
            propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            button1 = new System.Windows.Forms.Button();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            button2 = new System.Windows.Forms.Button();
            SaveFile = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            StencylVersion = new System.Windows.Forms.ComboBox();
            MbsVersion = new System.Windows.Forms.ComboBox();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            WireframeDrawPanel = new System.Windows.Forms.Panel();
            saveMbs = new System.Windows.Forms.SaveFileDialog();
            RefreshTimer = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // openMbs
            // 
            openMbs.FileName = "mbs";
            openMbs.Filter = "mbs scene files|scene-*.mbs|mbs behavior files|behaviors.mbs";
            // 
            // propertyGrid1
            // 
            propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            propertyGrid1.Location = new System.Drawing.Point(5, 5);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new System.Drawing.Size(384, 367);
            propertyGrid1.TabIndex = 0;
            propertyGrid1.PropertyValueChanged += propertyGrid1_PropertyValueChanged;
            propertyGrid1.SelectedGridItemChanged += propertyGrid1_SelectedGridItemChanged;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(182, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(89, 28);
            button1.TabIndex = 1;
            button1.Text = "Open file";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.056123F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.03061F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.2449F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.30612F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.06633F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.80612F));
            tableLayoutPanel1.Controls.Add(button2, 1, 0);
            tableLayoutPanel1.Controls.Add(SaveFile, 3, 0);
            tableLayoutPanel1.Controls.Add(button1, 2, 0);
            tableLayoutPanel1.Controls.Add(button3, 0, 0);
            tableLayoutPanel1.Controls.Add(MbsVersion, 5, 0);
            tableLayoutPanel1.Controls.Add(StencylVersion, 4, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(784, 34);
            tableLayoutPanel1.TabIndex = 2;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(73, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(103, 28);
            button2.TabIndex = 5;
            button2.Text = "Order actors";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // SaveFile
            // 
            SaveFile.Location = new System.Drawing.Point(277, 3);
            SaveFile.Name = "SaveFile";
            SaveFile.Size = new System.Drawing.Size(103, 28);
            SaveFile.TabIndex = 2;
            SaveFile.Text = "Save file";
            SaveFile.UseVisualStyleBackColor = true;
            SaveFile.Click += SaveFile_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(3, 3);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(64, 28);
            button3.TabIndex = 6;
            button3.Text = "Refresh";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // StencylVersion
            // 
            StencylVersion.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            StencylVersion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            StencylVersion.FormattingEnabled = true;
            StencylVersion.Items.AddRange(new object[] { "Newest stencyl version", "Dadish/Dadish2/Catbird version" });
            StencylVersion.Location = new System.Drawing.Point(396, 3);
            StencylVersion.Name = "StencylVersion";
            StencylVersion.Size = new System.Drawing.Size(166, 23);
            StencylVersion.TabIndex = 3;
            // 
            // MbsVersion
            // 
            MbsVersion.FormattingEnabled = true;
            MbsVersion.Items.AddRange(new object[] { "Mbs v2", "Mbs v1" });
            MbsVersion.Location = new System.Drawing.Point(568, 3);
            MbsVersion.Name = "MbsVersion";
            MbsVersion.Size = new System.Drawing.Size(213, 23);
            MbsVersion.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(propertyGrid1, 0, 0);
            tableLayoutPanel2.Controls.Add(WireframeDrawPanel, 1, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(0, 34);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(2);
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(784, 377);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // WireframeDrawPanel
            // 
            WireframeDrawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            WireframeDrawPanel.Location = new System.Drawing.Point(395, 5);
            WireframeDrawPanel.Name = "WireframeDrawPanel";
            WireframeDrawPanel.Padding = new System.Windows.Forms.Padding(2);
            WireframeDrawPanel.Size = new System.Drawing.Size(384, 367);
            WireframeDrawPanel.TabIndex = 1;
            WireframeDrawPanel.Paint += WireframeDisplay_Paint;
            WireframeDrawPanel.Resize += WireframeDrawPanel_Resize;
            // 
            // saveMbs
            // 
            saveMbs.Filter = "Scene file|scene-*.mbs|Other mbs file|*.mbs";
            // 
            // RefreshTimer
            // 
            RefreshTimer.Enabled = true;
            RefreshTimer.Tick += RefreshTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 411);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Mbs Editor V 1.5.1.0";
            Load += Form1_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
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
        private System.Windows.Forms.Timer RefreshTimer;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

