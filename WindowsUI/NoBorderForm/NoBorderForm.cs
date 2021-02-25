using System;
using System.Drawing;
using System.Windows.Forms;

namespace NoBorderForm
{
    /// <summary>
    /// 无边框窗体类(可设定: 拖动标识-Draggable, 缩放标识-Resizable)
    /// </summary>
    public partial class NoBorderForm : Form
    {
        public NoBorderForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
        }

        #region property

        /// <summary>
        /// 窗口可拖动标识
        /// </summary>
        public bool Draggable { get; set; }

        /// <summary>
        /// 窗口可缩放标识
        /// </summary>
        public bool Resizable { get; set; }

        #endregion

        #region 处理拖动和缩放

        /*
        private const int GuyingHtleft = 10;
        private const int GuyingHtright = 11;
        private const int GuyingHttop = 12;
        private const int GuyingHttopleft = 13;
        private const int GuyingHttopright = 14;
        private const int GuyingHtbottom = 15;
        private const int GuyingHtbottomleft = 0x10;
        private const int GuyingHtbottomright = 17;
        */
        private const int GuyingHtLeft = 0x0A;
        private const int GuyingHtRight = 0x0B;
        private const int GuyingHtTop = 0x0C;
        private const int GuyingHtTopLeft = 0x0D;
        private const int GuyingHtTopRight = 0x0E;
        private const int GuyingHtBottom = 0x0F;
        private const int GuyingHtBottomLeft = 0x10;
        private const int GuyingHtBottomRight = 0x11;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    if ( /*this.*/Resizable)
                    {
                        Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = PointToClient(vPoint);
                        if (vPoint.X <= 5)
                            if (vPoint.Y <= 5) m.Result = (IntPtr)GuyingHtTopLeft;
                            else if (vPoint.Y >= ClientSize.Height - 5) m.Result = (IntPtr)GuyingHtBottomLeft;
                            else m.Result = (IntPtr)GuyingHtLeft;
                        else if (vPoint.X >= ClientSize.Width - 5)
                            if (vPoint.Y <= 5) m.Result = (IntPtr)GuyingHtTopRight;
                            else if (vPoint.Y >= ClientSize.Height - 5) m.Result = (IntPtr)GuyingHtBottomRight;
                            else m.Result = (IntPtr)GuyingHtRight;
                        else if (vPoint.Y <= 5) m.Result = (IntPtr)GuyingHtTop;
                        else if (vPoint.Y >= ClientSize.Height - 5) m.Result = (IntPtr)GuyingHtBottom;
                    }
                    break;
                case 0x0201: //鼠标左键按下的消息 
                    if ( /*this.*/Draggable)
                    {
                        m.Msg = 0x00A1; //更改消息为非客户区按下鼠标 
                        m.LParam = IntPtr.Zero; //默认值 
                        m.WParam = new IntPtr(2); //鼠标放在标题栏内 
                    }
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion
    }
}
