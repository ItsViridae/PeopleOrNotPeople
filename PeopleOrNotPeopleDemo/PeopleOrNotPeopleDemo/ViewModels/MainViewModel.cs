using PeopleOrNotPeopleDemo.Models;
using PeopleOrNotPeopleDemo.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace PeopleOrNotPeopleDemo.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private IClassifier classifier;
        private byte[] bytes;
        private int opacity;

        public MainViewModel(IClassifier classifier)
        {
            this.classifier = classifier;
        }

        private void HandlePhoto(MediaFile photo)
        {
            if(photo == null)
            {
                return;
            }

            var stream = photo.GetStream();
            bytes = ReadFully(stream);

            classifier.ClassificationCompleted += Classifier_ClassificationCompleted;
            classifier.Classify(bytes);
        }

        void HandleOverlayImage(MediaFile overlayImage)
        {
            if (overlayImage == null)
            {
                return;
            }

            var stream = overlayImage.GetStream();
            bytes = ReadFully(stream);

            var image = new OverlayImage()
            {
                PhotoBytes = bytes
            };

            //manually set opacity FIX ME LATER
            opacity = 75;

            var view = Resolver.Resolve<OverlayImageView>();
            ((OverlayImageViewModel)view.BindingContext).Initialize(image, opacity);

            Navigation.PushAsync(view);
        }

        private byte[] ReadFully(Stream inputStream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                inputStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        void Classifier_ClassificationCompleted(object sender, ClassificationEventArgs e)
        {
            classifier.ClassificationCompleted -= Classifier_ClassificationCompleted;

            Result result = null;

            if (e.Classifications.Any())
            {
                var classificationResult = e.Classifications.OrderByDescending(x => x.Value).First();

                result = new Result()
                {
                    IsPeople = classificationResult.Key == "people",
                    Confidence = classificationResult.Value,
                    PhotoBytes = bytes
                };
            }
            else
            {
                result = new Result()
                {
                    IsPeople = false,
                    Confidence = 1.0f,
                    PhotoBytes = bytes
                };
            }

            var view = Resolver.Resolve<ResultView>();
            ((ResultViewModel)view.BindingContext).Initialize(result);

            Navigation.PushAsync(view);
            Thread.Sleep(10000);
        }

        public ICommand TakePhoto => new Command(async () =>
        {
            var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                DefaultCamera = CameraDevice.Rear
            });
            HandlePhoto(photo);
        });

        public ICommand PickPhoto => new Command(async () =>
        {
            var photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { });

            if (photo == null) return;

            HandlePhoto(photo);
        });

        public ICommand PickOverlayImage => new Command(async () =>
        {
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                //CustomPhotoSize = 100,
                //RotateImage = true,
                //SaveMetaData = true
            });

            if (file == null) return;

            HandleOverlayImage(file);
        });

    }
    
}
