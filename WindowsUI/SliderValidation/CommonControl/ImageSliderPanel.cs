using System.Drawing;
using System.Windows.Forms;

namespace SliderValidation.CommonControl
{
    public partial class ImageSliderPanel : Label
    {
        public ImageSliderPanel()
        {
            InitializeComponent();
            InitForm();
            InitContent();
        }
        #region property
        private Image _sliderImage;
        public Image SliderImage
        {
            get { return _sliderImage; }
            set
            {
                _sliderImage = value;
                _picSlider.Image = _sliderImage;
            }
        }

        #endregion

        #region control
        private PictureBox _picSlider;
        #endregion

        #region method
        /// <summary>
        /// //绘制边框
        /// </summary>      
        /// <param name="color">边框颜色</param>
        /// <param name="x">label宽度</param>
        /// <param name="y">label高度</param>
        private void DrawBorder(Color bordercolor, int x, int y)
        {
            Graphics graphics = CreateGraphics();
            BorderStyle = BorderStyle.None;
            Rectangle myRectangle = new Rectangle(0, 0, x, y);
            ControlPaint.DrawBorder(graphics, myRectangle, bordercolor, ButtonBorderStyle.Solid);
        }
        #endregion

        #region 初始化窗口
        private void InitForm()
        {
            DoubleBuffered = true;
            Size = new Size(45, 45);
        }
        private void InitContent()
        {
            _picSlider = new ImageButton
            {
                Cursor = Cursors.Hand,
                Parent = this,
                SizeMode = PictureBoxSizeMode.CenterImage,
                Image = SliderImage,
                Size = new Size(20, 20)
            };
            _picSlider.Location = new Point(ClientSize.Width / 2 - _picSlider.Width / 2, ClientSize.Height / 2 - _picSlider.Height / 2);
            _picSlider.Paint += (sender, e) => { DrawBorder(Color.FromArgb(255, 95, 74), this.Width, this.Height); };
            _picSlider.MouseUp += (sender, e) =>
            {
                MouseUpEvent(sender, e);
            };
            _picSlider.MouseMove += (sender, e) =>
            {
                MouseMoveEvent(sender, e);
            };
            _picSlider.MouseDown += (sender, e) =>
            {
                MouseDownEvent(sender, e);
            };
        }
        #endregion

        #region event
        public delegate void MouseUpEventHandler(object sender, MouseEventArgs e);
        public event MouseUpEventHandler MouseUpEvent;

        public delegate void MouseMoveEventHandler(object sender, MouseEventArgs e);
        public event MouseMoveEventHandler MouseMoveEvent;

        public delegate void MouseDownEventHandler(object sender, MouseEventArgs e);
        public event MouseDownEventHandler MouseDownEvent;
        #endregion

    }
}
