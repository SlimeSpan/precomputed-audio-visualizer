namespace sound_visualization
{
    partial class Audio_visualizer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            closeB = new Button();
            timer = new System.Windows.Forms.Timer(components);
            volumeBar = new TrackBar();
            checkBoxCurve = new CheckBox();
            checkBoxRectangle = new CheckBox();
            visualizerP = new Panel();
            panelMenu = new Panel();
            labelVolume = new Label();
            labelHeight = new Label();
            heightBar = new TrackBar();
            TriggerPanel = new TransparentPanel();
            Spectrum = new SpectrumControl();
            AnimationTimer = new System.Windows.Forms.Timer(components);
            analyzeB = new Button();
            chooseFileB = new Button();
            textBox1 = new TextBox();
            initialP = new Panel();
            trackBar1 = new TrackBar();
            labelFilter = new Label();
            ((System.ComponentModel.ISupportInitialize)volumeBar).BeginInit();
            visualizerP.SuspendLayout();
            panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)heightBar).BeginInit();
            initialP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // closeB
            // 
            closeB.BackColor = SystemColors.ActiveBorder;
            closeB.Location = new Point(0, 2);
            closeB.Name = "closeB";
            closeB.Size = new Size(51, 26);
            closeB.TabIndex = 4;
            closeB.Text = "Close";
            closeB.UseVisualStyleBackColor = false;
            closeB.Click += closeB_Click;
            // 
            // timer
            // 
            timer.Interval = 33;
            timer.Tick += timer_Tick;
            // 
            // volumeBar
            // 
            volumeBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            volumeBar.Location = new Point(582, 0);
            volumeBar.Maximum = 100;
            volumeBar.Name = "volumeBar";
            volumeBar.Size = new Size(105, 45);
            volumeBar.TabIndex = 4;
            volumeBar.TickStyle = TickStyle.None;
            volumeBar.Value = 60;
            volumeBar.Scroll += volumeBar_Scroll;
            // 
            // checkBoxCurve
            // 
            checkBoxCurve.AutoSize = true;
            checkBoxCurve.Location = new Point(57, 22);
            checkBoxCurve.Name = "checkBoxCurve";
            checkBoxCurve.Size = new Size(59, 21);
            checkBoxCurve.TabIndex = 5;
            checkBoxCurve.Text = "Wave";
            checkBoxCurve.UseVisualStyleBackColor = true;
            checkBoxCurve.CheckedChanged += checkBoxCurve_CheckedChanged;
            // 
            // checkBoxRectangle
            // 
            checkBoxRectangle.AutoSize = true;
            checkBoxRectangle.Checked = true;
            checkBoxRectangle.CheckState = CheckState.Checked;
            checkBoxRectangle.Location = new Point(57, 2);
            checkBoxRectangle.Name = "checkBoxRectangle";
            checkBoxRectangle.Size = new Size(54, 21);
            checkBoxRectangle.TabIndex = 4;
            checkBoxRectangle.Text = "Strip";
            checkBoxRectangle.UseVisualStyleBackColor = true;
            checkBoxRectangle.CheckedChanged += checkBoxRectangle_CheckedChanged;
            // 
            // visualizerP
            // 
            visualizerP.Controls.Add(panelMenu);
            visualizerP.Controls.Add(TriggerPanel);
            visualizerP.Controls.Add(Spectrum);
            visualizerP.Dock = DockStyle.Fill;
            visualizerP.Location = new Point(0, 0);
            visualizerP.Name = "visualizerP";
            visualizerP.Size = new Size(687, 519);
            visualizerP.TabIndex = 6;
            visualizerP.Visible = false;
            // 
            // panelMenu
            // 
            panelMenu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelMenu.Controls.Add(labelFilter);
            panelMenu.Controls.Add(labelVolume);
            panelMenu.Controls.Add(labelHeight);
            panelMenu.Controls.Add(closeB);
            panelMenu.Controls.Add(trackBar1);
            panelMenu.Controls.Add(volumeBar);
            panelMenu.Controls.Add(heightBar);
            panelMenu.Controls.Add(checkBoxCurve);
            panelMenu.Controls.Add(checkBoxRectangle);
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(687, 46);
            panelMenu.TabIndex = 4;
            // 
            // labelVolume
            // 
            labelVolume.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelVolume.AutoSize = true;
            labelVolume.Location = new Point(617, 28);
            labelVolume.Name = "labelVolume";
            labelVolume.Size = new Size(52, 17);
            labelVolume.TabIndex = 6;
            labelVolume.Text = "Volume";
            // 
            // labelHeight
            // 
            labelHeight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelHeight.AutoSize = true;
            labelHeight.Location = new Point(503, 28);
            labelHeight.Name = "labelHeight";
            labelHeight.Size = new Size(46, 17);
            labelHeight.TabIndex = 4;
            labelHeight.Text = "Height";
            // 
            // heightBar
            // 
            heightBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            heightBar.Location = new Point(472, 0);
            heightBar.Maximum = 100;
            heightBar.Name = "heightBar";
            heightBar.Size = new Size(104, 45);
            heightBar.TabIndex = 4;
            heightBar.TickStyle = TickStyle.None;
            heightBar.Value = 25;
            heightBar.Scroll += heightBar_Scroll;
            // 
            // TriggerPanel
            // 
            TriggerPanel.Dock = DockStyle.Top;
            TriggerPanel.Location = new Point(0, 0);
            TriggerPanel.Name = "TriggerPanel";
            TriggerPanel.Size = new Size(687, 28);
            TriggerPanel.TabIndex = 4;
            TriggerPanel.MouseEnter += TriggerPanel_MouseEnter;
            // 
            // Spectrum
            // 
            Spectrum.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Spectrum.Location = new Point(0, 0);
            Spectrum.Name = "Spectrum";
            Spectrum.Size = new Size(687, 519);
            Spectrum.TabIndex = 5;
            Spectrum.Render += Spectrum_Render;
            Spectrum.Resize += Spectrum_Resize;
            // 
            // AnimationTimer
            // 
            AnimationTimer.Interval = 10;
            AnimationTimer.Tick += AnimationTimer_Tick;
            // 
            // analyzeB
            // 
            analyzeB.Anchor = AnchorStyles.None;
            analyzeB.Location = new Point(308, 259);
            analyzeB.Name = "analyzeB";
            analyzeB.Size = new Size(48, 26);
            analyzeB.TabIndex = 3;
            analyzeB.Text = "start";
            analyzeB.UseVisualStyleBackColor = true;
            analyzeB.Click += analyzeB_Click;
            // 
            // chooseFileB
            // 
            chooseFileB.Anchor = AnchorStyles.None;
            chooseFileB.Location = new Point(410, 228);
            chooseFileB.Name = "chooseFileB";
            chooseFileB.Size = new Size(29, 27);
            chooseFileB.TabIndex = 2;
            chooseFileB.Text = "...";
            chooseFileB.UseVisualStyleBackColor = true;
            chooseFileB.Click += chooseFileB_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Location = new Point(260, 230);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(144, 23);
            textBox1.TabIndex = 1;
            // 
            // initialP
            // 
            initialP.Controls.Add(textBox1);
            initialP.Controls.Add(chooseFileB);
            initialP.Controls.Add(analyzeB);
            initialP.Dock = DockStyle.Fill;
            initialP.Location = new Point(0, 0);
            initialP.Name = "initialP";
            initialP.Size = new Size(687, 519);
            initialP.TabIndex = 5;
            // 
            // trackBar1
            // 
            trackBar1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            trackBar1.Location = new Point(362, 0);
            trackBar1.Maximum = 100;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(104, 45);
            trackBar1.TabIndex = 4;
            trackBar1.TickStyle = TickStyle.None;
            trackBar1.Value = 15;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // labelFilter
            // 
            labelFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelFilter.AutoSize = true;
            labelFilter.Location = new Point(391, 28);
            labelFilter.Name = "labelFilter";
            labelFilter.Size = new Size(36, 17);
            labelFilter.TabIndex = 7;
            labelFilter.Text = "Filter";
            // 
            // Audio_visualizer
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(687, 519);
            Controls.Add(visualizerP);
            Controls.Add(initialP);
            MinimumSize = new Size(373, 89);
            Name = "Audio_visualizer";
            Text = "Audio_visulaizer";
            FormClosing += Audio_visulaizer_FormClosing;
            Load += Audio_visulaizer_Load;
            ((System.ComponentModel.ISupportInitialize)volumeBar).EndInit();
            visualizerP.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)heightBar).EndInit();
            initialP.ResumeLayout(false);
            initialP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button closeB;
        private System.Windows.Forms.Timer timer;
        private Panel visualizerP;
        private SpectrumControl Spectrum;
        private Panel panelMenu;
        private TransparentPanel TriggerPanel;
        private System.Windows.Forms.Timer AnimationTimer;
        private CheckBox checkBoxCurve;
        private CheckBox checkBoxRectangle;
        private TrackBar volumeBar;
        private Label labelVolume;
        private TrackBar heightBar;
        private Label labelHeight;
        private Button analyzeB;
        private Button chooseFileB;
        private TextBox textBox1;
        private Panel initialP;
        private Label labelFilter;
        private TrackBar trackBar1;
    }
}