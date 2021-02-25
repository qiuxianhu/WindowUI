using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageCrop
{
    public class ImageOperate
    {
        /// <summary>
        /// 获取等比例缩放的图片（高宽不一致时获取最中间部分的图片）
        /// </summary>
        public static Image AdjImageToFitSize(Image fromImage, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Point[] destPoints = new Point[] {
                new Point(0, 0),
                new Point(width, 0),
                new Point(0, height)
            };
            Rectangle rect = GetImageRectangle(fromImage.Width, fromImage.Height);
            graphics.DrawImage(fromImage, destPoints, rect, GraphicsUnit.Pixel);
            Image image = Image.FromHbitmap(bitmap.GetHbitmap());
            bitmap.Dispose();
            graphics.Dispose();
            return image;
        }

        /// <summary>
        /// 居中位置获取
        /// </summary>
        private static Rectangle GetImageRectangle(int w, int h)
        {
            int x = 0;
            int y = 0;

            if (h > w)
            {
                h = w;
                y = (h - w) / 2;
            }
            else
            {
                w = h;
                x = (w - h) / 2;
            }

            return new Rectangle(x, y, w, h);
        }

        /// <summary>
        /// 通过地址加载图片
        /// </summary>
        /// <param name="url"></param>
        public static Image LoadPhotoImage(string url)
        {
            try
            {
                var request = System.Net.WebRequest.Create(url);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                return stream == null ? null : Image.FromStream(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// 绘制圆形头像框
        /// </summary>
        public static Region DrawingCircle(Rectangle clientRectangle)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(clientRectangle);
            Region region = new Region(gp);
            return region;
        }
    }
}
