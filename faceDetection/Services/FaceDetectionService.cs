using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace faceDetection.Services
{
    public class FaceDetectionService
    {
        private readonly CascadeClassifier _faceCascade;

        public FaceDetectionService(string haarCascadePath)
        {
            if (!File.Exists(haarCascadePath))
            {
                throw new FileNotFoundException("Haar cascade file not found", haarCascadePath);
            }

            _faceCascade = new CascadeClassifier(haarCascadePath);
        }

        public byte[] DetectFaces(byte[] imageData)
        {
            using (var mat = Mat.FromImageData(imageData))
            {
                var faces = _faceCascade.DetectMultiScale(mat);

                foreach (var face in faces)
                {
                    mat.Rectangle(face, new Scalar(0, 255, 0), 2);
                }

                return mat.ToBytes(".png");
            }
        }
        public List<FaceRectangle> RealDetectFaces(byte[] imageData)
        {
            // Your face detection logic here
            // This is a placeholder
            return new List<FaceRectangle>
    {
        new FaceRectangle { x = 100, y = 100, width = 50, height = 50 },
        new FaceRectangle { x = 200, y = 150, width = 60, height = 60 }
    };
        }

        public class FaceRectangle
        {
            public int x { get; set; }
            public int y { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }
    }
}
