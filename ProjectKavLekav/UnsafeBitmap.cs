using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectKavLekav
{
    public unsafe class UnsafeBitmap
    {
        readonly Bitmap _bitmap;

        private int _localWidth;
        BitmapData _bitmapData;
        Byte* _pBase = null;

        public int Width { get { return _bitmap.Width; } }
        public int Height { get { return _bitmap.Height; } }

        public UnsafeBitmap(string path)
        {
            _bitmap = new Bitmap(path);
        }

        public UnsafeBitmap(Bitmap bitmap)
        {
            _bitmap = new Bitmap(bitmap);
        }

        public UnsafeBitmap(int width, int height)
        {
            _bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        }

        public void Dispose()
        {
            _bitmap.Dispose();
        }

        public Bitmap Bitmap
        {
            get
            {
                return (_bitmap);
            }
        }

        private Point PixelSize
        {
            get
            {
                var unit = GraphicsUnit.Pixel;
                var bounds = _bitmap.GetBounds(ref unit);
                return new Point((int)bounds.Width, (int)bounds.Height);
            }
        }

        public PixelData GetPixel(int x, int y)
        {
            var returnValue = *PixelAt(x, y);
            return returnValue;
        }

        public void SetPixel(int x, int y, PixelData color)
        {
            var pixel = PixelAt(x, y);
            *pixel = color;
        }

        public void SetPixel(int x, int y, Color color)
        {
            var pixel = PixelAt(x, y);
            var pixelData = new PixelData { R = color.R, G = color.G, B = color.B };
            *pixel = pixelData;
        }

        private PixelData* PixelAt(int x, int y)
        {
            return (PixelData*)(_pBase + y * _localWidth + x * sizeof(PixelData));
        }

        public void LockBitmap()
        {
            var unit = GraphicsUnit.Pixel;
            var boundsF = _bitmap.GetBounds(ref unit);
            var bounds = new Rectangle((int)boundsF.X,
           (int)boundsF.Y,
           (int)boundsF.Width,
           (int)boundsF.Height);
            _localWidth = (int)boundsF.Width * sizeof(PixelData);
            if (_localWidth % 4 != 0)
            {
                _localWidth = 4 * (_localWidth / 4 + 1);
            }
            _bitmapData =
           _bitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            _pBase = (Byte*)_bitmapData.Scan0.ToPointer();
        }

        public void UnlockBitmap()
        {
            _bitmap.UnlockBits(_bitmapData);
            _bitmapData = null;
            _pBase = null;
        }
    }

    public struct PixelData
    {
        public byte R;
        public byte G;
        public byte B;

        public double GetBrightness()
        {
            return (double)(R + G + B) / 765;
        }
    }
}
