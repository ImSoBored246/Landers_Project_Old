using System.Windows;
using System.Windows.Media;
using System;
using System.Windows.Shapes;

namespace EntityControl
{
    class Entity
    {
        public bool IsLandingPad = false; 

        public Rectangle character = new Rectangle();
        
        public int GetAngleToEntity(int coordX, int coordY)
        {
            return (int)(Math.Atan((coordX - character.Margin.Left) / (0 - (coordY + 5 - character.Margin.Top))) * 180 / Math.PI);
        }

        public Entity(int sizeX, int sizeY, int locX, int locY)
        {
            character.HorizontalAlignment = HorizontalAlignment.Left;
            character.VerticalAlignment = VerticalAlignment.Top;
            character.Height = sizeY;
            character.Width = sizeX;
            character.Margin = new Thickness(locX, locY, 0, 0);
            character.Fill = Brushes.Red;
            character.StrokeThickness = 0;
        }
    }
}