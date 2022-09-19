
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KryptonGUI.Render
{
    public class RenderMap : IDrawable
    {
     
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 6;
            canvas.DrawLine(10, 10, 90, 100);

            canvas.StrokeColor = Colors.Green;
            canvas.StrokeSize = 6;
            canvas.DrawLine(100, 100, 300, 800);

        }

       
    }
    
}
