using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;

namespace GeometryPainting
{
    public static class AddColors
    {
        private static Dictionary<Segment, Color> colorDict = new Dictionary<Segment, Color>();

        public static void SetColor(this Segment segment, Color color)
        {
            colorDict[segment] = color;
        }
        
        public static Color GetColor(this Segment segment)
        {
            return colorDict.ContainsKey(segment) ? colorDict[segment] : Color.Black;
        }
    }
}