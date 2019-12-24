using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreML;
using Foundation;
using UIKit;
using Vision;

namespace PeopleOrNotPeopleDemo.iOS
{
    public class CoreMLClassifier : IClassifier
    {
        public event EventHandler<ClassificationEventArgs> ClassificationCompleted;

        public void Classify(byte[] bytes)
        {
            var modelUrl = NSBundle.MainBundle.GetUrlForResource("people-or-not", "mlmodel");
            var compiledUrl = MLModel.CompileModel(modelUrl, out var error);
            var compiledModel = MLModel.Create(compiledUrl, out error);

            var vnCoreModel = VNCoreMLModel.FromMLModel(compiledModel, out error);

            var classificationRequest = new VNCoreMLRequest(vnCoreModel, HandleVNRequest);

            var data = NSData.FromArray(bytes);
            var handler = new VNImageRequestHandler(data, ImageIO.CGImagePropertyOrientation.Up, new VNImageOptions());
            handler.Perform(new[] { classificationRequest }, out error);
        }

        //Callback function
        private void HandleVNRequest(VNRequest request, NSError error)
        {
            if (error != null)
            {
                ClassificationCompleted?.Invoke(this, new ClassificationEventArgs(new Dictionary<string, float>()));
            }

            var result = request.GetResults<VNClassificationObservation>();

            var classifications = result.OrderByDescending(x => x.Confidence)
                                        .ToDictionary(x => x.Identifier,x => x.Confidence);

            ClassificationCompleted?.Invoke(this, new ClassificationEventArgs(classifications));
        }
    }
}