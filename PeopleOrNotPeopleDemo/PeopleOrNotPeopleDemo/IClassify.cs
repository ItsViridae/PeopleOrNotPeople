using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleOrNotPeopleDemo
{
    public interface IClassifier
    {
        void Classify(byte[] bytes);
        event EventHandler<ClassificationEventArgs> ClassificationCompleted;
    }
}
