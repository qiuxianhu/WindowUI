using System.Drawing;
using System.Drawing.Imaging;

namespace ImageAlpha
{
    public class ImageOperate
    {
        /// <summary>
        /// 方法一  设置图像透明度
        /// </summary>
        /// <param name="srcImage"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static Image TransparentImage(Image srcImage, float opacity)
        {
            float[][] nArray ={ new float[] {1, 0, 0, 0, 0},
                        new float[] {0, 1, 0, 0, 0},
                        new float[] {0, 0, 1, 0, 0},
                        new float[] {0, 0, 0, opacity, 0},
                        new float[] {0, 0, 0, 0, 1}};
            ColorMatrix matrix = new ColorMatrix(nArray);
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Bitmap resultImage = new Bitmap(srcImage.Width, srcImage.Height);
            Graphics g = Graphics.FromImage(resultImage);
            g.DrawImage(srcImage, new Rectangle(0, 0, srcImage.Width, srcImage.Height), 0, 0, srcImage.Width, srcImage.Height, GraphicsUnit.Pixel, attributes);

            return resultImage;
        }

        #region 不安全代码 需要设置项目允许不安全代码 
        /// <summary>
        /// 方法二  设置图像透明度，若原图不为32位ARGB格式，则自动转换为32位ARGB输出
        /// </summary>
        /// <param name="src">原图</param>
        /// <param name="transparency">透明度（0~1之间双精度浮点数）</param>
        //public static unsafe void SetTransparent(ref Bitmap src, double transparency)
        //{
        //    if (transparency < 0.0 || transparency > 1.0)
        //        throw new ArgumentOutOfRangeException("透明度必须为0~1之间的双精度浮点数");

        //    BitmapData srcData;
        //    Rectangle rect;
        //    byte* p;

        //    rect = new Rectangle(0, 0, src.Width, src.Height);
        //    src = src.Clone(rect, PixelFormat.Format32bppArgb);
        //    //转换到32位，否则丢失透明度
        //    srcData = src.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
        //    //锁定字节数组到内存
        //    p = (byte*)srcData.Scan0.ToPointer();
        //    //获取字节数组在内存中地址
        //    if (src.PixelFormat == PixelFormat.Format32bppArgb)
        //    {
        //        //如果是32位图片，有Alpha通道，以原Alpha通道的值为基准
        //        p += 3;
        //        //默认修改Alpha通道的值
        //        for (int i = 0; i < srcData.Stride * srcData.Height; i += 4)
        //        {
        //            *p = (byte)(*p * transparency);
        //            //原Alpha通道值*透明度
        //            p += 4;
        //        }
        //    }
        //    else
        //    {
        //        //不是32位图片，无Alpha通道，以Alpha通道最大值255为基准
        //        p += 3;
        //        //默认修改Alpha通道的值
        //        for (int i = 0; i < srcData.Stride * srcData.Height; i += 4)
        //        {
        //            *p = (byte)(255 * transparency);
        //            //255*透明度
        //            p += 4;
        //        }
        //    }
        //    src.UnlockBits(srcData);
        //    //解锁
        //}
        #endregion
    }
}
