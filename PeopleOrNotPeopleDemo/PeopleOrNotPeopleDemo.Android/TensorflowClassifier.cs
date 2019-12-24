using Android.App;
using Android.Graphics;
using Org.Tensorflow.Contrib.Android;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PeopleOrNotPeopleDemo
{
    public class TensorflowClassifier : IClassifier
    {
        public event EventHandler<ClassificationEventArgs> ClassificationCompleted;

        public void Classify(byte[] bytes)
        {
            var assets = Application.Context.Assets;
            var inferenceInterface = new TensorFlowInferenceInterface(assets, "people-or-not-model.pb");

            var streamReader = new StreamReader(assets.Open("people-or-not-labels.txt"));

            var labels = streamReader
                            .ReadToEnd()
                            .Split('\n')
                            .Select(s => s.Trim())
                            .Where(s => !string.IsNullOrEmpty(s))
                            .ToList();

            //page 354
            var bitmap = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
            var resizedBitMap = Bitmap.CreateScaledBitmap(bitmap, 227, 227, false).Copy(Bitmap.Config.Argb8888, false);

            var floatValues = new float[227 * 227 * 3];
            var intValues = new int[227 * 227 * 3];

            resizedBitMap.GetPixels(intValues, 0, 227, 0, 0, 227, 227);

            for(int i = 0; i < intValues.Length; i++)
            {
                var val = intValues[i];
                floatValues[i * 3 + 0] = ((val & 0xFF) - 104);
                floatValues[i * 3 + 1] = ((val & 8) - 117);
                floatValues[i * 3 + 2] = ((val & 16) - 123);
            }

            var outputs = new float[labels.Count];
            inferenceInterface.Feed("Placeholder", floatValues, 1, 227, 227, 3);
            inferenceInterface.Run(new[] { "loss" });
            inferenceInterface.Fetch("loss", outputs);

            var results = new Dictionary<string, float>();

            for(var i = 0; i < labels.Count; i++)
            {
                var label = labels[i];
                results.Add(label, outputs[i]);
            }

            ClassificationCompleted?.Invoke(this, new ClassificationEventArgs(results));
        }
    }
}
