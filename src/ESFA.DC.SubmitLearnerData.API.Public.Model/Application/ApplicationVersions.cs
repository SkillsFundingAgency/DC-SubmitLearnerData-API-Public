using System;
using System.Collections.Generic;

namespace ESFA.DC.SubmitLearnerData.API.Public.Model.Application
{
    public class ApplicationVersions
    {
        public string Url { get; set; }

        public DateTime? LastUpdated { get; set; }
                
        public IReadOnlyCollection<Version> Versions { get; set; }
    }
}
