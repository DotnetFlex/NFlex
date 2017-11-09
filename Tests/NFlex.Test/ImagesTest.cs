using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Test
{
    public class ImagesTest
    {
        string imageFile = @"F:\TempResources\test.png";
        string thumFolder = @"F:\TempResources\ThumImages";

        [Fact]
        public void CreateWH()
        {
            CreateThumImage(400, 300, ThumbnailMode.WidthHeight);
        }


        [Fact]
        public void CreateW()
        {
            CreateThumImage(300, 400, ThumbnailMode.Width);
        }

        [Fact]
        public void CreateH()
        {
            CreateThumImage(400, 300, ThumbnailMode.Height);
        }

        [Fact]
        public void CreateCut()
        {
            CreateThumImage(550, 472, ThumbnailMode.Cut);
        }

        private void CreateThumImage(int width,int height,ThumbnailMode mode)
        {
            var image = Images.CreateThumbnail(imageFile, width, height, mode);
            string newFileName = string.Format("{0}x{1}_{2}.jpg", width, height, mode.ToString());
            newFileName = Path.Combine(thumFolder, newFileName);
            image.Save(newFileName,ImageFormat.Jpeg);
        }
    }
}
