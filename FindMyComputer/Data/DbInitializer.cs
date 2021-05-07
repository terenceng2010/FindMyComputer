using FindMyComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindMyComputer.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FindMyComputerContext context)
        {
            context.Database.EnsureCreated();

            // Look for any computers.
            if (context.Computers.Any())
            {
                return;   // DB has been seeded
            }

            var computers = new Computer[]
            {
                new Computer
                {
                    Ram = 8000,
                    HarddiskSize = 1000,
                    HarddiskType = "SSD",
                    GraphicsCardModel = "NVIDIA GeForce GTX 770",
                    CPUBrand = "Intel",
                    CPUModel = "Intel® Celeron™ N3050 Processor",
                    PowerSupplyWatt = 500,
                    TowerWeight = 8.1M
                },
                new Computer
                {
                    Ram = 16000,
                    HarddiskSize = 2000,
                    HarddiskType = "HDD",
                    GraphicsCardModel = "NVIDIA GeForce GTX 960",
                    CPUBrand = "AMD",
                    CPUModel = "AMD FX 4300 Processor",
                    PowerSupplyWatt = 500,
                    TowerWeight = 12M
                }
            };
            foreach (Computer s in computers)
            {
                context.Computers.Add(s);
            }
            context.SaveChanges();
        }
    }
}
