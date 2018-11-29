using ML_Lib.DataType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ML_Lib.Tools
{
}

class MnistDataSetLoader
{
    public static RawImage28x28[] LoadData(string pixelFile, string labelFile)
    {
        int numImages = 60000;
        RawImage28x28[] result = new RawImage28x28[numImages];

        byte[,] pixels = new byte[28, 28];

        FileStream ifsPixels = new FileStream(pixelFile, FileMode.Open);
        FileStream ifsLabels = new FileStream(labelFile, FileMode.Open);

        BinaryReader brImages = new BinaryReader(ifsPixels);
        BinaryReader brLabels = new BinaryReader(ifsLabels);

        int magic1 = brImages.ReadInt32(); // stored as Big Endian
        magic1 = ReverseBytes(magic1); // convert to Intel format

        int imageCount = brImages.ReadInt32();
        imageCount = ReverseBytes(imageCount);

        int numRows = brImages.ReadInt32();
        numRows = ReverseBytes(numRows);
        int numCols = brImages.ReadInt32();
        numCols = ReverseBytes(numCols);

        int magic2 = brLabels.ReadInt32();
        magic2 = ReverseBytes(magic2);

        int numLabels = brLabels.ReadInt32();
        numLabels = ReverseBytes(numLabels);

        // each image
        for (int di = 0; di < numImages; ++di)
        {
            for (int i = 0; i < 28; ++i) // get 28x28 pixel values
            {
                for (int j = 0; j < 28; ++j)
                {
                    byte b = brImages.ReadByte();
                    pixels[i, j] = b;
                }
            }

            byte lbl = brLabels.ReadByte(); // get the label
            RawImage28x28 dImage = new RawImage28x28(pixels, 28, 28, lbl);
            result[di] = dImage;
        } // each image

        ifsPixels.Close(); brImages.Close();
        ifsLabels.Close(); brLabels.Close();

        return result;
    } // LoadData


    public static int ReverseBytes(int v)
    {
        byte[] intAsBytes = BitConverter.GetBytes(v);
        Array.Reverse(intAsBytes);
        return BitConverter.ToInt32(intAsBytes, 0);
    }



}
