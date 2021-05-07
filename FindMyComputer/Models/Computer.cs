using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyComputer.Models
{
    public class Computer
    {
        public int ID { get; set; }

        /// <summary>
        /// Ram in MB
        /// </summary>
        public decimal Ram { get; set; }

        /// <summary>
        /// Harddisk size in GB
        /// </summary>
        public decimal HarddiskSize { get; set; }

        public string HarddiskType { get; set; }

        public string GraphicsCardModel { get; set; }

        /// <summary>
        /// Tower Weight in kg
        /// </summary>
        public decimal TowerWeight { get; set; }

        public int PowerSupplyWatt { get; set; }

        public string CPUModel { get; set; }

        //public CPUTier CPUTier { get; set; }

        public string CPUBrand { get; set; }

        //public List<Connector> Connectors { get; set; }
        //public int ConnectorCount {
        //    get {
        //        if (Connectors == null)
        //        { return 0; } else { return Connectors.Count; }
        //    }
        //}
    }
}