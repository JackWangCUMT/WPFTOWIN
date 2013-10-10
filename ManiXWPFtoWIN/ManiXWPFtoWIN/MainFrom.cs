using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ManiX;

namespace ManiXWPFtoWIN
{
    public partial class MainFrom : Form
    {
        #region Fields

        //Create style for highlighting
        static Brush te = new  SolidBrush(Color.FromArgb(43,145,175));
        TextStyle Bluestyle = new TextStyle(te, null, FontStyle.Regular);
        TextStyle redsty = new TextStyle(Brushes.Red, null, FontStyle.Regular);

        //LinearGradientBrush
        List<Tuple<int, string, PointF[], RectangleF,LinearGradientMode,float,bool>> LinearGradientBrush_Coll =   new List<Tuple<int,string,PointF[],RectangleF,LinearGradientMode,float,bool>>();
        //GradientStop
        List<Tuple<int, Color, float>> GradientStop_Coll = new List<Tuple<int, Color, float>>();
        //SolidColorBrush
        List<Tuple<int, string, Color>> SolidBrish_Coll = new List<Tuple<int, string, Color>>();

        //RadialGradientBrush
        List<Tuple<int, string, double[], PointF, PointF>> RadialGradientBrush_Coll = new List<Tuple<int, string, double[], PointF, PointF>>();

        List<Tuple<LinearGradientBrush,string>> LinearBrush = new List<Tuple<LinearGradientBrush,string>>();
        List<Tuple<PathGradientBrush, string>> PathBrush = new List<Tuple<PathGradientBrush, string>>();
        List<Tuple<SolidBrush, string>> SolidBru = new List<Tuple<SolidBrush, string>>();

        Color currentLineColor = Color.FromArgb(100, 210, 210, 255);
        Color changedLineColor = Color.FromArgb(255, 230, 230, 255);


        #endregion

        #region Constructor

        public MainFrom()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void CSharpCode_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle();
            e.ChangedRange.SetStyle(Bluestyle, @"\b(LinearGradientBrush|ColorBlend|PointF|Color|ColorTranslator|SolidBrush)\b", RegexOptions.Singleline);
        }

        private void convert_btn_Click(object sender, EventArgs e)
        {
            ConvertWPFtoWIN();
        }

        //Open File Dialog
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "Select File to Edit";
            openfile.Filter = "AllFiles (*.*)|*.*";


            if (Directory.Exists(Filepath))
            {
                openfile.InitialDirectory = Filepath;
            }
            else
            {
                openfile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    byte[] readBuffer = System.IO.File.ReadAllBytes(openfile.FileName);
                    string Texts = System.Text.Encoding.ASCII.GetString(readBuffer);
                    string[] pgmcodes = Texts.ToString().Split('\n');
                    XamlCode.Text = string.Empty;
                    for (int i = 0; i < pgmcodes.Length; i++)
                    {
                        XamlCode.AppendText(pgmcodes[i] + "\n");
                    }
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        //Save File Dialog
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            SaveFileDialog Savefil = new SaveFileDialog();
            Savefil.Title = "Save File As";
            Savefil.FileName = "NewProgram";
            Savefil.Filter = "CSharp Class (*.cs)|*.cs";

            if (Directory.Exists(Filepath))
            {
                Savefil.InitialDirectory = Filepath;
            }
            else
            {
                Savefil.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            if (Savefil.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string myString;
                    using (System.IO.FileStream fs = System.IO.File.Create(Savefil.FileName))
                    {
                        myString = CSharpCode.Text;
                        byte[] myByteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(myString);
                        foreach (byte c in myByteArray)
                        {
                            fs.WriteByte(c);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Unable to Save");
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutbo = new AboutBox1();
            aboutbo.ShowDialog();
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Preview bi = new Preview();
            bi.LinearBrushColl = LinearBrush;
            bi.PathBrushColl = PathBrush;
            bi.SolidBrushColl = SolidBru;
            bi.ShowDialog();
        }

        #region Context Menu

        #region XAML Context

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XamlCode.Cut();
        }
       
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XamlCode.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XamlCode.Paste();
        }
     
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XamlCode.SelectAll();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XamlCode.Clear();
        }

        private void Xaml_Context_Opening(object sender, CancelEventArgs e)
        {
            cutToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem.Enabled = false;
            pasteToolStripMenuItem.Enabled = false;
            selectAllToolStripMenuItem.Enabled = false;
            clearAllToolStripMenuItem.Enabled = false;

            if (XamlCode.SelectedText.Length > 0)
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
            }

            if (Clipboard.ContainsText())
            {
                pasteToolStripMenuItem.Enabled = true;
            }

            if (XamlCode.Text.Length > 0)
            {
                selectAllToolStripMenuItem.Enabled = true;
                clearAllToolStripMenuItem.Enabled = true;
            }
        }

