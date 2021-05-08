using FindMyComputer.Manager;
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

            //var computers = new Computer[]
            //{
            //    new Computer
            //    {
            //        Ram = 8000,
            //        HarddiskSize = 1000,
            //        HarddiskType = "SSD",
            //        GraphicsCardModel = "NVIDIA GeForce GTX 770",
            //        CPUBrand = "Intel",
            //        CPUModel = "Intel® Celeron™ N3050 Processor",
            //        PowerSupplyWatt = 500,
            //        TowerWeight = 8.1M,
            //        Connectors = new List<Connector>{
            //                        new Connector { Name = "USB 3.0", Quantity = 2 },
            //                        new Connector { Name = "USB 2.0", Quantity = 4 },
            //                     }
            //                    
            //    },
            //    new Computer
            //    {
            //        Ram = 16000,
            //        HarddiskSize = 2000,
            //        HarddiskType = "HDD",
            //        GraphicsCardModel = "NVIDIA GeForce GTX 960",
            //        CPUBrand = "AMD",
            //        CPUModel = "AMD FX 4300 Processor",
            //        PowerSupplyWatt = 500,
            //        TowerWeight = 12M,
            //        Connectors = new List<Connector>{
            //            new Connector { Name = "USB 3.0", Quantity = 3 },
            //            new Connector { Name = "USB 2.0", Quantity = 4 },
            //        }
            //
            //    }
            //};
            List<Computer> computers = new List<Computer>();
            var m = new ComputerManager();
            computers.Add( m.ReadComputer("8 GB", "1 TB SSD", "2 x USB 3.0, 4 x USB 2.0", "NVIDIA GeForce GTX 770", "8.1 kg", "500 W PSU", " Intel® Celeron™ N3050 Processor"));
            computers.Add( m.ReadComputer("16 GB", "2 TB HDD", "3 x USB 3.0, 4 x USB 2.0", "NVIDIA GeForce GTX 960", "12 kg", "500 W PSU", "AMD FX 4300 Processor"));
            computers.Add( m.ReadComputer("8 GB", "3 TB HDD", "4 x USB 3.0, 4 x USB 2.0", "Radeon R7360", "16 lb", "450 W PSU", "AMD Athlon Quad-Core APU Athlon 5150"));
            computers.Add( m.ReadComputer("16 GB", "4 TB HDD", "5 x USB 2.0, 4 x USB 3.0", "NVIDIA GeForce GTX 1080", "13.8 lb", "500 W PSU", "AMD FX 8-Core Black Edition FX-8350"));
            computers.Add( m.ReadComputer("32 GB", "750 GB SDD", "2 x USB 3.0, 2 x USB 2.0, 1 x USB C", "Radeon RX 480", "7 kg", "1000 W PSU", "AMD FX 8-Core Black Edition FX-8370"));
            computers.Add( m.ReadComputer("32 GB", "2 TB SDD", "2 x USB C, 4 x USB 3.0", "Radeon R9 380", "6 kg", "450 W PSU", "Intel Core i7-6700K 4GHz Processor"));
            computers.Add( m.ReadComputer("8 GB", "2 TB HDD", "8 x USB 3.0", "NVIDIA GeForce GTX 1080", "15 lb", "1000 W PSU", "Intel® Core™ i5-6400 Processor"));
            computers.Add( m.ReadComputer("16 GB", "500 GB SDD", "4 x USB 2.0", "NVIDIA GeForce GTX 770", "8 lb", "750 W PSU", "Intel® Core™ i5-6400 Processor"));
            computers.Add( m.ReadComputer("2 GB", "2 TB HDD", "10 x USB 3.0, 10 x USB 2.0, 10 x USB C", "AMD FirePro W4100", "9 kg", "508 W PSU", "Intel Core i7 Extreme Edition 3 GHz Processor"));
            computers.Add( m.ReadComputer("512 MB", "80 GB SSD", "19 x USB 3.0, 4 x USB 2.0", "Radeon RX 480", "22 lb", "700 W PSU", "Intel® Core™ i5-6400 Processor"));

            foreach (Computer s in computers)
            {
                context.Computers.Add(s);
            }
            context.SaveChanges();
        }
    }
}
