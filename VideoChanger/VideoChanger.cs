using Emgu.CV;
using System.Drawing;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace VideoChanger
{
    public class VideoChanger
    {
        private string _videoFilePath;
        public VideoChanger(string videoFilePath) {
            _videoFilePath = videoFilePath;
        }

        public void AddBrightness(string outputFileName, double randomChangeNumber)
        {
            using (VideoCapture videoCapture = new VideoCapture(_videoFilePath))
            {
                if (!videoCapture.IsOpened)
                {
                    Console.WriteLine("Error opening video file.");
                    return;
                }

                // Get video properties
                int frameWidth = (int)videoCapture.Get(CapProp.FrameWidth);
                int frameHeight = (int)videoCapture.Get(CapProp.FrameHeight);
                double fps = videoCapture.Get(CapProp.Fps);

                // Create an output window
                CvInvoke.NamedWindow("Processed Video", WindowFlags.Normal);

                VideoWriter videoWriter = new VideoWriter(outputFileName, VideoWriter.Fourcc('X', '2', '6', '4'), fps, new Size(frameWidth, frameHeight), true);

                while (true)
                {
                    Mat frame = new Mat();
                    if (!videoCapture.Read(frame))
                        break;                    

                    // Adjust brightness of the frame
                    frame = randomChangeNumber * frame;

                    // Display the frame
                    CvInvoke.Imshow("Processed Video", frame);

                    // Write the frame to the output video file
                    videoWriter.Write(frame);

                    // Exit loop if 'Esc' key is pressed
                    if (CvInvoke.WaitKey((int)(1000 / fps)) == 27)
                        break;

                    frame.Dispose();
                }

                videoWriter.Dispose();
            }

            CvInvoke.DestroyAllWindows();

        }
    }
}

