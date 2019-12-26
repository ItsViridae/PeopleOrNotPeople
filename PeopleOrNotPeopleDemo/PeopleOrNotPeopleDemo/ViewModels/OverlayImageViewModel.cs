using PeopleOrNotPeopleDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PeopleOrNotPeopleDemo.ViewModels
{
    public class OverlayImageViewModel : ViewModel
    {
        public Image OverlayImage { get; set; }
        public int Opacity { get; set; }

        private byte[] photoBytes;
        public byte[] PhotoBytes
        {
            get => photoBytes;
            set => Set(ref photoBytes, value);
        }

        public void Initialize(OverlayImage image, int opacityValue)
        {
            PhotoBytes = image.PhotoBytes;
            Opacity = opacityValue;
        }
        
    }
    
}
