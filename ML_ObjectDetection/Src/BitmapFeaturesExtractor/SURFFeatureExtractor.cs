using Accord.Imaging;
using ML_Lib.DataType;
using ML_Lib.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ML_Lib.BitmapFeaturesExtractor
{
    public class SURFFeatureExtractor : FeaturesExtractorBase<SURFFeature>
    {
        public float Threshold;
        public int Octaves, Initial;

        public bool? ExtractPositiveOnly = null, ExtractNegativeOnly = null;
        public double? MinimumScale = null, MaximumScale = null;

        public SURFFeatureExtractor(float threshold = 0.0002F, int octaves = 5, int initial = 2)
        {
            Threshold = threshold;
            Octaves = octaves;
            Initial = initial;

        }

        public override IEnumerable<SURFFeature> ExtractFeatures(Bitmap bitmap, string TagSet)
        {
            SpeededUpRobustFeaturesDetector Surf = new SpeededUpRobustFeaturesDetector(Threshold, Octaves, Initial);
            IEnumerable<SpeededUpRobustFeaturePoint> FeaturePoints = Surf.Transform(bitmap);

            //Check laplacian
            if (ExtractPositiveOnly ?? false)
                FeaturePoints = FeaturePoints.Where(x => x.Laplacian < 0);
            if (ExtractNegativeOnly ?? false)
                FeaturePoints = FeaturePoints.Where(x => x.Laplacian > 0);
            
            //Check scale
            if (MinimumScale != null)
                FeaturePoints = FeaturePoints.Where(x => x.Scale > MinimumScale);
            if (MaximumScale != null)
                FeaturePoints = FeaturePoints.Where(x => x.Scale < MaximumScale);

            return FeaturePoints.Select(x => new SURFFeature(x, TagSet));
        }
    }
}
