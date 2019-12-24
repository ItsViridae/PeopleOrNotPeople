using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleOrNotPeopleDemo
{
    public class ClassificationEventArgs : EventArgs
    {
        public Dictionary<string, float> Classifications { get; private set; }
        public ClassificationEventArgs(Dictionary<string, float> classifications)
        {
            Classifications = classifications;
        }
    }
}
