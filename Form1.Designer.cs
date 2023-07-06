
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
            open_file_button = new System.Windows.Forms.Button();
            top_row = new System.Windows.Forms.TableLayoutPanel();
            save_as_button = new System.Windows.Forms.Button();
            help_button = new System.Windows.Forms.Button();
            close_all_button = new System.Windows.Forms.Button();
            close_button = new System.Windows.Forms.Button();
            open_folder_button = new System.Windows.Forms.Button();
            swap_to_log_button = new System.Windows.Forms.Button();
            MbsVersion = new System.Windows.Forms.ComboBox();
            StencylVersion = new System.Windows.Forms.ComboBox();
            order_actors_button = new System.Windows.Forms.Button();
            refresh_button = new System.Windows.Forms.Button();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            WireframeDrawPanel = new System.Windows.Forms.Panel();
            Log_view = new System.Windows.Forms.TextBox();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            left_buttons = new System.Windows.Forms.TableLayoutPanel();
            scene_select_dropdown = new System.Windows.Forms.ComboBox();
            saveMbs = new System.Windows.Forms.SaveFileDialog();
            RefreshTimer = new System.Windows.Forms.Timer(components);
            Folder_browser = new System.Windows.Forms.FolderBrowserDialog();
            top_row.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            WireframeDrawPanel.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            left_buttons.SuspendLayout();
            SuspendLayout();
            // 
            // openMbs
            // 
            openMbs.FileName = "mbs";
            openMbs.Filter = "mbs file|*.mbs|mbs scene files|scene-*.mbs|mbs behavior files|behaviors.mbs";
            // 
            // propertyGrid1
            // 
            propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            propertyGrid1.Location = new System.Drawing.Point(5, 40);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new System.Drawing.Size(381, 332);
            propertyGrid1.TabIndex = 0;
            propertyGrid1.PropertyValueChanged += propertyGrid1_PropertyValueChanged;
            propertyGrid1.SelectedGridItemChanged += propertyGrid1_SelectedGridItemChanged;
            // 
            // open_file_button
            // 
            open_file_button.Dock = System.Windows.Forms.DockStyle.Fill;
            open_file_button.Location = new System.Drawing.Point(3, 3);
            open_file_button.Name = "open_file_button";
            open_file_button.Size = new System.Drawing.Size(74, 28);
            open_file_button.TabIndex = 1;
            open_file_button.Text = "Open file";
            open_file_button.UseVisualStyleBackColor = true;
            open_file_button.Click += open_file_button_Click;
            // 
            // top_row
            // 
            top_row.BackColor = System.Drawing.Color.Transparent;
            top_row.ColumnCount = 9;
            top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            top_row.Controls.Add(open_file_button, 0, 0);
            top_row.Controls.Add(save_as_button, 1, 0);
            top_row.Controls.Add(help_button, 5, 0);
            top_row.Controls.Add(close_all_button, 4, 0);
            top_row.Controls.Add(close_button, 2, 0);
            top_row.Controls.Add(open_folder_button, 3, 0);
            top_row.Controls.Add(swap_to_log_button, 7, 0);
            top_row.Dock = System.Windows.Forms.DockStyle.Top;
            top_row.Location = new System.Drawing.Point(0, 0);
            top_row.Name = "top_row";
            top_row.RowCount = 1;
            top_row.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            top_row.Size = new System.Drawing.Size(784, 34);
            top_row.TabIndex = 2;
            // 
            // save_as_button
            // 
            save_as_button.Dock = System.Windows.Forms.DockStyle.Fill;
            save_as_button.Location = new System.Drawing.Point(83, 3);
            save_as_button.Name = "save_as_button";
            save_as_button.Size = new System.Drawing.Size(64, 28);
            save_as_button.TabIndex = 6;
            save_as_button.Text = "Save as";
            save_as_button.UseVisualStyleBackColor = true;
            save_as_button.Click += save_as_button_Click;
            // 
            // help_button
            // 
            help_button.Dock = System.Windows.Forms.DockStyle.Fill;
            help_button.Location = new System.Drawing.Point(388, 3);
            help_button.Name = "help_button";
            help_button.Size = new System.Drawing.Size(44, 28);
            help_button.TabIndex = 6;
            help_button.Text = "Help";
            help_button.UseVisualStyleBackColor = true;
            help_button.Click += help_button_Click;
            // 
            // close_all_button
            // 
            close_all_button.Dock = System.Windows.Forms.DockStyle.Fill;
            close_all_button.Location = new System.Drawing.Point(313, 3);
            close_all_button.Name = "close_all_button";
            close_all_button.Size = new System.Drawing.Size(69, 28);
            close_all_button.TabIndex = 5;
            close_all_button.Text = "Close all";
            close_all_button.UseVisualStyleBackColor = true;
            close_all_button.Click += close_all_button_Click;
            // 
            // close_button
            // 
            close_button.Dock = System.Windows.Forms.DockStyle.Fill;
            close_button.Location = new System.Drawing.Point(153, 3);
            close_button.Name = "close_button";
            close_button.Size = new System.Drawing.Size(64, 28);
            close_button.TabIndex = 7;
            close_button.Text = "Close file";
            close_button.UseVisualStyleBackColor = true;
            close_button.Click += close_button_Click;
            // 
            // open_folder_button
            // 
            open_folder_button.Dock = System.Windows.Forms.DockStyle.Fill;
            open_folder_button.Location = new System.Drawing.Point(223, 3);
            open_folder_button.Name = "open_folder_button";
            open_folder_button.Size = new System.Drawing.Size(84, 28);
            open_folder_button.TabIndex = 8;
            open_folder_button.Text = "Open folder";
            open_folder_button.UseVisualStyleBackColor = true;
            open_folder_button.Click += open_folder_button_Click;
            // 
            // swap_to_log_button
            // 
            swap_to_log_button.Dock = System.Windows.Forms.DockStyle.Fill;
            swap_to_log_button.Location = new System.Drawing.Point(438, 3);
            swap_to_log_button.Name = "swap_to_log_button";
            swap_to_log_button.Size = new System.Drawing.Size(123, 28);
            swap_to_log_button.TabIndex = 9;
            swap_to_log_button.Text = "To log view";
            swap_to_log_button.UseVisualStyleBackColor = true;
            swap_to_log_button.Click += swap_to_log_button_click;
            // 
            // MbsVersion
            // 
            MbsVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            MbsVersion.FormattingEnabled = true;
            MbsVersion.Items.AddRange(new object[] { "Mbs v2", "Mbs v1" });
            MbsVersion.Location = new System.Drawing.Point(226, 3);
            MbsVersion.Name = "MbsVersion";
            MbsVersion.Size = new System.Drawing.Size(158, 23);
            MbsVersion.TabIndex = 4;
            // 
            // StencylVersion
            // 
            StencylVersion.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            StencylVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            StencylVersion.FormattingEnabled = true;
            StencylVersion.Items.AddRange(new object[] { "Newest stencyl version", "Dadish/Dadish2/Catbird version" });
            StencylVersion.Location = new System.Drawing.Point(3, 3);
            StencylVersion.Name = "StencylVersion";
            StencylVersion.Size = new System.Drawing.Size(217, 23);
            StencylVersion.TabIndex = 3;
            // 
            // order_actors_button
            // 
            order_actors_button.Dock = System.Windows.Forms.DockStyle.Fill;
            order_actors_button.Location = new System.Drawing.Point(294, 3);
            order_actors_button.Name = "order_actors_button";
            order_actors_button.Size = new System.Drawing.Size(84, 23);
            order_actors_button.TabIndex = 5;
            order_actors_button.Text = "Order actors";
            order_actors_button.UseVisualStyleBackColor = true;
            order_actors_button.Click += order_actors_button_Click;
            // 
            // refresh_button
            // 
            refresh_button.Dock = System.Windows.Forms.DockStyle.Fill;
            refresh_button.Location = new System.Drawing.Point(216, 3);
            refresh_button.Name = "refresh_button";
            refresh_button.Size = new System.Drawing.Size(72, 23);
            refresh_button.TabIndex = 6;
            refresh_button.Text = "Refresh";
            refresh_button.UseVisualStyleBackColor = true;
            refresh_button.Click += refresh_button_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.6153831F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.3846169F));
            tableLayoutPanel2.Controls.Add(WireframeDrawPanel, 1, 1);
            tableLayoutPanel2.Controls.Add(propertyGrid1, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel2.Controls.Add(left_buttons, 0, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(0, 34);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(2);
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(784, 377);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // WireframeDrawPanel
            // 
            WireframeDrawPanel.Controls.Add(Log_view);
            WireframeDrawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            WireframeDrawPanel.Location = new System.Drawing.Point(392, 40);
            WireframeDrawPanel.Name = "WireframeDrawPanel";
            WireframeDrawPanel.Padding = new System.Windows.Forms.Padding(2);
            WireframeDrawPanel.Size = new System.Drawing.Size(387, 332);
            WireframeDrawPanel.TabIndex = 1;
            WireframeDrawPanel.Paint += WireframeDisplay_Paint;
            WireframeDrawPanel.Resize += WireframeDrawPanel_Resize;
            // 
            // Log_view
            // 
            Log_view.AcceptsReturn = true;
            Log_view.AcceptsTab = true;
            Log_view.Dock = System.Windows.Forms.DockStyle.Fill;
            Log_view.Location = new System.Drawing.Point(2, 2);
            Log_view.Margin = new System.Windows.Forms.Padding(0);
            Log_view.Multiline = true;
            Log_view.Name = "Log_view";
            Log_view.ReadOnly = true;
            Log_view.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            Log_view.Size = new System.Drawing.Size(383, 328);
            Log_view.TabIndex = 0;
            Log_view.Visible = false;
            Log_view.WordWrap = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.6227379F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.3772621F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 215F));
            tableLayoutPanel3.Controls.Add(StencylVersion, 0, 0);
            tableLayoutPanel3.Controls.Add(MbsVersion, 1, 0);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(392, 5);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            tableLayoutPanel3.Size = new System.Drawing.Size(387, 29);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // left_buttons
            // 
            left_buttons.ColumnCount = 3;
            left_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.19588F));
            left_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.8041229F));
            left_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            left_buttons.Controls.Add(refresh_button, 1, 0);
            left_buttons.Controls.Add(scene_select_dropdown, 0, 0);
            left_buttons.Controls.Add(order_actors_button, 2, 0);
            left_buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            left_buttons.Location = new System.Drawing.Point(5, 5);
            left_buttons.Name = "left_buttons";
            left_buttons.RowCount = 1;
            left_buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            left_buttons.Size = new System.Drawing.Size(381, 29);
            left_buttons.TabIndex = 3;
            // 
            // scene_select_dropdown
            // 
            scene_select_dropdown.Dock = System.Windows.Forms.DockStyle.Fill;
            scene_select_dropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            scene_select_dropdown.FormattingEnabled = true;
            scene_select_dropdown.Items.AddRange(new object[] { "--please select a scene to edit--" });
            scene_select_dropdown.Location = new System.Drawing.Point(3, 3);
            scene_select_dropdown.MaxDropDownItems = 20;
            scene_select_dropdown.Name = "scene_select_dropdown";
            scene_select_dropdown.Size = new System.Drawing.Size(207, 23);
            scene_select_dropdown.TabIndex = 5;
            scene_select_dropdown.DrawItem += scene_select_dropdown_DrawItem;
            scene_select_dropdown.SelectedValueChanged += scene_select_dropdown_SelectedValueChanged;
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
            // Folder_browser
            // 
            Folder_browser.Description = "Select folder to open .mbs files from";
            Folder_browser.UseDescriptionForTitle = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 411);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(top_row);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Mbs Editor V 1.6.0.0";
            Load += Form1_Load;
            top_row.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            WireframeDrawPanel.ResumeLayout(false);
            WireframeDrawPanel.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            left_buttons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openMbs;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button open_file_button;
        private System.Windows.Forms.TableLayoutPanel top_row;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel WireframeDrawPanel;
        private System.Windows.Forms.SaveFileDialog saveMbs;
        private System.Windows.Forms.ComboBox StencylVersion;
        private System.Windows.Forms.Timer RefreshTimer;
        private System.Windows.Forms.Button order_actors_button;
        private System.Windows.Forms.Button refresh_button;
        private System.Windows.Forms.ComboBox MbsVersion;
        private System.Windows.Forms.ComboBox scene_select_dropdown;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button close_all_button;
        private System.Windows.Forms.Button help_button;
        private System.Windows.Forms.TableLayoutPanel left_buttons;
        private System.Windows.Forms.Button save_as_button;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Button open_folder_button;
        private System.Windows.Forms.FolderBrowserDialog Folder_browser;
        private System.Windows.Forms.TextBox Log_view;
        private System.Windows.Forms.Button swap_to_log_button;
    }
}

