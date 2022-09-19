using Krypton_Core;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KryptonGUI.Render
{
    public class RenderMap : IDrawable
    {
        public Api Api;
        public double X;
        public double Y;
        public int MapID;

        #region convert
        private double convertX { get; set; }
        private double convertY { get; set; }
        #endregion
        #region time
        public int totalS;
        private int Sec;
        private int Min;
        private int Hour;

        #endregion

        public RenderMap(Api api, double x, double y, int MapID, int totalS)
        {
            this.Api = api;
            this.X = x;
            this.Y = y;
            this.MapID = MapID;
            this.totalS = totalS;

            convertX = (double)x / 20500;
            convertY = (double)y / 13000;
            if (MapID == 29 || MapID == 16 || MapID == 91 || MapID == 93)
            {
                convertX = (double)x / 41000;
                convertY = (double)y / 26000;
            }
        }
        private List<Task> Tasks = new();
        public async void Draw(ICanvas canvas, RectF dirtyRect)
        {
            
            Tasks.Add(DrawBases(canvas));
            Tasks.Add(DrawPorts(canvas));
            Tasks.Add(DrawPlayer(canvas));
            await Task.WhenAll(Tasks);
        }  
        private async Task DrawPlayer(ICanvas canvas)
        {
            canvas.FillColor = Colors.White;
            canvas.FillEllipse((float)(Api._user.Position.X * convertX), (float)(Api._user.Position.Y * convertY), 10, 10);
            
        }
        private async Task DrawBases(ICanvas canvas)
        {
            canvas.FillColor = Colors.DarkBlue;
            foreach(var Base in Api._user.BasesPorts.Bases.ToList())
            {
                canvas.FillEllipse((float)(Base.X * convertX), (float)(Base.Y * convertY), 8, 8);
            }

        }
        private async Task DrawPorts(ICanvas canvas)
        {
            canvas.StrokeColor = Colors.White;
            foreach (var port in Api._user.BasesPorts.gates.ToList())
            {
                canvas.DrawEllipse(ReturnFloat(port.X) * (float)convertX, ReturnFloat(port.Y) * (float)convertY, 20, 20);
            }
        }
        private float ReturnFloat(int num)
        {
            return (float)num;
        }

       
    }
    
}
