using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EntityControl
{
    class Rocket_NPC : Rocket
    {
        int[] homeCoordinates { get; set; } //respawn point
        public bool IsAlive { get; set; } //prevent countdown of launcher
        public Rocket_NPC(int locX, int locY, float gravValue, float thrustPow, int rotatePow) : base(locX, locY,gravValue, (int)(thrustPow * 0.8), rotatePow)
        {
            character.Fill = Brushes.Orange; //to differentiate from others
            homeCoordinates = new int[] { locX, locY };
            IsAlive = false; //starts dead
            character.Visibility = Visibility.Hidden;
            character.Height = 8;
            character.Width = 4;
        }

        public override void UniversalUpdate()
        {
            base.UniversalUpdate();
            character.Fill = Brushes.Orange;
        }
        
        public void RotateToPlayer(int playerPointX, int playerPointY)
        {
            int deltaAngle = Angle - GetAngleToEntity(playerPointX, playerPointY); //pulls angle of player. Freaks when player is underneath rocket
            if (playerPointY > character.Margin.Top) { deltaAngle = 0 - deltaAngle; } //seems to force the rocket into a deathspin, good for next line
            if (Math.Abs(Angle) > 180) { Angle = 0; } //makes deathspin last for little time
            if (deltaAngle > 0)
            { Rotate(0 - RotatePower); }
            else if (deltaAngle < 0)
            { Rotate(RotatePower); }
        }
        public void Death()
        {
            AccelX = 0; AccelY = 0; VelX = 0; VelY = 0; Angle = 0; //return to zero. Just feels right to have this here
            IsAlive = false;
            character.Margin = new Thickness(homeCoordinates[0], homeCoordinates[1], 0, 0);
        }
        public void Spawning()
        {
            AccelX = 0; AccelY = 0; VelX = 0; VelY = 0; Angle = 0;//stops it flying down at start
            IsAlive = true;
            character.Margin = new Thickness(homeCoordinates[0], homeCoordinates[1], 0, 0); //change location
        }

        public void UpdateAppearance()
        {
            if (!IsAlive)
            { character.Visibility = Visibility.Hidden; }
            else { character.Visibility = Visibility.Visible; }
        }
    }
}