        #endregion 

        #region C# Context

        private void cutMenuItem1_Click(object sender, EventArgs e)
        {
            CSharpCode.Cut();
        }

        private void copyMenuItem2_Click(object sender, EventArgs e)
        {
            CSharpCode.Copy();
        }

        private void pasteMenuItem3_Click(object sender, EventArgs e)
        {
            CSharpCode.Paste();
        }

        private void selectallMenuItem5_Click(object sender, EventArgs e)
        {
            CSharpCode.SelectAll();
        }

        private void clearallMenuItem6_Click(object sender, EventArgs e)
        {
            CSharpCode.Clear();
        }

        private void CSharp_Context_Opening(object sender, CancelEventArgs e)
        {
            cutMenuItem1.Enabled = false;
            copyMenuItem2.Enabled = false;
            pasteMenuItem3.Enabled = false;
            selectallMenuItem5.Enabled = false;
            clearallMenuItem6.Enabled = false;

            if (CSharpCode.SelectedText.Length > 0)
            {
                cutMenuItem1.Enabled = true;
                copyMenuItem2.Enabled = true;
            }

            if (Clipboard.ContainsText())
            {
                pasteMenuItem3.Enabled = true;
            }

            if (CSharpCode.Text.Length > 0)
            {
                selectallMenuItem5.Enabled = true;
                clearallMenuItem6.Enabled = true;
            }
        }

        #endregion

        #endregion

        #endregion

        #region Methods

