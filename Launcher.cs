using System.Windows.Input;
using System.Collections.Generic;
using EntityControl;
using System;
using System.Windows.Media;

namespace EntityControl
{
    class Launcher : Entity
    {
        int TimeToRespawn { get; set; }
        int RespawnMaxTime { get; set; }
        public bool FireMissile { get; set; }
        public bool IsLOS { get; set; }
        public bool ChildAlive { get; set; }
        public Launcher(int locX, int locY, int ttRespawn) : base(20, 5, locX, locY)
        {
            RespawnMaxTime = ttRespawn;
            TimeToRespawn = RespawnMaxTime * 4;
            character.Fill = Brushes.Yellow; //default. should change when line of sight
            FireMissile = false;
        }

        public void CheckFire()
        {
            if (!(TimeToRespawn == 0) && !(TimeToRespawn == RespawnMaxTime))
            { FireMissile = false; goto BREAKPOINT; } //skip the next if/else. could use elseif, but this felt right for some reason
            if (ChildAlive)
            { TimeToRespawn = RespawnMaxTime; FireMissile = false; }
            else
            { TimeToRespawn = RespawnMaxTime; }
        BREAKPOINT:;
        }

        public void NextFrame()
        {
            if (!ChildAlive) { TimeToRespawn--; } //if the launcher is idle, countdown
            if (TimeToRespawn > 10) { character.Fill = Brushes.DeepSkyBlue; } //if not about to fire, deep sky blue
            else if (ChildAlive) { character.Fill = Brushes.DarkBlue; } //if its kid is alive, dark blue
        }

        public bool IsLineOfSight(double playerPointX, double playerPointY, Entity terrain)
        {
            #region old code
            /* The following is old code that I was unable to make work properly. It is being kept for documentation
            if (((lineData[0] * terrain.character.Margin.Left) > terrain.character.Margin.Left && (lineData[0] * terrain.character.Margin.Left) < terrain.character.Margin.Left + terrain.character.Width))
            { return false; } //if not in between margin and margin + width (x-ax) 
            else if (((lineData[0] * terrain.character.Margin.Top) > terrain.character.Margin.Top && (lineData[0] * terrain.character.Margin.Top) < terrain.character.Margin.Top + terrain.character.Height))
            { return false; } //if not in between margin and margin + height (y-ax)
            return true; //must be los
            */
            #endregion
            if (character.Margin.Top < playerPointY)
            {
                return false;
            }
            int angleTerrain1 = GetAngleToEntity((int)terrain.character.Margin.Left, (int)(terrain.character.Margin.Top + terrain.character.Height));
            int angleTerrain2 = GetAngleToEntity((int)(terrain.character.Margin.Left + terrain.character.Width), (int)(terrain.character.Margin.Top + terrain.character.Height));
            int anglePlayer = GetAngleToEntity((int)playerPointX, (int)playerPointY);
            if (angleTerrain1 < anglePlayer && anglePlayer < angleTerrain2)
            {
                return false;
            }
            return true;
        }
    }
}