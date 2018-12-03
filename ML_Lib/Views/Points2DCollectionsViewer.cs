using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ML_Lib.DataType;
using System.Threading.Tasks;
using System.Threading;

namespace ML_Lib.Views
{
    public partial class Points2DCollectionsViewer : Form
    {
        static List<Points2DCollectionsViewer> OpenedView = new List<Points2DCollectionsViewer>();

        public bool RenderFinish = false;
        public Points2DCollectionsViewer()
        {
            InitializeComponent();
        }

        public static Points2DCollectionsViewer BuildViewer(IEnumerable<Point2D> PointsWithTag, string Title, int min, int max)
        {
            Points2DCollectionsViewer viewer = new Points2DCollectionsViewer();
            viewer.Text = Title;

            var model = new PlotModel { Title = viewer.Text };
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };


            int MaxTagValue = 2;
            foreach (var point in PointsWithTag)
            {
                int tag = Convert.ToInt32(point.ClassifiedTag);
                scatterSeries.Points.Add(new ScatterPoint(point.x, point.y, 2, tag));

                if (tag > MaxTagValue)
                    MaxTagValue = tag;
            }

            model.Series.Add(scatterSeries);
            model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Rainbow(++MaxTagValue) });

            model.Axes.Add(new LinearAxis() { Minimum = min, Maximum = max, Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis() { Minimum = min, Maximum = max, Position = AxisPosition.Left });


            viewer.plotView.Model = model;

            return viewer;
        }
        
        public static void CloseAllButLastView()
        {
            if (OpenedView.Any())
                OpenedView.Remove(OpenedView.Last());
            foreach (var view in OpenedView)
            {
                view.Invoke((MethodInvoker)delegate
                {
                    view.Close();
                });
            }
            OpenedView.Clear();
        }


        public static void ShowViewer(Points2DCollectionsViewer view)
        {
            OpenedView.Add(view);
            Task t = Task.Factory.StartNew(() => view.ShowDialog());
            while (view.RenderFinish == false)
                Thread.Sleep(100);
        }

        /// <summary>
        /// type: 1=Star ,2=Cross ,3=Circle
        /// </summary>
        public void AddMarkAt(Point2D point,int size, int type)
        {
            MarkerType t = MarkerType.None;
            if (type == 1)
                t = MarkerType.Star;
            else if (type == 2)
                t = MarkerType.Cross;
            else if (type == 3)
                t = MarkerType.Circle;

            var scatterSeries = new ScatterSeries { MarkerType = t };
            scatterSeries.Points.Add(new ScatterPoint(point.x, point.y, size, Convert.ToDouble(point.ClassifiedTag)));
            plotView.Model.Series.Add(scatterSeries);
            plotView.Refresh();
        }



        private void Points2DCollectionsViewer_Shown(object sender, EventArgs e)
        {
            RenderFinish = true;
        }




    }
}
