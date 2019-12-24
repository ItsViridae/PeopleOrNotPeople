using PeopleOrNotPeopleDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleOrNotPeopleDemo.ViewModels
{
    public class ResultViewModel : ViewModel
    {
        private string title;
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }

        private byte[] photoBytes;
        public byte[] PhotoBytes
        {
            get => photoBytes;
            set => Set(ref photoBytes, value); 
        }
        public void Initialize(Result result)
        {
            PhotoBytes = result.PhotoBytes;
            if(result.IsPeople && result.Confidence > 0.9)
            {
                Title = "People";
                Description = "This is for sure People";
            }
            else if (result.IsPeople)
            {
                Title = "Maybe";
                Description = "This is maybe People?";
            }
            else
            {
                Title = "NOT People";
                Description = "This is not People";
            }
        }
    }
}
