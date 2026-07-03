using AudioPlay;
using AudioProcessing.Core;
using Filter.Core;
using SignalVisualizer.Core;
using System.Runtime.CompilerServices;



namespace sound_visualization
{
    public partial class Audio_visualizer : Form
    {

        private readonly AudioToData _processor;

        FilterBase _filter;


        private readonly AudioPlayer _AudioPlayer;

        WinformControl _control;

        RenderChannel<RectangleF>? _rectangelRender;
        RenderChannel<PointF>? _curveRender;



        private readonly Color[] _colors;

        private float _maxHeightRatio = 0.25f;
        private float _filterAlpha = 0.15f;

        public Audio_visualizer()
        {
            InitializeComponent();


            _processor = new AudioToData(fftWindowSize: 2048, bandCount: 128);

            _filter = new LowPassFilter(_filterAlpha, _processor.BandCount);


            _AudioPlayer = new AudioPlayer(AudioPlayer.AudioPlayerLatency.DefaultLatency);



            _control = new WinformControl();
            InitializeVisualizerChannels(_control);

            _colors = GetColors();


            InitializeNotifyIcon();


        }


        private void InitializeVisualizerChannels(WinformControl c)
        {
            float currentHeight = this.ClientSize.Height * _maxHeightRatio;

            RectangleDataVisualizer _rectDataCalc = new RectangleDataVisualizer(currentHeight, this.ClientSize.Width, this.ClientSize.Height, _processor.BandCount, 0.6f);
            _rectDataCalc.MinBarWidth = 1;
            _rectDataCalc.MinBarHeight = 1;

            PointsDataVisualizer pointDataCalc = new PointsDataVisualizer(currentHeight, this.ClientSize.Width, this.ClientSize.Height, _processor.BandCount);

            DrawRectangles draw = new DrawRectangles();

            DrawWave drawCurve = new DrawWave();

            _rectangelRender = new RenderChannel<RectangleF>(_rectDataCalc, draw);

            _curveRender = new RenderChannel<PointF>(pointDataCalc, drawCurve)
            {
                Enable = false
            };





            c.AddRenderChannel(_rectangelRender);
            c.AddRenderChannel(_curveRender);


        }


        private NotifyIcon? notifyIcon;
        private ContextMenuStrip? contextMenu;
        bool _isTransparent = false;
        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            contextMenu = new ContextMenuStrip();

            notifyIcon.DoubleClick += OnShowForm;

            contextMenu.Items.Add("Show/Transparent", null, OnShowMenuItemClick);
            contextMenu.Items.Add("Exit", null, OnExitMenuItemClick);
            notifyIcon.ContextMenuStrip = contextMenu;
            notifyIcon.Icon = this.Icon;
            notifyIcon.Text = "Audio Visualizer";
            notifyIcon.Visible = true;
        }
        private void OnShowForm(object? sender, EventArgs e)
        {
            this.Show();

        }

        private void OnShowMenuItemClick(object? sender, EventArgs e)
        {

            if (_isTransparent)
            {
                DisableTransparent();
                _isTransparent = false;
            }
            else
            {
                EnableTransparent();
                _isTransparent = true;
            }
        }
        private void EnableTransparent()
        {



            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;

            this.ShowInTaskbar = false;

            this.TopMost = true;

        }
        private void DisableTransparent()
        {



            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.BackColor = SystemColors.Control;
            this.TransparencyKey = Color.Empty;

            this.ShowInTaskbar = true;
            this.TopMost = false;
        }


        private void OnExitMenuItemClick(object? sender, EventArgs e)
        {
            this.Close();
        }



        private void Audio_visulaizer_Load(object sender, EventArgs e)
        {


            panelMenu.BackColor = Color.White;
            panelMenu.Height = this.ClientSize.Height / 6;
            panelMenu.Top = -panelMenu.Height;


            TriggerPanel.Height = panelMenu.Height;

        }




