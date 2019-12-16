using System;
using System.Collections.Generic;

namespace ESFA.DC.SubmitLearnerData.API.Public.Model.ReferenceData
{
    public class ReferenceDataVersions
    {
        public DateTime? LastUpdated { get; set; }

        public IReadOnlyCollection<Version> Versions { get; set; }
    }
}