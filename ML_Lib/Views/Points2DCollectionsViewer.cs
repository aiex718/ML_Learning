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

        static Dictionary<string, int> TagToValueDict = new Dictionary<string, int>();

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

            foreach (var point in PointsWithTag)
            {
                scatterSeries.Points.Add(new ScatterPoint(point.x, point.y, 2, TagToValue(point.Tag)));
            }

            model.Series.Add(scatterSeries);
            model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Rainbow(TagToValueDict.Count() + 1) });

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
            MarkerType t = (MarkerType)type;
            var scatterSeries = new ScatterSeries { MarkerType = t };
            scatterSeries.Points.Add(new ScatterPoint(point.x, point.y, size, TagToValue(point.Tag)));
            plotView.Model.Series.Add(scatterSeries);
        }

        private void Points2DCollectionsViewer_Shown(object sender, EventArgs e)
        {
            RenderFinish = true;
        }

        private static int TagToValue(string Tag)
        {
            int TagValue;
            if (TagToValueDict.Keys.Contains(Tag) == false)
            {
                TagValue = TagToValueDict.Count() + 1;
                TagToValueDict.Add(Tag, TagValue);
            }
            else
            {
                TagValue = TagToValueDict[Tag];
            }

            return TagValue;
        }
    }
}