        private void chooseFileB_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Title = "Select an audio file";
                fileDialog.Filter = "Audio files (*.wav;*.mp3;*.mp4)|*.wav;*.mp3;*.mp4|WAV files (*.wav)|*.wav|MP3 files (*.mp3)|*.mp3|MP4 files (*.mp4)|*.mp4";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = fileDialog.FileName;
                }
            }
        }




        private int _sampleRate = 0;
        private int _hopSize = 0;
        private int _frameCount = 0;
        private int _bandCount = 0;

        private async void analyzeB_Click(object sender, EventArgs e)
        {

            string path = textBox1.Text;
            try
            {
                this.analyzeB.Enabled = false;//avoid user click button when analyze
                AudioToData.AudioAnalysisData? data = await Task.Run(() =>
                {
                    AudioToData.AudioAnalysisData? result = _processor.Process(path);

                    if (result is not null)
                    {
                        NormalizeFrameData(result);
                    }

                    return result;
                });


                if (data is null)
                {
                    MessageBox.Show("Invalid audio file.");
                    return;
                }

                _sampleRate = data.SampleRate;
                _hopSize = data.HopSize;
                _frameCount = data.FrameCount;
                _bandCount = data.BandCount;
            }
            finally
            {
                this.analyzeB.Enabled = true;
            }


            //data now can be released since no reference to it is needed anymore, will be handled by GC
            //data.ReleaseFrames();
            ShowVisualizer(true);

            _AudioPlayer.PlayAudio(path);
            _AudioPlayer.Volume = MathF.Pow(volumeBar.Value / 100.0f, 2.2f);



            timer.Start();


        }




        private void ShowVisualizer(bool showUI)
        {
            initialP.Visible = !showUI;
            visualizerP.Visible = showUI;
        }




        private float[]? NormalizedFrameData;
        private void NormalizeFrameData(AudioToData.AudioAnalysisData data)
        {

            //The extra bandCount is left with zeros for the purpose of fading out the visualization
            //after the audio has finished playing
            NormalizedFrameData = new float[data.FrameCount * data.BandCount + data.BandCount];

            data.NormalizeFrameData(NormalizedFrameData.AsSpan(0, data.FrameCount * data.BandCount), -60);
        }




        private int _fadeOutFrameCount;
        private int _currentFrame;
        private int _colorIndex = 0;
        private static readonly Color[] _gradientColors = new Color[2];



        private void timer_Tick(object sender, EventArgs e)
        {


            double time = _AudioPlayer.GetCurrentTime();
            _currentFrame = (int)(time * (double)_sampleRate / (double)_hopSize);



            if (_currentFrame >= _frameCount)
            {

                _currentFrame = _frameCount;


                _fadeOutFrameCount++;


                if (_fadeOutFrameCount > 30)
                {
                    timer.Stop();
                    ShowVisualizer(false);
                    FormData_initialize();


                    return;
                }


            }



            _colorIndex = (_colorIndex + 1) % _colors.Length;
            _gradientColors[0] = _colors[_colorIndex];

            _gradientColors[1] = _colors[(_colorIndex + 256) % _colors.Length];


            _control.SetColor(_gradientColors);


            Spectrum.Invalidate();

        }



        private Color[] GetColors()
        {
            Color[] colors = new Color[256 * 6];
            for (int i = 0; i < 256; i++)
            {
                colors[i] = Color.FromArgb(255, i, 0);

            }
            for (int i = 0; i < 256; i++)
            {
                colors[256 + i] = Color.FromArgb(255 - i, 255, 0);
            }
            for (int i = 0; i < 256; i++)
            {
                colors[512 + i] = Color.FromArgb(0, 255, i);
            }
            for (int i = 0; i < 256; i++)
            {
                colors[768 + i] = Color.FromArgb(0, 255 - i, 255);
            }
            for (int i = 0; i < 256; i++)
            {
                colors[1024 + i] = Color.FromArgb(i, 0, 255);
            }
            for (int i = 0; i < 256; i++)
            {
                colors[1280 + i] = Color.FromArgb(255, 0, 255 - i);
            }

            return colors;
        }




        private void FormData_initialize()
        {
            _currentFrame = 0;

            _fadeOutFrameCount = 0;

            _filter.Reset();
            NormalizedFrameData = null;


        }

        private void Spectrum_Resize(object? sender, EventArgs e)
        {
            float currentHeight = this.ClientSize.Height * _maxHeightRatio;
            _control.UpdateSize(currentHeight, Spectrum.ClientSize.Width, Spectrum.ClientSize.Height);


            int targetHeight = (this.ClientSize.Height / 6);


            if (targetHeight < 60)
            {
                targetHeight = 60;
            }
            else if (targetHeight > 150)
            {
                targetHeight = 150;
            }



            panelMenu.Height = targetHeight;
            panelMenu.Top = -targetHeight;
            panelMenu.Width = this.ClientSize.Width;

            TriggerPanel.Height = targetHeight;


        }




        private void Spectrum_Render(Graphics g)
        {
            //if (NormalizedFrameData != null && NormalizedFrameData.Length != 0 && _currentFrame >= 0 && _frameCount > 0 && _currentFrame < _frameCount + 1)
            //{


            //    ReadOnlySpan<float> smoothedData = _filter.Process(NormalizedFrameData.AsSpan(_currentFrame * _bandCount, _bandCount));

            //    control.UpdateAndDraw(g, smoothedData);

            //}

            ReadOnlySpan<float> smoothedData = _filter.Process(NormalizedFrameData.AsSpan(_currentFrame * _bandCount, _bandCount));

            _control.UpdateAndDraw(g, smoothedData);

        }




        private async void closeB_Click(object sender, EventArgs e)
        {

            timer.Stop();

            _AudioPlayer.StopAudio();

            ShowVisualizer(false);
            FormData_initialize();




        }





        bool _opening = false;

        private void TriggerPanel_MouseEnter(object sender, EventArgs e)
        {
            _opening = true;
            AnimationTimer.Start();
        }




        float _currentSpeed = 2.0f;
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (_opening)
            {

                if (panelMenu.Top < 0)
                {
                    int remainingDistance = 0 - panelMenu.Top;
                    panelMenu.Top += (int)Math.Ceiling(remainingDistance * 0.15);
                }
                else
                {
                    Check_MouseLeve();
                }
            }
            else
            {
                int targetTop = -panelMenu.Height;
                if (panelMenu.Top > targetTop)
                {

                    _currentSpeed *= 1.1f;

                    panelMenu.Top -= (int)Math.Ceiling(_currentSpeed);
                }
                else
                {
                    panelMenu.Top = targetTop;
                    _currentSpeed = 2.0f;
                    AnimationTimer.Stop();
                }
            }

        }




        private void Check_MouseLeve()
        {
            Point mouseScreenPos = Control.MousePosition;


            Point mouseClientPos = panelMenu.PointToClient(mouseScreenPos);


            if (!panelMenu.ClientRectangle.Contains(mouseClientPos))
            {
                _opening = false;
                AnimationTimer.Start();
            }
        }

        private void checkBoxRectangle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRectangle.Checked)
            {
                _rectangelRender?.Enable = true;
            }
            else
            {
                _rectangelRender?.Enable = false;
            }
        }

        private void checkBoxCurve_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurve.Checked)
            {
                _curveRender?.Enable = true;
            }
            else
            {
                _curveRender?.Enable = false;
            }
        }


        private async void Audio_visulaizer_FormClosing(object? sender, FormClosingEventArgs e)
        {

            e.Cancel = true;
            try
            {
                _AudioPlayer.StopAudio();

                await _AudioPlayer.shutDown();
            }
            finally
            {

                this.FormClosing -= Audio_visulaizer_FormClosing;

                this.Close();
            }

        }

        private void volumeBar_Scroll(object sender, EventArgs e)
        {
            _AudioPlayer.Volume = MathF.Pow(volumeBar.Value / 100.0f, 2.2f);
        }

        private void heightBar_Scroll(object sender, EventArgs e)
        {
            _maxHeightRatio = heightBar.Value / 100.0f;
            _control.UpdateSize(this.ClientSize.Height * _maxHeightRatio, Spectrum.ClientSize.Width, Spectrum.ClientSize.Height);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            _filterAlpha = trackBar1.Value / 100.0f;
            if (_filter is LowPassFilter lowPassFilter)
            {
                lowPassFilter.Alpha = _filterAlpha;

            }
        }
    }

}
