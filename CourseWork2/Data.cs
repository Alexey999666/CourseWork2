using CourseWork2.ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork2
{
    public static class Data
    {
        public static Экскурсии экскурсии;
        public static Экспонат экспонат;
    }
    public static class Flags
    {
        public static bool FlagADD { get; set; } = false;
        public static bool FlagEdit { get; set; } = false;
        public static bool FlagView { get; set; } = false;
    }
}
