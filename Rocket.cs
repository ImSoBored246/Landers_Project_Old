using System;
using System.Windows.Media;

namespace EntityControl
{
    class Rocket : DynamicEntity
    {
        public Rocket(int locX, int locY, float gravValue, float thrustPow, int rotatePow) : base(10, 20, locX, locY, 0, 0, gravValue)
        {
            ThrustPower = thrustPow; //thrust should always be at least 2x gravity
            RotatePower = rotatePow; //5 seems ideal, but can be bumped to 10 for a worst-case scenario
        }
        public float ThrustPower { get; set; }
        public int RotatePower { get; set; }
        public void Thrust()
        {
            AccelX = 0 - (int)Math.Round(ThrustPower * Math.Sin(PI * Angle / 180));
            AccelY = (int)Math.Round(ThrustPower * Math.Cos(PI * Angle / 180));
        }
        public override void UniversalUpdate()
        {
            character.Fill = Brushes.Aqua; //aqua in this case means "not dead". This is temporary!
            //calculate new velocity and reset acceleration
            UpdateVelocity();
            //calculate new location
            UpdateLocation();
        }
        
        public void DeathFlag(ushort reason)
        {
            VelX = 0 - VelX;
            VelY = 0 - VelY;
            UpdateLocation(); //these three lines move the player back, to undo clipping. Can be commented out to test extremity of clip
            SolidColorBrush[] deathColour = new SolidColorBrush[5] { Brushes.White, Brushes.OrangeRed, Brushes.Orange, Brushes.Yellow, Brushes.Lime };
            character.Fill = deathColour[reason];
        }
    }
}