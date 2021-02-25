using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageCompress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitForm();
            InitContent();
        }
        public static string ImgFile => string.Format("图像文件(*.bmp;*jpg;*tif;*png;*.gif)|*.bmp;*jpg;*tif;*png;*.gif" +
                                                      "|bmp文件(*.bmp)|*.bmp" +
                                                      "|jpg文件(*.jpg)|*.jpg" +
                                                      "|tif文件(*.tif)|*.tif" +
                                                      "|png文件(*.png)|*.png" +
                                                      "|gif文件(*.gif)|*.gif" +
                                                      "|所有文件(*.*)|*.*");


        #region control
        private Button btnUpload;
        private PictureBox _originalPic;
        private Label lblOriginalDec;
        private PictureBox _endPic;
        private Label lblEndDec;
        #endregion

        #region method 
        private void Upload(object sender, EventArgs e)
        {
            var openDlg = new OpenFileDialog { Filter = ImgFile };
            if (openDlg.ShowDialog() != DialogResult.OK) return;
            _originalPic.Image = Image.FromFile(openDlg.FileName);
            _endPic.Image = ImageOperate.ZoomImage((Bitmap)Image.FromFile(openDlg.FileName), 100, 100);
        }
        #endregion

        #region 界面初始化
        private void InitForm()
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            // 窗口属性
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("微软雅黑", 12, GraphicsUnit.Pixel);
            BackColor = Color.FromArgb(255, 255, 255);
            TopMost = true;
            Shown += (sender, e) => { };
            Size = new Size(650, 600);
        }


        private void InitContent()
        {
            btnUpload = new Button
            {
                Parent = this,
                AutoSize = true,
                Text = "上传图片"
            };
            btnUpload.Location = new Point(50, (ClientSize.Height - btnUpload.Height) / 2);
            btnUpload.Click += Upload;

            //原图
            _originalPic = new PictureBox
            {
                Parent = this,
                Size = new Size(250, 200),
                SizeMode = PictureBoxSizeMode.Normal,
                BorderStyle = BorderStyle.Fixed3D
            };
            _originalPic.Location = new Point(250, 30);

            lblOriginalDec = new Label
            {
                Parent = this,
                Text = "原图：",
                AutoSize = true
            };
            lblOriginalDec.Location = new Point(_originalPic.Left, _originalPic.Top - lblOriginalDec.Height - 5);

            //透明图            
            _endPic = new PictureBox
            {
                Parent = this,
                Size = new Size(250, 200),
                SizeMode = PictureBoxSizeMode.Normal,
                BorderStyle = BorderStyle.Fixed3D
            };
            _endPic.Location = new Point(250, _originalPic.Height + 80);

            lblEndDec = new Label
            {
                Parent = this,
                Text = "效果图:",
                AutoSize = true
            };
            lblEndDec.Location = new Point(_endPic.Left, _endPic.Top - lblEndDec.Height - 5);


        }
        #endregion
    }
}
