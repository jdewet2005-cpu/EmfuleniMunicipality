using System;
using System.Collections.Generic;
using System.Linq;

namespace EmfuleniMunicipality
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to Emfuleni Municipality Service Desk ===");

            // 1. Register Residents
            Console.Write("How many residents do you want to register? ");
            int numResidents = int.Parse(Console.ReadLine());

            List<Resident> residentsList = new List<Resident>();

            for (int i = 1; i <= numResidents; i++)
            {
                Console.WriteLine($"\n--- Resident {i} ---");
                Resident newResident = new Resident();

                Console.Write("Name: ");
                newResident.Name = Console.ReadLine();

                Console.Write("Address: ");
                newResident.Address = Console.ReadLine();

                Console.Write("Account Number: ");
                newResident.AccountNumber = Console.ReadLine();

                Console.Write("Monthly Utility Usage (kWh or litres): ");
                newResident.MonthlyUtilityUsage = double.Parse(Console.ReadLine());

                residentsList.Add(newResident);
            }

            // 2. Log Service Requests
            Console.Write("\nHow many service requests do you want to log? ");
            int numRequests = int.Parse(Console.ReadLine());

            List<ServiceRequest> requestQueue = new List<ServiceRequest>();
            UtilitiesManager manager = new UtilitiesManager();

            for (int i = 1; i <= numRequests; i++)
            {
                Console.WriteLine($"\n--- Service Request {i} ---");

                Console.Write($"Select resident by number (1 to {residentsList.Count}): ");
                int resIndex = int.Parse(Console.ReadLine()) - 1;

                ServiceRequest req = new ServiceRequest();
                req.AssignedResident = residentsList[resIndex];

                Console.Write("Request Type (e.g., Water Outage, Burst Pipe): ");
                req.RequestType = Console.ReadLine();

                Console.Write("Priority Level (1-5): ");
                req.PriorityLevel = int.Parse(Console.ReadLine());

                Console.Write("Severity Level (1-10): ");
                req.SeverityLevel = int.Parse(Console.ReadLine());

                Console.Write("Estimated Resolution Hours: ");
                req.EstimatedResolutionHours = int.Parse(Console.ReadLine());

                // 3 & 4. Process and Generate Report interactively
                manager.ProcessRequest(req);
                requestQueue.Add(req);
                manager.GenerateReport(req);
            }

            // 5 & 6. Final Summary Generation
            Console.WriteLine("\n==== FINAL MUNICIPAL SUMMARY ====");
            if (requestQueue.Count > 0)
            {
                // Find the request with the highest urgency score
                ServiceRequest highestPriority = requestQueue.OrderByDescending(r => r.UrgencyScore).First();

                Console.WriteLine("Highest priority issue:");
                Console.WriteLine($"Resident: {highestPriority.AssignedResident.Name}");
                Console.WriteLine($"Service Type: {highestPriority.RequestType}");
                Console.WriteLine($"Urgency Score: {highestPriority.UrgencyScore}");
                Console.WriteLine($"Adjusted Resolution: {highestPriority.AdjustedResolution} hours");
                Console.WriteLine($"Household Impact Score: {highestPriority.HouseholdImpactScore:F2}");
            }

            Console.WriteLine("\nThank you for using the Emfuleni Municipality Service Desk.");
            Console.ReadKey();
        }
    }
}