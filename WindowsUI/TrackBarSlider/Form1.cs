using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TrackBarSlider
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitForm();
            InitContent();
        }

        #region 窗口初始化
        private void InitForm()
        {
            DoubleBuffered = true;

            // 窗口属性
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("微软雅黑", 12, GraphicsUnit.Pixel);
            BackColor = Color.FromArgb(255, 255, 255);
            TopMost = true;
            Shown += (sender, e) => { };
            Paint += (sender, e) =>
            {
                var graphics = e.Graphics;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawRectangle(new Pen(Color.FromArgb(255, 255, 255)), 0, 0, 300, 300);
            };
        }

        private void InitContent()
        {
            //切滑动条
            MediaSlider _mediaSlider1 = new MediaSlider
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Animated = false,
                AnimationSize = 0.2F,
                AnimationSpeed = MediaSlider.AnimateSpeed.Normal,
                AutoScrollMargin = new Size(0, 0),
                AutoScrollMinSize = new Size(0, 0),
                AutoSize = true,
                BackColor = Color.White,
                BackgroundImage = null,
                ButtonAccentColor = Color.White,
                ButtonBorderColor = Color.Black,
                ButtonColor = Color.White,
                ButtonCornerRadius = ((uint)(6u)),
                ButtonSize = new Size(9, 9),
                ButtonStyle = MediaSlider.ButtonType.Round,
                ContextMenuStrip = null,
                LargeChange = 2,
                Margin = new Padding(0),
                Maximum = 3,
                Minimum = 1,
                Name = "mediaSlider",
                Orientation = Orientation.Horizontal,
                ShowButtonOnHover = false,
                Size = new Size(150, 9),
                SliderFlyOut = MediaSlider.FlyOutStyle.None,
                SmallChange = 1,
                SmoothScrolling = true,
                TabIndex = 17,
                TickColor = Color.DarkGray,
                TickStyle = TickStyle.None,
                TickType = MediaSlider.TickMode.Standard,
                TrackBorderColor = Color.White,
                TrackDepth = 4,
                TrackFillColor = Color.FromArgb(218, 218, 218),
                TrackProgressColor = Color.Black,
                TrackShadow = false,
                TrackShadowColor = Color.Maroon,
                TrackStyle = MediaSlider.TrackType.Progress,
                Value = 1,
                Parent = this
            };
            _mediaSlider1.Location = new Point((ClientSize.Width - _mediaSlider1.Width) / 2, (ClientSize.Height - _mediaSlider1.Height) / 2);


        }
        #endregion
    }
}
