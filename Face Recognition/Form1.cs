using System;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.ML;
using Face_Recognition;
using Microsoft.ML.Transforms.Image;
using System.Linq;

namespace Simple_Face_Recognition_App
{
    public partial class Form1 : Form
    {
        #region Variables
        private VideoCapture videoCapture = null;
        private Mat currentFrame = null;
        Mat frame = new Mat();
        MLContext mlContext = new MLContext();
        private bool faceDetectionEnabled = false;
        private bool emotionEnabled = false;

        CascadeClassifier faceCascadeClassifier = new CascadeClassifier(@"F:\Internship\repos\Emotion Recognition\Face Recognition\haarcascade_frontalface_default.xml");
        private string modelFilePath = @"F:\Internship\repos\Emotion Recognition\Face Recognition\fer2013_mini_XCEPTION.110-0.65-keras.onnx";

        static string[] SentimentLabel => new string[]
        {
            "angry",
            "disgust",
            "fear",
            "happy",
            "sad",
            "surprise",
            "neutral"
        };
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Capture_Click(object sender, EventArgs e)
        { 
            videoCapture = new VideoCapture(0);
            var model = LoadModel();
            PredictionEngine<ModelInput, ModelOutput> predict = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
                while (true)
                {
                    videoCapture.Read(frame);
                    if (picCapture.Image != null)
                        picCapture.Image.Dispose();

                    currentFrame = frame.Clone();

                    if (faceDetectionEnabled)
                    {
                        Mat grayImage = new Mat();
                        Cv2.CvtColor(currentFrame, grayImage, ColorConversionCodes.BGR2GRAY);
                        //Cv2.EqualizeHist(grayImage, grayImage);

                        Rect[] faces = faceCascadeClassifier.DetectMultiScale(grayImage, 1.1, 4);

                        if (faces.Length > 0)
                        {
                            foreach (var face in faces)
                            {
                                Cv2.Rectangle(currentFrame, face, new Scalar(255, 255, 255), 3);
                                if (emotionEnabled)
                                {
                                    using (var face_crop = new Mat(grayImage, new Rect(face.X, face.Y, face.Width, face.Height)))
                                    {
                                        string predictionsLabel = string.Empty;
                                        string predictedScore = string.Empty;
                                        List<float> PredictionScores;
                                        PredictionScores = predict.Predict(new ModelInput
                                        {
                                            Image = face_crop.ToBitmap()
                                        }).PredictedLabels.ToList();

                                        predictedScore = "Score(" + PredictionScores.Max() + ")";
                                        var secondMax = PredictionScores.OrderByDescending(r => r).Skip(1).FirstOrDefault();
                                        if (PredictionScores.IndexOf(PredictionScores.Max()) == 6 && secondMax > 0.20)
                                        {
                                            predictionsLabel = SentimentLabel[PredictionScores.IndexOf(secondMax)];
                                        }
                                        else
                                        {
                                            predictionsLabel = SentimentLabel[PredictionScores.IndexOf(PredictionScores.Max())];
                                        }


                                        //frame.Rectangle(face, new Scalar(0, 0, 0), 2);

                                        int baseLine = 0;
                                        for (int i = 0; i < SentimentLabel.Length; i++)
                                        {
                                            var predictionText = SentimentLabel[i] + PredictionScores.ToArray()[i];
                                            var predictionTextSize = Cv2.GetTextSize(predictionText, HersheyFonts.HersheyComplex, .6, 5, out baseLine);
                                            //var predictedScoreTextSize = Cv2.GetTextSize(predictedScore, HersheyFonts.HersheyComplex, 1, 5, out baseLine);

                                            var positionOfPredictionLabel = new OpenCvSharp.Point(face.X - predictionTextSize.Width, face.Y + predictionTextSize.Height + i * 25);
                                            //var positionOfPredictionScoreLabel = new Point(face.X - predictedScoreTextSize.Width, face.Y + predictedScoreTextSize.Height + predictionTextSize.Height + 20);

                                            Cv2.PutText(currentFrame, predictionText, positionOfPredictionLabel, HersheyFonts.HersheyComplex, .6, Scalar.Black);

                                        }
                                        Cv2.PutText(currentFrame, predictionsLabel, new OpenCvSharp.Point(25, 25), HersheyFonts.HersheyComplex, 1, Scalar.Blue);
                                    }

                                }
                            }
                        }
                    }

                    picCapture.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(currentFrame);
                    picCapture.Refresh();

                    Thread.Sleep(50);
                    Application.DoEvents();
                }
        }

        private void btnDetector_Click(object sender, EventArgs e)
        {
            faceDetectionEnabled = true;
        }

        private void btn_emotion_Click(object sender, EventArgs e)
        {
            emotionEnabled = true;

        }

        private ITransformer LoadModel()
        {


            var dataView = mlContext.Data.LoadFromEnumerable(new List<ModelInput>());

            var pipeline = mlContext.Transforms
                                .ConvertToGrayscale(
                                inputColumnName: nameof(ModelInput.Image),
                                outputColumnName: nameof(ModelInput.Image))
                            .Append(mlContext.Transforms
                                .ResizeImages(
                                    inputColumnName: nameof(ModelInput.Image),
                                    imageWidth: ModelInput.InputModelConfiguration.ImageWidth,
                                    imageHeight: ModelInput.InputModelConfiguration.ImageHeight,
                                    outputColumnName: nameof(ModelInput.Image)
                                    ))
                            .Append(mlContext.Transforms
                                .ExtractPixels(
                                    inputColumnName: nameof(ModelInput.Image),
                                    outputColumnName: "input_1",
                                    outputAsFloatArray: true,
                                    colorsToExtract: ImagePixelExtractingEstimator.ColorBits.Blue, interleavePixelColors: true, scaleImage: .0039216f
                                    ))
                            .Append(mlContext.Transforms
                                .ApplyOnnxModel(modelFilePath));

            //var model = mlContext.Transforms.ApplyOnnxModel(modelFilePath);

            var model = pipeline.Fit(dataView);
            return model;
        }
        

        
        //private bool TrainImagesFormDir()
        //{
        //    int imagesCount = 0;
        //    int threshold = 1000;

        //    try
        //    {
        //        string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
        //        string[] files = Directory.GetFiles(path,"*.jpg",SearchOption.AllDirectories);

        //        //foreach (var file in files)
        //        //{
        //        //    Image<Gray, Byte> trainedImage = new Image<Gray,Byte>(file);

        //        //    trainedFaces.Add(trainedImage);
        //        //    personLabel.Add(imagesCount);

        //        //    imagesCount++;
        //        //}
        //        //EigenFaceRecognizer faceRecognizer = new EigenFaceRecognizer(imagesCount, threshold);
        //        //faceRecognizer.Train(trainedFaces.ToArray(), personLabel.ToArray());
        //        return isTrained = true;
        //    }
        //    catch
        //    {
        //        return isTrained = false;
        //    }
        //}
    }
}