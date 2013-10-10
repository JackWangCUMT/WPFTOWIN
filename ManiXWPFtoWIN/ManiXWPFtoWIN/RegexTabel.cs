using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ManiXWPFtoWIN
{
    public static class RegexTabel
    {
        #region REGEX USED

        //SolidBrush
        public static Regex Solidbrush_regex = new Regex(@"^\s*<SolidColorBrush\s*x:Key=\""(.*?)\""\s*Color=\""(.*?)\""\s*/>\s*$");

        //LinearGradientBrush
        public static Regex LinearGradientBrush_regex = new Regex(@"^\s*<LinearGradientBrush|X:KEY=\""(\w*)\""|StartPoint=\""(\d+.?\d*,\d+.?\d*)\""|EndPoint=\""(\d+.?\d*,\d+.?\d*)\""|>\s*$", RegexOptions.IgnoreCase);
        public static Regex LinearEnd_regex = new Regex(@"^\s*</LinearGradientBrush>\s*$");


        //RadialGradientBrush
        public static Regex RadialGradientBrush_regex = new Regex(@"^\s*<RadialGradientBrush|" +
                                                                @"x:Key=\""(\w*)\""|" +
                                                                @"RadiusY=\""(.*?)\""|" +
                                                                @"Center=\""(.*?)\""|" +
                                                                @"GradientOrigin=\""(.*?)\""|" +
                                                                @"RadiusX=\""(.*?)\""|>\s*$");

        
        //GradientStop
        public static Regex GradientStopColoroffset_regex = new Regex(@"^\s*<GradientStop|Color=\""(.*?)\""\s*|Offset=\""(.*?)\""\s*|/>\s*$");

        #endregion
    }
}
