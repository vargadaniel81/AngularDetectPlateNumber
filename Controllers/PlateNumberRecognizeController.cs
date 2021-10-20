using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RecognizeText;


using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Tesseract;
using AForge;
using AForge.Imaging;
using AForge.Math.Geometry;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Diagnostics;

namespace AspDotNetAngular.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class PlateNumberRecognizeController : ControllerBase
    {
        Recognize rec = new Recognize();

        [HttpGet]
        public string GetText()
        {

            //  Recognize recognize = new Recognize();

            string text = "hello";
            return text;
        }
    }

    //public class RecognizeTextClass
    //{
    //    public static Bitmap CurrentEditedImage { get; set; }
    //    public static IList<System.Drawing.Point> Coordinates = new List<System.Drawing.Point>();
    //    public static string PlateNumber { get; set; }
    //    public static FilterInfoCollection filterInfoCollection;
    //    public static VideoCaptureDevice videoCaptureDevice;
    //    public static int CheckedFrames;
    //    public static bool IsDetectRectangles = false;
    //    public static bool IsCroped = false;
    //    public static bool IsRecognized = false;
    //    public static bool IsVideoOnline = true;

    //    private static void RecognizeAlgorithm()
    //    {
    //        GetPicture("jeep");
    //        //GrayScale(CurrentEditedImage);
    //        ColorKiller(CurrentEditedImage);
    //        AdjustContrast(CurrentEditedImage, 1000);
    //        DetectRectangles(CurrentEditedImage);
    //        CropPlateNumber(CurrentEditedImage);
    //        SkewPlateNumber(CurrentEditedImage);
    //        RecognizeText(CurrentEditedImage);
    //        CheckPlateNumber();
    //    }

    //    //private static void StartWebCamera()
    //    //{
    //    //    filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
    //    //    videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);
    //    //    var maxResolution = videoCaptureDevice.VideoCapabilities.Length - 1;
    //    //    videoCaptureDevice.VideoResolution = videoCaptureDevice.VideoCapabilities[maxResolution];
    //    //    videoCaptureDevice.Start();
    //    //    videoCaptureDevice.NewFrame += CreateCurrentEditedImage;
    //    //}

    //    //private static void CreatePictureAndSave(object sender, NewFrameEventArgs e)
    //    //{
    //    //    System.Threading.Thread.Sleep(2000);
    //    //    CheckedFrames += 1;
    //    //    var picture = (Bitmap)e.Frame.Clone();
    //    //    picture.Save("./algorithm/" + "cam" + CheckedFrames + ".jpg");
    //    //    Console.WriteLine(CheckedFrames);
    //    //}

    //    //private static void CreateCurrentEditedImage(object sender, NewFrameEventArgs e)
    //    //{
    //    //    System.Threading.Thread.Sleep(500);
    //    //    CheckedFrames += 1;

    //    //    if (CheckedFrames == 20)
    //    //    {
    //    //        Console.WriteLine(CheckedFrames);
    //    //        IsVideoOnline = false;
    //    //        Environment.Exit(0);
    //    //        videoCaptureDevice.Stop();
    //    //    }
    //    //    else
    //    //    {
    //    //        CurrentEditedImage = (Bitmap)e.Frame.Clone();
    //    //        CurrentEditedImage.SetResolution(96.0F, 96.0F);
    //    //        Console.WriteLine(CheckedFrames);
    //    //        RecognizeAlgorithm();
    //    //    }
    //    //}

    //    private static void GetPicture(string path)
    //    {
    //        CurrentEditedImage = (Bitmap)Bitmap.FromFile("./cars/" + path + ".jpg");
    //        CurrentEditedImage.SetResolution(96.0F, 96.0F);
    //    }

    //    private static void ColorKiller(Bitmap image)
    //    {
    //        Bitmap picture = image;
    //        //image.Dispose();

    //        int width = picture.Width;
    //        int height = picture.Height;
    //        Color p;

    //        for (int y = 0; y < height; y++)
    //        {
    //            for (int x = 0; x < width; x++)
    //            {
    //                p = picture.GetPixel(x, y);

    //                int a = p.A;
    //                int r = p.R;
    //                int g = p.G;
    //                int b = p.B;

    //                int avg = (r + g + b) / 3;

    //                picture.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
    //            }
    //        }

    //        picture.Save("./algorithm/" + "1_colorkiller.jpg");
    //        CurrentEditedImage = picture;
    //        //picture.Dispose();
    //    }

    //    private static void GrayScale(Bitmap image)
    //    {
    //        // create grayscale filter (BT709)
    //        Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
    //        // apply the filter
    //        Bitmap grayImage = filter.Apply(image);
    //        //image.Dispose();
    //        grayImage.Save("./algorithm/" + "1_colorkiller.jpg");
    //        CurrentEditedImage = grayImage;
    //        //grayImage.Dispose();
    //    }

    //    private static void AdjustContrast(Bitmap image, int correction)
    //    {
    //        Bitmap picture = image;
    //        //image.Dispose();
    //        ContrastCorrection filter = new ContrastCorrection(correction);
    //        filter.ApplyInPlace(picture);
    //        picture.Save("./algorithm/" + "2_adjustcontrast.jpg");
    //        CurrentEditedImage = picture;
    //        //picture.Dispose();
    //    }

    //    private static void DetectRectangles(Bitmap image)
    //    {
    //        Bitmap picture = image;
    //        //image.Dispose();

    //        // locating objects
    //        BlobCounter blobCounter = new BlobCounter();

    //        blobCounter.FilterBlobs = true;
    //        blobCounter.MinHeight = 10;
    //        blobCounter.MinWidth = 10;

    //        blobCounter.ProcessImage(picture);
    //        Blob[] blobs = blobCounter.GetObjectsInformation();

    //        Console.WriteLine("Észlelt objektum: " + blobs.Length);
    //        //Console.ReadKey();

    //        // check for rectangles
    //        SimpleShapeChecker shapeChecker = new SimpleShapeChecker();

    //        foreach (var blob in blobs)
    //        {
    //            List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blob);
    //            List<IntPoint> cornerPoints;

    //            // use the shape checker to extract the corner points
    //            if (shapeChecker.IsQuadrilateral(edgePoints, out cornerPoints))
    //            {
    //                // only do things if the corners form a rectangle
    //                if (shapeChecker.CheckPolygonSubType(cornerPoints) == PolygonSubType.Rectangle)
    //                {
    //                    // here i use the graphics class to draw an overlay, but you
    //                    // could also just use the cornerPoints list to calculate your
    //                    // x, y, width, height values.
    //                    List<System.Drawing.Point> Points = new List<System.Drawing.Point>();
    //                    foreach (var point in cornerPoints)
    //                    {
    //                        Points.Add(new System.Drawing.Point(point.X, point.Y));
    //                        Coordinates.Add(new System.Drawing.Point(point.X, point.Y));
    //                        //Console.WriteLine("X point: " + point.X + ", Y point: " + point.Y);
    //                        //Console.ReadKey();
    //                    }

    //                    Graphics g = Graphics.FromImage(picture);
    //                    g.DrawPolygon(new Pen(Color.Red, 3.0f), Points.ToArray());

    //                    picture.Save("./algorithm/" + "3_detectrectangles.jpg");
    //                    CurrentEditedImage = picture;
    //                    IsDetectRectangles = true;
    //                    Console.WriteLine("négyzet kifelölve");
    //                }
    //            }
    //        }
    //    }
    //    private static bool isNotFullCrop()
    //    {
    //        Bitmap picture = CurrentEditedImage;
    //        bool isCrop = true;

    //        if (Coordinates.Count > 0)
    //        {
    //            foreach (var point in Coordinates)
    //            {
    //                if (point.X == 0 || (point.Y == picture.Width - 1 && point.X == picture.Height - 1) || point.Y == 0)
    //                {
    //                    isCrop = false;
    //                }
    //            }
    //        }
    //        if (!isCrop) { Console.WriteLine("teljes kijelőlés, algoritmus leáll"); }

    //        return isCrop;
    //    }

    //    private static void CropPlateNumber(Bitmap image)
    //    {
    //        if (IsDetectRectangles && isNotFullCrop() && Coordinates.Count < 5)
    //        {
    //            Bitmap picture = image;
    //            // image.Dispose();
    //            int[] minXY = { int.MaxValue, int.MaxValue };
    //            int[] maxXY = { int.MinValue, int.MinValue };

    //            foreach (var point in Coordinates)
    //            {
    //                maxXY[0] = Math.Max(maxXY[0], point.X);
    //                maxXY[1] = Math.Max(maxXY[1], point.Y);
    //                minXY[0] = Math.Min(minXY[0], point.X);
    //                minXY[1] = Math.Min(minXY[1], point.Y);
    //            }

    //            int[] size = { maxXY[0] - minXY[0], maxXY[1] - minXY[1] };

    //            Rectangle section = new Rectangle(new System.Drawing.Point(minXY[0], minXY[1]), new Size(size[0], size[1]));
    //            //Console.WriteLine("minXY0: " + minXY[0] + ", minXY1: " + minXY[1] + "size:" + size[0] + ", " + size[1]);
    //            //Console.ReadKey();
    //            Bitmap cropImage = CropImage(picture, section);
    //            cropImage.Save("./algorithm/" + "4_cropimage.jpg");
    //            CurrentEditedImage = cropImage;
    //            IsCroped = true;
    //            //cropImage.Dispose();
    //        }
    //        else
    //        {
    //            Console.WriteLine("1-nél több kijelölés, algoritmus leáll");
    //            Coordinates.Clear();
    //        }
    //    }

    //    private static Bitmap CropImage(Bitmap source, Rectangle section)
    //    {
    //        var picture = new Bitmap(section.Width, section.Height);
    //        using (var g = Graphics.FromImage(picture))
    //        {
    //            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
    //            return picture;
    //        }
    //        Console.WriteLine("rendszám kivágva");
    //    }

    //    private static void SkewPlateNumber(Bitmap image)
    //    {
    //        if (IsCroped)
    //        {
    //            Bitmap tempImage = image;
    //            // image.Dispose();
    //            Bitmap picture;
    //            if (tempImage.PixelFormat.ToString().Equals("Format8bppIndexed"))
    //            {
    //                picture = tempImage;
    //            }
    //            else
    //            {
    //                picture = AForge.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(tempImage);
    //            }

    //            //tempImage.Dispose();

    //            AForge.Imaging.DocumentSkewChecker skewChecker = new AForge.Imaging.DocumentSkewChecker();
    //            double angle = skewChecker.GetSkewAngle(picture);
    //            AForge.Imaging.Filters.RotateBilinear rotationFilter = new AForge.Imaging.Filters.RotateBilinear(-angle);
    //            rotationFilter.FillColor = Color.White;
    //            Bitmap rotatedImage = rotationFilter.Apply(picture);
    //            // picture.Dispose();

    //            var deskewedImagePath = "./algorithm/5_skewplatenumber.jpg";
    //            rotatedImage.Save(deskewedImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);

    //            CurrentEditedImage = rotatedImage;
    //            //rotatedImage.Dispose();
    //        }
    //    }

    //    private static void RecognizeText(Bitmap image)
    //    {
    //        if (IsCroped)
    //        {
    //            Bitmap picture = (Bitmap)image;
    //            // image.Dispose();
    //            picture.SetResolution(96.0F, 96.0F);

    //            TesseractEngine engine = new TesseractEngine("./", "eng", EngineMode.Default);
    //            Page page = engine.Process(picture, PageSegMode.Auto);
    //            //picture.Dispose();

    //            if (page.GetText().Length > 4)
    //            {
    //                string result = page.GetText();
    //                PlateNumber = result;
    //                IsRecognized = true;

    //                Console.WriteLine("A felismert rendszám: " + result);
    //                System.Threading.Thread.Sleep(2000);
    //            }
    //        }
    //    }

    //    private static void CheckPlateNumber()
    //    {
    //        if (IsRecognized)
    //        {
    //            var result = (PlateNumber.Contains("RAP") && PlateNumber.Contains("235")) ? "Megjött apuci!" : "Illetéktelen behatoló!";
    //            Console.WriteLine(result);
    //            System.Threading.Thread.Sleep(5000);
    //        }
    //    }
    //}
}