        //Convert WPF Brushes to WIN
        private void ConvertWPFtoWIN()
        {

        
            int indx = 0;
            LinearGradientBrush_Coll.Clear();
            SolidBrish_Coll.Clear();
            GradientStop_Coll.Clear();
            LinearBrush.Clear();
            PathBrush.Clear();
            RadialGradientBrush_Coll.Clear();
            SolidBru.Clear();
            CSharpCode.Text = string.Empty;

            string[] LinestoConvert = new string[XamlCode.LinesCount];

            #region ManiX_Code

            #region get XAML Data

            try
            {
                for (int i = 0; i < XamlCode.LinesCount; i++)
                {
                    //Line To Process
                    string LinetoProcess = XamlCode.Lines[i].ToString();

                    #region LinearGradientBrush

                    if (LinetoProcess.Contains("<LinearGradientBrush"))
                    {
                        Tuple<int, string, PointF[], RectangleF, LinearGradientMode, float, bool> tmp = LinearGradientBrush_(indx, " " + LinetoProcess);
                        if (tmp != null)
                        {
                            LinearGradientBrush_Coll.Add(tmp);
                        }
                    }
                    else if (LinetoProcess.Contains("</LinearGradientBrush>"))
                    {
                        indx = indx + 1;
                    }
                    else if (LinetoProcess.Contains("<GradientStop"))
                    {
                        Tuple<int, Color, float> tmp = GradiendStopColorOffset_(indx, " " + LinetoProcess);
                        if (tmp != null)
                        {
                            GradientStop_Coll.Add(tmp);
                        }
                    }

                    #endregion

                    #region Radial

                    else if (LinetoProcess.Contains("<RadialGradientBrush"))
                    {
                        Tuple<int, string, double[], PointF, PointF> tmp = RadialGradientBrush_(indx, " " + LinetoProcess);
                        if (tmp != null)
                        {
                            RadialGradientBrush_Coll.Add(tmp);
                        }
                    }
                    else if (LinetoProcess.Contains("</RadialGradientBrush>"))
                    {
                        indx = indx + 1;
                    }

                    #endregion

                    #region SolidBrush

                    else if (LinetoProcess.Contains("<SolidColorBrush"))
                    {
                        Tuple<int, string, Color> tmp = SolidBrush_(indx, " " + LinetoProcess);
                        if (tmp != null)
                        {
                            SolidBrish_Coll.Add(tmp);
                        }
                    }

                    #endregion
                }
            }
            catch { }

            #endregion

            #region set C# Data

            int totals = LinearGradientBrush_Coll.Count + SolidBrish_Coll.Count + RadialGradientBrush_Coll.Count;

            string CSharpCode_ = "//Thanks for Using My WPF -> WIN Code Converter" + Environment.NewLine +
                                "//ManiX (Coding is Fun)" + Environment.NewLine +
                                "//EMAIL ID : manivannangd@gmail.com" + Environment.NewLine + Environment.NewLine +
                                "using System;" + Environment.NewLine +
                                "using System.Drawing;" + Environment.NewLine +
                                "using System.Drawing.Drawing2D;" + Environment.NewLine +
                                Environment.NewLine + Environment.NewLine +
                                "namespace ManiX " + Environment.NewLine +
                                "{" + Environment.NewLine +
                                "    public sealed class XBrush" + Environment.NewLine +
                                "    {" + Environment.NewLine;

            if (totals > 0)
            {
                for (int i = 0; i < totals; i++)
                {
                    #region LinearGradientBrush

                    if (LinearGradientBrush_Coll.Count > 0)
                    {
                        for (int j = 0; j < LinearGradientBrush_Coll.Count; j++)
                        {
                            if (LinearGradientBrush_Coll[j].Item1 == i)
                            {
                                float[] floararr = { 0, 1 };
                                Color[] colorarr = { Color.White, Color.Black };
                                Color fir = Color.White;
                                Color sec = Color.Black;

                                #region Point

                                string pointorRectstring_ = string.Empty;

                                if (LinearGradientBrush_Coll[j].Item3 != null)
                                {
                                    string val1x = "0";
                                    string val1y = "0";
                                    string val2x = "0";
                                    string val2y = "0";
                                    if (LinearGradientBrush_Coll[j].Item3[0].X != 0)
                                        val1x = "(float)(Rect.Width/(1/" + LinearGradientBrush_Coll[j].Item3[0].X + "))";
                                    if (LinearGradientBrush_Coll[j].Item3[0].Y != 0)
                                        val1y = "(float)(Rect.Height/(1/" + LinearGradientBrush_Coll[j].Item3[0].Y + "))";
                                    if (LinearGradientBrush_Coll[j].Item3[1].X != 0)
                                        val2x = "(float)(Rect.Width/(1/" + LinearGradientBrush_Coll[j].Item3[1].X + "))";
                                    if (LinearGradientBrush_Coll[j].Item3[1].Y != 0)
                                        val2y = "(float)(Rect.Height/(1/" + LinearGradientBrush_Coll[j].Item3[1].Y + "))";

                                    pointorRectstring_ = "new PointF(" +val1x+"," +val1y +") " +
                                                  ",new PointF(" + val2x + ", " + val2y + ") ";
                                }
                                else
                                {
                                    pointorRectstring_ = "new Rectangle(" + LinearGradientBrush_Coll[j].Item4.X.ToString() + "," +
                                        LinearGradientBrush_Coll[j].Item4.Y.ToString() + "," +
                                        LinearGradientBrush_Coll[j].Item4.Width.ToString() + "," +
                                        LinearGradientBrush_Coll[j].Item4.Height.ToString() + ")";
                                }

                                #endregion

                                #region Linear Gradient brush

                                string brushname = LinearGradientBrush_Coll[j].Item2;

                                if (brushname == string.Empty)
                                    brushname = "LinearBrush" + i;

                                string Colorstring = string.Empty;
                                ArrayList colcoll;
                                ArrayList floarcol;

                                string gstop = GradientStopToString(i, brushname, out Colorstring, out fir, out sec, out colcoll, out floarcol);

                              

                                CSharpCode_ += Environment.NewLine +
                                               "        public static LinearGradientBrush " + brushname + "(Rectangle Rect)" + Environment.NewLine +
                                               "        {" + Environment.NewLine;

                                CSharpCode_ += "            LinearGradientBrush " + brushname + "_ = new LinearGradientBrush(" + pointorRectstring_ + ", " + Colorstring + ");" + Environment.NewLine + Environment.NewLine;
                                CSharpCode_ += gstop;
                                CSharpCode_ += "            return " + brushname + "_ ;" + Environment.NewLine + 
                                               "        }" + Environment.NewLine;

                                #endregion

                                #region Creating Brush

                                try
                                {
                                    
                                    Rectangle rect_ = new Rectangle(0, 0, 200, 200);
                                    LinearGradientBrush brushname_ = new LinearGradientBrush(rect_, fir, sec, LinearGradientMode.Vertical);
                                    ColorBlend bend_ = new ColorBlend();
                                    bend_.Colors = colcoll.ToArray(typeof(Color)) as Color[];
                                    bend_.Positions = floarcol.ToArray(typeof(float)) as float[];
                                    brushname_.InterpolationColors = bend_;

                                    LinearBrush.Add(new Tuple<LinearGradientBrush, string>(brushname_, brushname));
                                }
                                catch { }

                                #endregion
                            }
                        }
                    }

                    #endregion

                    #region RadialGradientBrush

                    if (RadialGradientBrush_Coll.Count > 0)
                    {
                        for (int j = 0; j < RadialGradientBrush_Coll.Count; j++)
                        {
                            if (RadialGradientBrush_Coll[j].Item1 == i)
                            {

                                float[] floararr = { 0, 1 };
                                Color[] colorarr = { Color.White, Color.Black };
                                Color fir = Color.White;
                                Color sec = Color.Black;
                                string Colorstring = string.Empty;
                                ArrayList colcoll;
                                ArrayList floarcol;

                                PointF centpo = new PointF(0,0);
                                PointF focpo = new PointF(0, 0);

                                #region Point

                                string Focalpoint = string.Empty;
                                string Centerpoint = string.Empty;

                                if (RadialGradientBrush_Coll[j].Item4 != null)
                                {
                                    string val1x = "0";
                                    string val1y = "0";

                                    float p1 = 0;
                                    float p2 = 0;

                                    if (RadialGradientBrush_Coll[j].Item4.X != 0)
                                    {
                                        val1x = "(float)(Rect.Width/(1/" + RadialGradientBrush_Coll[j].Item4.X + "))";
                                        p1 = (float)(300 / (1 / +RadialGradientBrush_Coll[j].Item4.X));
                                    }
                                    if (RadialGradientBrush_Coll[j].Item4.Y != 0)
                                    {
                                        val1y = "(float)(Rect.Height/(1/" + RadialGradientBrush_Coll[j].Item4.Y + "))";
                                        p2 = (float)(300 / (1 / +RadialGradientBrush_Coll[j].Item4.Y));
                                    }
                                    Centerpoint += "new PointF(" + val1x + ", " + val1y + ") ";

                                    centpo = new PointF(p1, p2);
                                }
                                if (RadialGradientBrush_Coll[j].Item5 != null)
                                {
                                    string val2x = "0";
                                    string val2y = "0";

                                    float p1 = 0;
                                    float p2 = 0;

                                    if (RadialGradientBrush_Coll[j].Item5.X != 0)
                                    {
                                        p1 = (float)(300 / (1 / +RadialGradientBrush_Coll[j].Item5.X));
                                        val2x = "(float)(Rect.Width/(1/" + RadialGradientBrush_Coll[j].Item5.X + "))";
                                    }
                                    if (RadialGradientBrush_Coll[j].Item5.Y != 0)
                                    {
                                        p1 = (float)(300 / (1 / +RadialGradientBrush_Coll[j].Item5.X));
                                        val2y = "(float)(Rect.Height/(1/" + RadialGradientBrush_Coll[j].Item5.Y + "))";
                                    }
                                    Focalpoint = "new PointF(" + val2x + ", " + val2y + ") ";
                                    focpo = new PointF(p1, p2);
                                }

                                #endregion

                                #region Radial Gradient brush

                                string brushname = RadialGradientBrush_Coll[j].Item2;

                                if (brushname.Trim() == string.Empty)
                                    brushname = "PathGradientBrush" + i;

                                string gstop = GradientStopToString(i, brushname, out Colorstring, out fir, out sec, out colcoll, out floarcol);

                                CSharpCode_ += Environment.NewLine +
                                               "        public static PathGradientBrush  " + brushname + "(Rectangle Rect)" + Environment.NewLine +
                                               "        {" + Environment.NewLine;

                                CSharpCode_ += "            GraphicsPath GPath_ = new GraphicsPath();" + Environment.NewLine +
                                               "            GPath_.AddRectangle(Rect);" + Environment.NewLine;

                                CSharpCode_ += "            PathGradientBrush " + brushname + "_ = new PathGradientBrush(GPath_);" + Environment.NewLine;

                                if (Centerpoint != string.Empty)
                                {
                                    CSharpCode_ += "            " + brushname + "_.CenterPoint = " + Focalpoint + ";" + Environment.NewLine;
                                }
                                if (Focalpoint != string.Empty)
                                    CSharpCode_ += "            " + brushname + "_.FocusScales = " + Centerpoint + ";" + Environment.NewLine;

                                CSharpCode_ += gstop;

                                CSharpCode_ += "            return "+ brushname +"_ ;" + Environment.NewLine + "        }" + Environment.NewLine;

                                #endregion

                                #region Creating Brush

                                try
                                {
                                    GraphicsPath pat = new GraphicsPath();
                                    pat.AddEllipse(new Rectangle(0, 0, 300, 300));
                                    PathGradientBrush pbru = new PathGradientBrush(pat);
                                    pbru.FocusScales = centpo;
                                    pbru.CenterPoint = focpo;
                                    ColorBlend bend_ = new ColorBlend();
                                    colcoll.Reverse();
                                    bend_.Colors = (colcoll.ToArray(typeof(Color)) as Color[]);
                                    bend_.Positions = floarcol.ToArray(typeof(float)) as float[];
                                    pbru.InterpolationColors = bend_;
                                    PathBrush.Add(new Tuple<PathGradientBrush, string>(pbru, brushname));
                                }
                                catch { }

                                #endregion
                            }
                        }
                    }

                    #endregion

                    #region Solid Brush

                    if (SolidBrish_Coll.Count > 0)
                    {
                        for (int j = 0; j < SolidBrish_Coll.Count; j++)
                        {
                            Color p = Color.Transparent;
                            if (SolidBrish_Coll[j].Item1 == i)
                            {
                                string brushname = SolidBrish_Coll[j].Item2;

                                if (brushname == string.Empty)
                                {
                                    brushname = "SolidBrush" + i;
                                }
                                string colnam = string.Empty;
                                if (SolidBrish_Coll[j].Item3.IsNamedColor)
                                {
                                    p = SolidBrish_Coll[j].Item3;
                                    colnam = "Color." + SolidBrish_Coll[j].Item3.Name;
                                }
                                else
                                {
                                    p = ColorTranslator.FromHtml("#" + SolidBrish_Coll[j].Item3.Name);
                                    colnam = "ColorTranslator.FromHtml(\"#" + SolidBrish_Coll[j].Item3.Name + "\")";
                                }

                                CSharpCode_ += Environment.NewLine + "        public static SolidBrush " + brushname + " = new SolidBrush(" + colnam + ");" + Environment.NewLine;

                                try
                                {
                                    SolidBrush b = new SolidBrush(p);
                                    SolidBru.Add(new Tuple<SolidBrush,string>(b,brushname));
                                }
                                catch { }
                            }
                        }
                    }

                    #endregion
                }
            }

            #endregion

            CSharpCode_ += Environment.NewLine + "    }" +
                           Environment.NewLine + "}";
            
            CSharpCode.Text = CSharpCode_;

            //PathGradientBrush PathGradie

            

            
            #endregion
        }

