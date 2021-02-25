using System;
using System.Drawing;
using System.Windows.Forms;

namespace SliderValidation.CommonControl
{
    /// <summary>
    /// 图片按钮
    /// </summary>
    public partial class ImageButton : PictureBox
    {
        #region constructor

        public ImageButton()
        {
            InitializeComponent();
            _toolTip = new ToolTip(components);
            SizeMode = PictureBoxSizeMode.AutoSize;
            MouseEnter += ImageButton_MouseEnter;
            MouseLeave += ImageButton_MouseLeave;
            MouseDown += ImageButton_MouseDown;
            MouseUp += ImageButton_MouseUp;
        }

        #endregion

        #region property

        public new Image Image
        {
            get { return base.Image; }
            set
            {
                base.Image = value;
                _imageNormal = value;
            }
        }

        private Image _imageNormal;

        public Image ImageNormal
        {
            get { return _imageNormal; }
            set
            {
                _imageNormal = value;
                base.Image = value;
            }
        }

        private Image _imageOver;

        public Image ImageOver
        {
            get { return _imageOver; }
            set { _imageOver = value; }
        }

        private Image _imageDown;

        public Image ImageDown
        {
            get { return _imageDown; }
            set { _imageDown = value; }
        }

        public string ToolTip
        {
            get { return _toolTip == null ? "" : _toolTip.GetToolTip(this); }
            set
            {
                if (_toolTip != null) _toolTip.SetToolTip(this, value);
            }
        }

        #endregion

        #region field

        private readonly ToolTip _toolTip;

        /// <summary>
        /// 鼠标已经leave标识
        /// </summary>
        private bool _mouseLeft;

        #endregion

        protected void ImageButton_MouseEnter(object sender, EventArgs e)
        {
            _mouseLeft = false;
            if (_imageOver != null) base.Image = _imageOver;
        }

        protected void ImageButton_MouseLeave(object sender, EventArgs e)
        {
            _mouseLeft = true;
            if (_imageNormal != null) base.Image = _imageNormal;
        }

        protected void ImageButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (_imageDown != null) base.Image = _imageDown;
        }

        protected void ImageButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_mouseLeft && _imageOver != null) base.Image = _imageOver;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
