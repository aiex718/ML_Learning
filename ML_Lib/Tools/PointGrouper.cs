using ML_Lib.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Lib.Tools
{
    public class Point2DGrouper
    {
        public double MaximumDistance;
        public double? MinimumGroupCount;
        public Point2DGrouper(double maximumDistance)
        {
            MaximumDistance = maximumDistance;
        }

        public IEnumerable<GroupedPoint2D> GetGrouped2DPoints(IEnumerable<Point2D> points)
        {
            List<GroupedPoint2D> GrouppedPoints = new List<GroupedPoint2D>();
            List<GroupedPoint2D> RawPoints = points.Select(p => new GroupedPoint2D(p, null)).ToList();
            int GroupIDEnum = 0;

            foreach (GroupedPoint2D p in RawPoints)
            {
                var NearPoints = RawPoints.Where(x =>x.GetEuclideanDistance(p) <= MaximumDistance);

                int? GroupIDSet = null;
                //Search GroupID
                foreach (var SearchPoint in NearPoints)
                {
                    if (SearchPoint.GroupID != null)
                    {
                        GroupIDSet = SearchPoint.GroupID;
                        break;
                    }
                }

                GroupIDSet = GroupIDSet ?? GroupIDEnum++;

                //Assign Group
                foreach (var NearPoint in NearPoints)
                    NearPoint.GroupID = GroupIDSet;

                GrouppedPoints.AddRange(NearPoints);
            }

            List<GroupedPoint2D> Result = new List<GroupedPoint2D>();
            if (MinimumGroupCount != null )
            {
                var groups = GrouppedPoints.GroupBy(x => x.GroupID);

                foreach (var group in groups)
                {
                    if (group.Count() >= MinimumGroupCount)
                    {
                        Result.AddRange(group);
                    }
                }
            }
            else
                Result = GrouppedPoints;

            return Result;
        }


    }
}
