using Svg;
using SvgApply.Properties;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SvgApply
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitForm();
            InitContent();
        }

        #region property
        private SvgDocument _document;
        private int _effectScaleWidth = 252;
        private int _effectScaleHeight = 40;
        public static string ApplicationDataTempPath
        {
            get
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SystemData\\Temp1\\";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                return path;
            }
        }
        #endregion

        #region method       

        /// <summary>
        /// 将SVG文件转换为位图图像。
        /// </summary>
        /// <param name="filePath">SVG图像的全路径。</param>
        /// <returns>返回转换位图图像。</returns>
        public Image GetImageFromSVG(string filePath)
        {
            try
            {
                _document = SvgDocument.Open(filePath);
                return ZoomImage(_document.Draw(), _effectScaleWidth, _effectScaleHeight);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 等比例缩放图片
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="destHeight"></param>
        /// <param name="destWidth"></param>
        /// <returns></returns>
        private Bitmap ZoomImage(Bitmap bitmap, int destWidth, int destHeight)
        {
            try
            {
                Image sourImage = bitmap;
                int width = 0, height = 0;
                //按比例缩放           
                int sourWidth = sourImage.Width;
                int sourHeight = sourImage.Height;
                if (sourHeight > destHeight || sourWidth > destWidth)
                {
                    if ((sourWidth * destHeight) > (sourHeight * destWidth))
                    {
                        width = destWidth;
                        height = (destWidth * sourHeight) / sourWidth;
                    }
                    else
                    {
                        height = destHeight;
                        width = (sourWidth * destHeight) / sourHeight;
                    }
                }
                else
                {
                    width = sourWidth;
                    height = sourHeight;
                }
                Bitmap destBitmap = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(destBitmap);
                g.Clear(Color.Transparent);
                //设置画布的描绘质量         
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //g.DrawImage(sourImage, new Rectangle((destWidth - width) / 2, (destHeight - height) / 2, width, height), 0, 0, sourImage.Width, sourImage.Height, GraphicsUnit.Pixel);
                g.DrawImage(sourImage, new Rectangle(12, 0, width, height), 0, 0, sourImage.Width, sourImage.Height, GraphicsUnit.Pixel);

                g.Dispose();
                //设置压缩质量     
                System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                sourImage.Dispose();
                return destBitmap;
            }
            catch
            {
                return bitmap;
            }
        }

        /// <summary>
        /// 下载SVG
        /// </summary>
        /// <param name="remoteFileUrl">SVG来源地址</param>
        /// <param name="localSvgFileName">保存SVG的路径</param>
        /// <param name="localPngEffectPreview">保存svg生成图片的路径</param>
        public void DownloadSvgPreviewFile(string remoteFileUrl, string localSvgFileName, string localPngEffectPreview)
        {
            var webClient = new WebClient();
            webClient.DownloadFileCompleted += (sender, e) =>
            {
                try
                {
                    if (File.Exists(e.UserState.ToString()))
                    {
                        DownloadPngPreviewFile(e.UserState.ToString(), localPngEffectPreview);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
            webClient.DownloadFileAsync(new Uri(remoteFileUrl), localSvgFileName, localSvgFileName);
        }

        /// <summary>
        /// 下载PNG图片
        /// </summary>
        /// <param name="remoteFileUrl"></param>
        /// <param name="localFileName"></param>
        public void DownloadPngPreviewFile(string localSvgFileName, string localPngEffectPreview)
        {
            var image = GetImageFromSVG(localSvgFileName);
            if (image != null)
            {
                Image pngImage = image;
                pngImage.Save(localPngEffectPreview);
            }
            CreatePicture(localSvgFileName, image);
        }

        /// <summary>
        /// 创建一个承载图片的容器
        /// </summary>
        private void CreatePicture(string localSvgFileName, Image image)
        {
            PictureBox pictureBox = new PictureBox
            {
                Parent = this,
                Image = image,
                SizeMode = PictureBoxSizeMode.Normal,
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(255, 255, 255)
            };
            pictureBox.Location = new Point((ClientSize.Width - pictureBox.Width) / 2, (ClientSize.Height - pictureBox.Height) / 2);
            pictureBox.Height = pictureBox.Image.Height;
            pictureBox.Width = pictureBox.Image.Width > ClientSize.Width ? ClientSize.Width - 10 : pictureBox.Image.Width;

            //特效字体可以拖动到设计软件中使用，目前支持XD、AI等。注 Word 、PS有外壳
            pictureBox.MouseDown += (senders, e) =>
            {
                string[] files = new string[1];
                files[0] = localSvgFileName;
                DoDragDrop(new DataObject(DataFormats.FileDrop, files), DragDropEffects.Copy | DragDropEffects.Move);
            };
        }
        #endregion

        #region 初始化窗口
        /// <summary>
        /// 初始化窗口属性
        /// </summary>
        private void InitForm()
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Font = new Font("微软雅黑", 12, GraphicsUnit.Pixel);
            BackColor = Color.FromArgb(240, 240, 240);
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
        /// 初始化页面内容
        /// </summary>
        private void InitContent()
        {
            string localSvgFileName = ApplicationDataTempPath + "test.svg";
            string localPngEffectPreview = ApplicationDataTempPath + "test.png";
            string fullName = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\"));
            string sourceSvgFile = fullName.Substring(0, fullName.LastIndexOf("\\")) + "\\" + "SvgResources\\a.svg";
            DownloadSvgPreviewFile(sourceSvgFile, localSvgFileName, localPngEffectPreview);
        }
        #endregion
    }
}
