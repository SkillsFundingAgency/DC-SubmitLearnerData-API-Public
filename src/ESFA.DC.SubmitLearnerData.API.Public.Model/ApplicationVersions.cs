using System;
using System.Collections.Generic;

namespace ESFA.DC.SubmitLearnerData.API.Public.Model
{
    public class ApplicationVersions
    {
        public string Url { get; set; }

        public DateTime? LastUpdated { get; set; }
                
        public IReadOnlyCollection<Version> Versions { get; set; }
    }
}
