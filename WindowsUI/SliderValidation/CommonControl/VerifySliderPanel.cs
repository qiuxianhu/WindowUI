using SliderValidation.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SliderValidation.CommonControl
{
    public partial class VerifySliderPanel : Panel
    {
        public VerifySliderPanel()
        {
            InitializeComponent();
            InitForm();
            InitContent();
            SizeChanged += (sender, e) =>
            {
                _sliderBackColor.Left = 0;
                _lblTip.Left = ClientSize.Width / 2 - _lblTip.Width / 2;
                _lblSuccessTip.Left = ClientSize.Width / 2 - _lblSuccessTip.Width / 2;
                _picSlider.Left = 0;
            };
        }

        #region method        

        /// <summary>
        /// 在picturebox的鼠标按下事件里,记录两个变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic_MouseDown(object sender, MouseEventArgs e)
        {
            _moveFlag = true;
        }

        /// <summary>
        /// 在picturebox的鼠标按下事件里
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic_MouseUp(object sender, MouseEventArgs e)
        {
            _moveFlag = false;
            if (!_successSlider)
            {
                _picSlider.Left = 0;
                _sliderBackColor.Width = 0;
            }
            else
            {
                _picSlider.SliderImage = Resources.selected_2x;
                _lblSuccessTip.ForeColor = Color.FromArgb(255, 255, 255);
                _lblSuccessTip.Text = "验证成功";
                SliderSuccessEvent(true);
            }
        }

        //在picturebox鼠标移动
        private void Pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (_moveFlag)
            {
                _picSlider.Top = 0;
                if (_picSlider.Left >= 0 && _picSlider.Left <= ClientSize.Width - _picSlider.Width - 1)
                {
                    _picSlider.Left += Convert.ToInt16(e.X);
                    _sliderBackColor.Width = _picSlider.Left;
                    _sliderBackColor.BringToFront();
                    _picSlider.BringToFront();
                    return;
                }
                if (_picSlider.Left < 0)
                {
                    _picSlider.Left = 0;
                    return;
                }
                if (_picSlider.Left > ClientSize.Width - _picSlider.Width)
                {
                    _picSlider.Left = ClientSize.Width - _picSlider.Width;
                }
                _sliderBackColor.Visible = false;
                this.BackColor = Color.FromArgb(255, 95, 74);
                _successSlider = true;
            }
        }
        #endregion

        #region property
        private Label _lblTip;
        private Label _lblSuccessTip;
        private ImageSliderPanel _picSlider;
        private Panel _sliderBackColor;
        bool _successSlider = false;//是否滑动到最右侧
        bool _moveFlag;//是否已经按下.
        #endregion

        #region 窗口初始化
        private void InitForm()
        {
            DoubleBuffered = true;
            Height = 40;
            Width = 290;
            Font = new Font("微软雅黑", 12, GraphicsUnit.Pixel);
            BackColor = Color.FromArgb(240, 240, 240);
        }
        private void InitContent()
        {
            _sliderBackColor = new Panel
            {
                Parent = this,
                Width = 1,
                Height = 40,
                BackColor = Color.FromArgb(255, 95, 74)
            };

            _lblTip = new Label
            {
                Parent = _sliderBackColor,
                Text = "请拖动左侧滑块",
                AutoSize = true,
                ForeColor = Color.FromArgb(108, 108, 108)
            };
            _lblTip.Top = ClientSize.Height / 2 - _lblTip.Height / 2;

            _lblSuccessTip = new Label
            {
                Parent = this,
                Text = "请拖动左侧滑块",
                AutoSize = true,
                ForeColor = Color.FromArgb(108, 108, 108)
            };
            _lblSuccessTip.Top = ClientSize.Height / 2 - _lblSuccessTip.Height / 2;

            _picSlider = new ImageSliderPanel
            {
                Parent = this,
                Cursor = Cursors.Hand,
                SliderImage = Resources.arrow,
                Size = new Size(40, 40),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(255, 255, 255),
                Top = 0
            };
            _picSlider.BringToFront();
            _picSlider.MouseUpEvent += Pic_MouseUp;
            _picSlider.MouseDownEvent += Pic_MouseDown;
            _picSlider.MouseMoveEvent += Pic_MouseMove;
        }
        #endregion

        #region event
        public delegate void SliderSuccessEventHandler(bool sliderSuccess);
        public event SliderSuccessEventHandler SliderSuccessEvent;
        #endregion
    }
}
