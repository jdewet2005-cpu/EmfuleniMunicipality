using System;

namespace EmfuleniMunicipality
{
    public class ServiceRequest
    {
        public string RequestType { get; set; }
        public int PriorityLevel { get; set; }   // 1 to 5
        public int SeverityLevel { get; set; }   // 1 to 10
        public int EstimatedResolutionHours { get; set; }

        // Link to the resident who logged the fault
        public Resident AssignedResident { get; set; }

        // Properties to hold calculated results
        public int UrgencyScore { get; set; }
        public int AdjustedResolution { get; set; }
        public double HouseholdImpactScore { get; set; }
    }
}