        //Convert WIN Brushes to WPF
        private void ConvertWINtoWPF()
        {
            // TODO
        }

        #region Brushes

        #region LinearGradientBrush

        private Tuple<int, string, PointF[], RectangleF, LinearGradientMode, float, bool> LinearGradientBrush_(int indx, string LinetoProcess)
        {
            MatchCollection _Match = RegexTabel.LinearGradientBrush_regex.Matches(LinetoProcess);

            if (_Match.Count > 0)
            {
                try
                {
                    PointF startpoint = new PointF(0, 0);
                    PointF endpoint = new PointF(0, 0);
                    PointF[] tmpp = null;
                    float tmpfloat1, tmpfloat2, tmpfloat3, tmpfloat4;
                    string _Name = string.Empty;
                    string[] _Point1 = null;
                    string[] _Point2 = null;

                    string txt = string.Empty;
                    for (int i = 0; i < _Match.Count; i++)
                    {
                        if (_Match[i].Groups[1].Value.Trim() != string.Empty)
                            _Name = _Match[i].Groups[1].Value;
                        if (_Match[i].Groups[2].Value.Trim() != string.Empty)
                            _Point1 = _Match[i].Groups[2].Value.Split(',');
                        if (_Match[i].Groups[3].Value.Trim() != string.Empty)
                            _Point2 = _Match[i].Groups[3].Value.Split(',');
                    }

                    if (_Point1.Length == 2)
                    {
                        tmpfloat1 = float.Parse(_Point1[0]);
                        tmpfloat2 = float.Parse(_Point1[1]);
                        startpoint = new PointF(tmpfloat1, tmpfloat2);
                    }
                    if (_Point2.Length == 2)
                    {
                        tmpfloat3 = float.Parse(_Point2[0]);
                        tmpfloat4 = float.Parse(_Point2[1]);
                        endpoint = new PointF(tmpfloat3, tmpfloat4);
                    }

                    tmpp = new PointF[] { startpoint, endpoint };
                    return new Tuple<int, string, PointF[], RectangleF, LinearGradientMode, float, bool>(indx, _Name, tmpp, new RectangleF(), LinearGradientMode.ForwardDiagonal, 0, false);
                }
                catch { }
            }
            return null;
        }

