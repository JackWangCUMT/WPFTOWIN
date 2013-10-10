using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ManiX;

namespace ManiXWPFtoWIN
{
    public partial class Preview : Form
    {
        public Preview()
        {
            InitializeComponent();
        }

        #region Properties

        private List<Tuple<SolidBrush, string>> _SolidBrush;
        public List<Tuple<SolidBrush, string>> SolidBrushColl
        {
            get { return _SolidBrush; }
            set { _SolidBrush = value; }
        }

        private List<Tuple<PathGradientBrush, string>> _PathBrush;
        public List<Tuple<PathGradientBrush, string>> PathBrushColl
        {
            get { return _PathBrush; }
            set { _PathBrush = value; }
        }

        private List<Tuple<LinearGradientBrush, string>> _LinearBrush;
        public List<Tuple<LinearGradientBrush, string>> LinearBrushColl
        {
            get { return _LinearBrush; }
            set { _LinearBrush = value; }
        }

        #endregion

        private void Preview_SizeChanged(object sender, EventArgs e)
        {
            Gradientpanels.Height = this.Height - 40;
            Gradientpanels.Width = this.Width - 20;

            this.Invalidate();
        }

        private void DrawBrushes()
        {
            Point _labelpoint = new Point(0, 0);


            if (_SolidBrush != null)
            {
                for (int i = 0; i < _SolidBrush.Count; i++)
                {
                    Label Label_ = new Label();
                    Label_.Name = "Solid_" + i.ToString();
                    Label_.Text = "SolidBrush";
                    Label_.Tag = (int)i;
                    Label_.Font = this.Font;
                    Label_.Size = new Size(300, 100);
                    Label_.Location = _labelpoint;
                    Label_.Paint += new PaintEventHandler(Label_Paint);
                    Gradientpanels.Controls.Add(Label_);
                    _labelpoint.Y += 100;

                }
            }

            if (_LinearBrush != null && _LinearBrush.Count > 0)
            {
                for (int i = 0; i < _PathBrush.Count; i++)
                {
                    Label Label_ = new Label();
                    Label_.Name = "Line_" + i.ToString();
                    Label_.Text = "LinearGradientBrush";
                    Label_.Tag = (int)i;
                    Label_.Size = new Size(300, 200);
                    Label_.Location = _labelpoint;
                    Label_.Paint += new PaintEventHandler(Label_Paint);
                    Gradientpanels.Controls.Add(Label_);
                    _labelpoint.Y += 200;

                }
            }



            if (_PathBrush != null)
            {
                for (int i = 0; i < _PathBrush.Count; i++)
                {
                    Label Label_ = new Label();
                    Label_.Name = "Path_" + i.ToString();
                    Label_.Text = "PathGradientBrush";
                    Label_.Tag = (int)i;
                    Label_.Font = this.Font;
                    Label_.Size = new Size(300, 300);
                    Label_.Location = _labelpoint;
                    Label_.Paint += new PaintEventHandler(Label_Paint);
                    Gradientpanels.Controls.Add(Label_);
                    _labelpoint.Y += 300;

                }
            }

           
        }

        private void Label_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Brush p = new SolidBrush(Color.Brown);
                Label Label_ = (Label)sender;

                if (Label_.Name.Contains("Line_"))
                {

                    LinearGradientBrush pbr = new LinearGradientBrush(Label_.ClientRectangle, _LinearBrush[(int)Label_.Tag].Item1.LinearColors[0], _LinearBrush[(int)Label_.Tag].Item1.LinearColors[1], LinearGradientMode.Vertical);
                    pbr.InterpolationColors = _LinearBrush[(int)Label_.Tag].Item1.InterpolationColors;

                    using (LinearGradientBrush prus = pbr)
                    {
                        e.Graphics.FillRectangle(prus, Label_.ClientRectangle);
                        e.Graphics.DrawString(_LinearBrush[(int)Label_.Tag].Item2, this.Font, p, new PointF(0, 30));
                    }
                }
                else if (Label_.Name.Contains("Path_"))
                {
                    GraphicsPath path = new GraphicsPath();
                    path.AddEllipse(Label_.ClientRectangle);
                    using (PathGradientBrush pdr = new PathGradientBrush(path))
                    {
                        pdr.InterpolationColors = _PathBrush[(int)Label_.Tag].Item1.InterpolationColors;
                        pdr.CenterPoint = _PathBrush[(int)Label_.Tag].Item1.CenterPoint;
                        pdr.FocusScales = _PathBrush[(int)Label_.Tag].Item1.FocusScales;

                        e.Graphics.FillRectangle(pdr, Label_.ClientRectangle);
                        e.Graphics.DrawString(_PathBrush[(int)Label_.Tag].Item2, this.Font, p, new PointF(0, 30));
                    }
                }
                else
                {
                    SolidBrush li = new SolidBrush( _SolidBrush[(int)Label_.Tag].Item1.Color);

                    using (SolidBrush bru = li)
                    {
                        e.Graphics.FillRectangle(bru, Label_.ClientRectangle);
                        e.Graphics.DrawString(_SolidBrush[(int)Label_.Tag].Item2, this.Font, p, new PointF(0, 30));
                    }
                }
            }
            catch { }
        }

        private void Preview_Load(object sender, EventArgs e)
        {
            Gradientpanels.Height = this.Height - 40;
            Gradientpanels.Width = this.Width - 20;
            DrawBrushes();
        }
    }
}
