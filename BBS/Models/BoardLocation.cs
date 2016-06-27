using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Models
{
    public class BoardLocation
    {
        public BoardLocation()
        {
        }

        public BoardLocation(int y, int x) : this()
        {
            Y = y;
            X = x;
            IsPitchBorder = true;

            if ((y == UP) && (x == LEFT))
                Type = LocationType.LeftHomeCorner;
            else if ((y == UP) && (x == RIGHT))
                Type = LocationType.RightHomeCorner;
            else if ((Y == DOWN) && (x == RIGHT))
                Type = LocationType.RightAwayCorner;
            else if ((Y == DOWN) && (x == LEFT))
                Type = LocationType.LeftAwayCorner;
            else if (Y == UP)
                Type = LocationType.HomeTouchDownLine;
            else if (X == RIGHT)
                Type = LocationType.RightSide;
            else if (Y == DOWN)
                Type = LocationType.AwayTouchDownLine;
            else if (X == LEFT)
                Type = LocationType.LeftSide;
            else if ((X > LEFT) && (X < RIGHT) && (Y > UP) && (Y < DOWN))
                Type = LocationType.InnerPitch;
            else
                Type = LocationType.Unknown;

            if ((Type == LocationType.InnerPitch) || (Type == LocationType.Unknown))
                IsPitchBorder = false;
        }

        private const int LEFT = 0;
        private const int UP = 0;
        private const int RIGHT = 14;
        private const int DOWN = 25;

        public int Y { get; protected set; }
        public int X { get; protected set; }
        public bool IsPitchBorder { get; protected set; }
        public LocationType Type { get; protected set; }

        public override string ToString()
        {
            return X + "," + Y;
        }

        public static bool operator ==(BoardLocation one, BoardLocation other)
        {
            if ((one.X == other.X) && (one.Y == other.Y))
                return true;
            else
                return false;
        }

        public static bool operator !=(BoardLocation one, BoardLocation other)
        {
            return !(one == other);
        }

        public static BoardLocation operator +(BoardLocation initialLocation, BoardLocation dispersion)
        {
            return new BoardLocation(initialLocation.Y + dispersion.Y, initialLocation.X + dispersion.X);
        }

        public static RelativeLocation operator -(BoardLocation position, BoardLocation referencePosition)
        {
            if (position == referencePosition)
                return RelativeLocation.SameLocation;
            else if ((position.X > referencePosition.X) && (position.Y > referencePosition.Y))
                return RelativeLocation.RightDown;
            else if ((position.X > referencePosition.X) && (position.Y < referencePosition.Y))
                return RelativeLocation.RightUp;
            else if ((position.X < referencePosition.X) && (position.Y > referencePosition.Y))
                return RelativeLocation.LeftDown;
            else if ((position.X < referencePosition.X) && (position.Y < referencePosition.Y))
                return RelativeLocation.LeftUp;
            else if (position.X > referencePosition.X)
                return RelativeLocation.Right;
            else if (position.X < referencePosition.X)
                return RelativeLocation.Left;
            else if (position.Y > referencePosition.Y)
                return RelativeLocation.Up;
            else if (position.Y < referencePosition.Y)
                return RelativeLocation.Down;
            else
                return RelativeLocation.Unknown;
        }
    }

    public enum LocationType
    {
        Unknown = -1,        
        InnerPitch = 0,
        HomeTouchDownLine = 1,
        RightHomeCorner = 2,
        RightSide = 3,
        RightAwayCorner = 4,
        AwayTouchDownLine = 5,
        LeftAwayCorner = 6,
        LeftSide = 7,        
        LeftHomeCorner = 8
    }


    public enum RelativeLocation
    {
        Unknown = -1,        
        SameLocation = 0,        
        Down = 1,        
        LeftDown = 2,        
        Left = 3,        
        LeftUp = 8,        
        Up = 5,
        RightUp = 6,        
        Right = 7,        
        RightDown = 8
    }
}