        #endregion

        #region Gradient Stops

        public Tuple<int, Color, float> GradiendStopColorOffset_(int indx, string LinetoProcess)
        {
            MatchCollection _Match = RegexTabel.GradientStopColoroffset_regex.Matches(LinetoProcess);
            if (_Match.Count > 0)
            {
                try
                {
                    Color tmpcol = Color.Transparent;
                    float tmpflo = 0;
                    for (int i = 0; i < _Match.Count; i++)
                    {
                        ColorConverter colorconvert = new ColorConverter();
                        if (_Match[i].Groups[1].Value != string.Empty)
                            tmpcol = (Color)colorconvert.ConvertFromString(_Match[i].Groups[1].Value.ToString());
                        if (_Match[i].Groups[2].Value != string.Empty)
                            tmpflo = float.Parse(_Match[i].Groups[2].Value.ToString());
                    }
                    return new Tuple<int, Color, float>(indx, tmpcol, tmpflo);
                }
                catch { }
            }
            return null;
        }

        public string GradientStopToString(int Indx, string BrushName,out string colorstrd, out Color FirstColor,out Color SecondColor,out ArrayList Colrar,out ArrayList floatar)
        {
            List<Tuple<Color, float>> GStops = new List<Tuple<Color, float>>();

            string ds = "Color.Transparent, Color.Transparent";
            Color Fs = Color.Transparent;
            Color Sc = Color.Transparent;
            FirstColor = Fs;
            SecondColor = Sc;
            colorstrd = ds;
            Colrar = new ArrayList();//new Color[] {Color.Transparent,Color.Transparent};
            floatar = new ArrayList(); //new float[] { 0, 1 };

            try
            {
                for (int i = 0; i < GradientStop_Coll.Count; i++)
                {
                    if (GradientStop_Coll[i].Item1 == Indx)
                        GStops.Add(new Tuple<Color, float>(GradientStop_Coll[i].Item2, GradientStop_Coll[i].Item3));
                }

                #region StartPoint

                bool startpos_ = false;
                int zeroidx = 0;

                for (int i = 0; i < GStops.Count; i++)
                {
                    if (GStops[i].Item2 == 0)
                    {
                        startpos_ = true;
                        zeroidx = i;
                    }
                }

                if (!startpos_)
                    GStops.Add(new Tuple<Color, float>(Color.Transparent, 0));

                GStops.Sort((a, b) => a.Item2.CompareTo(b.Item2));

                #endregion

                string colorstr = "new Color[] {";
                string flotstr = "new float[] {";

                for (int i = 0; i < GStops.Count; i++)
                {
                    FirstColor = GStops[0].Item1;
                    SecondColor = GStops[GStops.Count - 1].Item1;

                    if (GStops[i].Item1.IsNamedColor)
                    {
                        Colrar.Add(GStops[i].Item1);
                        colorstr += "Color." + GStops[i].Item1.Name + ", ";
                    }
                    else
                    {
                        Colrar.Add(ColorTranslator.FromHtml("#" + GStops[i].Item1.Name));

                        colorstr += "ColorTranslator.FromHtml(\"#" + GStops[i].Item1.Name + "\"), ";
                    }

                    floatar.Add(GStops[i].Item2);
                    flotstr += GStops[i].Item2.ToString() + "f, ";
                }

                string tp = colorstr.Remove(colorstr.Length - 2, 2);
                colorstr = tp + "}";

                string ep = flotstr.Remove(flotstr.Length - 2, 2);
                flotstr = ep + "}";


                string retstring = "            ColorBlend XBlend" + Indx + " = new ColorBlend();" + Environment.NewLine +
                                    "            XBlend" + Indx + ".Colors = " + colorstr + ";" + Environment.NewLine +
                                    "            XBlend" + Indx + ".Positions = " + flotstr + ";" + Environment.NewLine +
                                    "            " + BrushName + "_.InterpolationColors = XBlend" + Indx + ";" +
                                    Environment.NewLine + Environment.NewLine;

                if (FirstColor.IsNamedColor)
                    ds = "Color." + FirstColor.Name.ToString();
                else
                    ds = "Color.FromArgb(" + FirstColor.A + "," + FirstColor.R + "," + FirstColor.G + "," + FirstColor.B + ")";

                if (SecondColor.IsNamedColor)
                    ds += ", Color." + SecondColor.Name.ToString();
                else
                    ds += ", Color.FromArgb(" + SecondColor.A + "," + SecondColor.R + "," + SecondColor.G + "," + SecondColor.B + ")";

                colorstrd = ds;

                return retstring;
            }
            catch { return string.Empty; }
        }

