using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GoDartingLogic
{
   public static class GameGeometry
    {
        public static Point HorizontalBar { get; set; }
        public static int HorizontalBarLength { get; set; }
        public static Point VerticalBar { get; set; }
        public static int VerticalBarLength { get; set; }
        public static Point DartBoard { get; set; }
        public static int DartBoardLength { get; set; }
        public static Point DartBoardCenter { get; set; }
        public static float BarToDartRatio { get; set; }
        public static int VerticalBarCenter { get; set; }
        public static int HorizontalBarCenter { get; set; }

        public static void CalculateAll()
        {
            DartBoardCenter = new Point { X = DartBoard.X + (DartBoardLength / 2), Y = DartBoard.Y + (DartBoardLength / 2) };
            BarToDartRatio = HorizontalBarLength / DartBoardLength;
            HorizontalBarCenter = HorizontalBar.X + (HorizontalBarLength / 2);
            VerticalBarCenter = VerticalBar.Y + (VerticalBarLength / 2);
        }

    }
}
