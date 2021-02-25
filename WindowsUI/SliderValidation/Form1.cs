using SliderValidation.CommonControl;
using SliderValidation.Properties;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SliderValidation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitForm();
            InitContent();
        }

        #region control
        private VerifySliderPanel _phoneVerifySliderPanel;
        #endregion


        #region 初始化窗口

        /// <summary>
        /// 初始化窗口
        /// </summary>
        private void InitForm()
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Font = new Font("微软雅黑", 12, GraphicsUnit.Pixel);
            BackColor = Color.FromArgb(255, 255, 255);
            Size = new Size(600, 450);
            TopMost = true;
            Paint += (sender, e) =>
            {
                Graphics graphics = e.Graphics;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawRectangle(new Pen(Color.FromArgb(230, 230, 230)), 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            };
            PictureBox picClose = new PictureBox
            {
                Parent = this,
                Size = new Size(10, 10),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                SizeMode = PictureBoxSizeMode.Normal,
                Image = Resources._popupclose
            };
            picClose.Location = new Point(ClientSize.Width - 10 - picClose.Width, 10);
            picClose.Click += (sender, e) => { Close(); };
        }

        /// <summary>
        /// 初始化窗口内容
        /// </summary>
        private void InitContent()
        {
            //滑块验证UI
            _phoneVerifySliderPanel = new VerifySliderPanel
            {
                Parent = this,
                Size = new Size(ClientSize.Width - 250, 40),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            _phoneVerifySliderPanel.Location = new Point((ClientSize.Width - _phoneVerifySliderPanel.Width) / 2, (ClientSize.Height - _phoneVerifySliderPanel.Height) / 2);

            //滑块验证成功后的操作
            _phoneVerifySliderPanel.SliderSuccessEvent += (sliderSuccess) =>
            {
                if (sliderSuccess)
                {
                    MessageBox.Show("验证成功，请输入秘密");
                }
            };
        }
        #endregion
    }
}