        #endregion

        #region SolidBrush

        private Tuple<int, string, Color> SolidBrush_(int indx, string LinetoProcess)
        {
            MatchCollection _Match = RegexTabel.Solidbrush_regex.Matches(LinetoProcess);
            if (_Match.Count > 0)
            {
                try
                {
                    ColorConverter colorconvert = new ColorConverter();
                    Color tmpcol = Color.Transparent;
                    string tmpflo = string.Empty;
                    for (int i = 0; i < _Match.Count; i++)
                    {
                        if (_Match[i].Groups[1].Value != string.Empty)
                            tmpflo = _Match[i].Groups[1].Value.ToString();
                        if (_Match[i].Groups[2].Value != string.Empty)
                            tmpcol =(Color)colorconvert.ConvertFromString(_Match[i].Groups[2].Value.ToString());
                    }
                    
                    return new Tuple<int, string, Color>(indx, tmpflo, tmpcol);
                }
                catch { }
            }
            return null;
        }

        #endregion

        #region PathGradientBrush

        private Tuple<int, string, double[], PointF, PointF> RadialGradientBrush_(int indx, string LinetoProcess)
        {
            MatchCollection _Match = RegexTabel.RadialGradientBrush_regex.Matches(LinetoProcess);

            if (_Match.Count > 0)
            {
                try
                {
                    PointF CenterPoint = new PointF(0, 0);
                    PointF GradientOrgin = new PointF(0, 0);
                    float tmpfloat1, tmpfloat2, tmpfloat3, tmpfloat4;
                    double radx = 0;
                    double rady = 0;
                    double[] rad = null;
                    string _Name = string.Empty;
                    string[] _Point1 = null;
                    string[] _Point2 = null;

                    string txt = string.Empty;
                    for (int i = 0; i < _Match.Count; i++)
                    {
                        if (_Match[i].Groups[1].Value.Trim() != string.Empty)
                            _Name = _Match[i].Groups[1].Value;

                        if (_Match[i].Groups[2].Value.Trim() != string.Empty)
                            rady =Convert.ToDouble(_Match[i].Groups[2].Value);

                        if (_Match[i].Groups[3].Value.Trim() != string.Empty)
                            _Point1 = _Match[i].Groups[3].Value.Split(',');
                        if (_Match[i].Groups[4].Value.Trim() != string.Empty)
                            _Point2 = _Match[i].Groups[4].Value.Split(',');

                        if (_Match[i].Groups[5].Value.Trim() != string.Empty)
                            radx = Convert.ToDouble(_Match[i].Groups[5].Value);
                        rad = new double[] { radx, rady };
                    }

                    if (_Point1 != null)
                    {
                        if (_Point1.Length == 2)
                        {
                            tmpfloat1 = float.Parse(_Point1[0]);
                            tmpfloat2 = float.Parse(_Point1[1]);
                            CenterPoint = new PointF(tmpfloat1, tmpfloat2);
                        }
                    }
                    if (_Point2 != null)
                    {
                        if (_Point2.Length == 2)
                        {
                            tmpfloat3 = float.Parse(_Point2[0]);
                            tmpfloat4 = float.Parse(_Point2[1]);
                            GradientOrgin = new PointF(tmpfloat3, tmpfloat4);
                        }
                    }

                    return new Tuple<int, string, double[], PointF, PointF>(indx, _Name, rad, CenterPoint, GradientOrgin);
                }
                catch { }
            }
            return null;
        }

        #endregion
     
        #endregion

        #endregion
    }
}
