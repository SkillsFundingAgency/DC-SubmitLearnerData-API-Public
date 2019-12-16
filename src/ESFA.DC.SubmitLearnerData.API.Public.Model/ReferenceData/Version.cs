using System;

namespace ESFA.DC.SubmitLearnerData.API.Public.Model.ReferenceData
{
    public class Version
    {
        public string FileName { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }

        public int Increment { get; set; }

        public DateTime? ReleaseDateTime { get; set; }
    }
}