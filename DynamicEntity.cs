using System;
using System.Windows;
using System.Windows.Media;

namespace EntityControl
{
    class DynamicEntity : Entity
    {
        internal const double PI = Math.PI;
        public DynamicEntity(int sizeX, int sizeY, int locX, int locY, int velX, int velY, float gravValue) : base(sizeX, sizeY, locX, locY)
        {
            VelX = velX;
            VelY = velY;
            GravityPower = gravValue;
        }

        public DynamicEntity(int sizeX, int sizeY, int locX, int locY, float gravValue) : base(sizeX, sizeY, locX, locY)
        {
            VelX = 0;
            VelY = 0;
            GravityPower = gravValue;
        }

        public int VelX { get; set; }
        public int VelY { get; set; }
        public int AccelX { get; set; }
        public int AccelY { get; set; }
        public float GravityPower { get; set; }
        public int Angle { get; set; }

        public void UpdateVelocity()
        {
            AccelY -= (int)Math.Round(GravityPower);
            VelX -= AccelX;
            VelY -= AccelY;
            AccelX = 0; AccelY = 0;
            
        }

        public void Rotate(int angle)
        {
            RotateTransform rotateTransform = new RotateTransform(angle + Angle, character.Width / 2, character.Height / 2);
            character.RenderTransform = rotateTransform;
            Angle += angle;
        }

        public void UpdateLocation()
        {
            character.Margin = new Thickness(character.Margin.Left + (0.01 * VelX), character.Margin.Top + (VelY * 0.01), 0, 0);
        }

        public virtual void UniversalUpdate()
        {
            //calculate new velocity and reset acceleration
            UpdateVelocity();
            //calculate new location
            UpdateLocation();
        }
    }
}
