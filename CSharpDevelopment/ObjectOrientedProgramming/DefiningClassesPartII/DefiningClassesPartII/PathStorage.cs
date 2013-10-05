using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DefiningClassesPartII
{
    public static class PathStorage
    {
        public static void SavePath(string filePath, List<Point3D> points)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var p in points)
                {
                    writer.WriteLine(p.X +" "+ p.Y +" "+ p.Z);
                }
            }
        }

        public static List<Point3D> LoadPath(string filePath)
        {
            List<Point3D> result = new List<Point3D>();
            string[] line;
            using (StreamReader reader = new StreamReader(filePath))
            {
                line = reader.ReadLine().Split(' ');
                result.Add(new Point3D(Convert.ToDouble(line[0]), Convert.ToDouble(line[1]), Convert.ToDouble(line[2])));
            }
            return result;
        }
    }
}
