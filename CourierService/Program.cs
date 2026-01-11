using System;
using System.Collections.Generic;
using CourierService.Core.Application.Handler;
using CourierService.Core.Presentation.ViewModels;
using CourierService.Core.Domain.Bussiness;

namespace CourierService.Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!GetInputs(out decimal baseDeliveryCost, out List<PackageVm> packages, out VechicleVm vechicle))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var shipmentPackages = new ShipmentHandler()
                .HandleShipment(baseDeliveryCost, packages, vechicle);

            PrintResult(shipmentPackages);
        }

        private static bool GetInputs(out decimal baseDeliveryCost, out List<PackageVm> packages, out VechicleVm vechicle)
        {
            packages = [];
            vechicle = new VechicleVm();

            Console.WriteLine("Enter the Base Delivery Cost:");
            if (!decimal.TryParse(Console.ReadLine(), out baseDeliveryCost))
                return false;

            Console.WriteLine("Enter the No. Of vechicles:");
            if (!int.TryParse(Console.ReadLine(), out int noOfVechicles))
                return false;

            Console.WriteLine("Enter the Vechicle Max Speed:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal maxSpeed))
                return false;

            Console.WriteLine("Enter the Vechicle Max Capacity:");
            if (!int.TryParse(Console.ReadLine(), out int maxLoad))
                return false;

            Console.WriteLine("Enter the No. Of Packages:");
            if (!int.TryParse(Console.ReadLine(), out int noOfPackages) || noOfPackages < 0)
                return false;

            packages = new List<PackageVm>(noOfPackages);
            for (int i = 0; i < noOfPackages; i++)
            {
                Console.WriteLine($"Enter the Details of PKG {i + 1}");
                Console.WriteLine("Enter the Package Weight (kg):");
                if (!int.TryParse(Console.ReadLine(), out int weightInKg))
                    return false;

                Console.WriteLine("Enter the Package Distance (km):");
                if (!decimal.TryParse(Console.ReadLine(), out decimal distanceInKm))
                    return false;

                packages.Add(new PackageVm { Id = i + 1, WeightInKg = weightInKg, DistanceInKm = distanceInKm });
            }

            vechicle = new VechicleVm
            {
                NoOfVechicles = noOfVechicles,
                Maxload = maxLoad,
                MaxSpeed = maxSpeed
            };

            return true;
        }

        private static void PrintResult(IEnumerable<Package> shipmentPackages)
        {
            var rows = shipmentPackages is null ? new List<Package>() : [.. shipmentPackages];
            if (rows.Count == 0)
            {
                Console.WriteLine("No shipment results to display.");
                return;
            }

            const string format = "{0,-12} | {1,15} | {2,12} | {3,30}";
            Console.WriteLine(format, "Package", "Discount Amount", "Final Cost", "Estimated Delivery Time (hrs)");
            Console.WriteLine(new string('-', 80));

            foreach (var package in rows)
            {
                var code = package.PackageCode ?? "-";
                var discount = package.DiscountAmount.ToString("F2");
                var total = package.TotalAmount.ToString("F2");
                var eta = package.EstimatedDeliveryInHrs.ToString("F2");

                Console.WriteLine(format, code, discount, total, eta);
            }
        }
    }
}
