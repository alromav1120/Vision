using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bordes
{
    class Bitmap24
    {
        private System.Drawing.Bitmap bm;

        public Bitmap24(System.Drawing.Bitmap bm)
        {
            // TODO: Complete member initialization
            this.bm = bm;
        }

        internal void LockBitmap()
        {
            throw new NotImplementedException();
        }
    }
}
