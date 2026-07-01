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
            textBox1 = new TextBox();
            chooseFileB = new Button();
            analyzeB = new Button();
            closeB = new Button();
            timer = new System.Windows.Forms.Timer(components);
            initialP = new Panel();
            volumeBar = new TrackBar();
            checkBoxCurve = new CheckBox();
            checkBoxRectangle = new CheckBox();
            visualizerP = new Panel();
            panelMenu = new Panel();
            TriggerPanel = new Panel();
            Spectrum = new SpectrumControl();
            AnimationTimer = new System.Windows.Forms.Timer(components);
            initialP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeBar).BeginInit();
            visualizerP.SuspendLayout();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Location = new Point(282, 247);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(144, 23);
            textBox1.TabIndex = 1;
            // 
            // chooseFileB
            // 
            chooseFileB.Anchor = AnchorStyles.None;
            chooseFileB.Location = new Point(432, 245);
            chooseFileB.Name = "chooseFileB";
            chooseFileB.Size = new Size(29, 27);
            chooseFileB.TabIndex = 2;
            chooseFileB.Text = "...";
            chooseFileB.UseVisualStyleBackColor = true;
            chooseFileB.Click += chooseFileB_Click;
            // 
            // analyzeB
            // 
            analyzeB.Anchor = AnchorStyles.None;
            analyzeB.Location = new Point(330, 276);
            analyzeB.Name = "analyzeB";
            analyzeB.Size = new Size(48, 26);
            analyzeB.TabIndex = 3;
            analyzeB.Text = "start";
            analyzeB.UseVisualStyleBackColor = true;
            analyzeB.Click += analyzeB_Click;
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
            // initialP
            // 
            initialP.Controls.Add(textBox1);
            initialP.Controls.Add(chooseFileB);
            initialP.Controls.Add(analyzeB);
            initialP.Dock = DockStyle.Fill;
            initialP.Location = new Point(0, 0);
            initialP.Name = "initialP";
            initialP.Size = new Size(730, 553);
            initialP.TabIndex = 5;
            // 
            // volumeBar
            // 
            volumeBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            volumeBar.Location = new Point(625, 0);
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
            checkBoxCurve.Location = new Point(208, 34);
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
            checkBoxRectangle.Location = new Point(208, 7);
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
            visualizerP.Size = new Size(730, 553);
            visualizerP.TabIndex = 6;
            // 
            // panelMenu
            // 
            panelMenu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelMenu.Controls.Add(closeB);
            panelMenu.Controls.Add(volumeBar);
            panelMenu.Controls.Add(checkBoxCurve);
            panelMenu.Controls.Add(checkBoxRectangle);
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(730, 46);
            panelMenu.TabIndex = 4;
            // 
            // TriggerPanel
            // 
            TriggerPanel.Dock = DockStyle.Top;
            TriggerPanel.Location = new Point(0, 0);
            TriggerPanel.Name = "TriggerPanel";
            TriggerPanel.Size = new Size(730, 28);
            TriggerPanel.TabIndex = 4;
            TriggerPanel.MouseEnter += TriggerPanel_MouseEnter;
            // 
            // Spectrum
            // 
            Spectrum.Dock = DockStyle.Fill;
            Spectrum.Location = new Point(0, 0);
            Spectrum.Name = "Spectrum";
            Spectrum.Size = new Size(730, 553);
            Spectrum.TabIndex = 5;
            Spectrum.Render += Spectrum_Render;
            Spectrum.Resize += Spectrum_Resize;
            // 
            // AnimationTimer
            // 
            AnimationTimer.Interval = 10;
            AnimationTimer.Tick += AnimationTimer_Tick;
            // 
            // Audio_visulaizer
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(730, 553);
            Controls.Add(initialP);
            Controls.Add(visualizerP);
            Name = "Audio_visulaizer";
            Text = "Audio_visulaizer";
            FormClosing += Audio_visulaizer_FormClosing;
            Load += Audio_visulaizer_Load;
            initialP.ResumeLayout(false);
            initialP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)volumeBar).EndInit();
            visualizerP.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox textBox1;
        private Button chooseFileB;
        private Button analyzeB;
        private Button closeB;
        private System.Windows.Forms.Timer timer;
        private Panel visualizerP;
        private Panel initialP;
        private SpectrumControl Spectrum;
        private Panel panelMenu;
        private Panel TriggerPanel;
        private System.Windows.Forms.Timer AnimationTimer;
        private CheckBox checkBoxCurve;
        private CheckBox checkBoxRectangle;
        private TrackBar volumeBar;
    }
}