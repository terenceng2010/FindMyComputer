using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyComputer.Models
{
    [Index(nameof(Ram))]
    [Index(nameof(HarddiskSize))]
    [Index(nameof(HarddiskType))]
    [Index(nameof(GraphicsCardModel))]
    [Index(nameof(TowerWeight))]
    [Index(nameof(PowerSupplyWatt))]
    [Index(nameof(CPUModel))]
    [Index(nameof(CPUBrand))]
    public class Computer
    {
        public int ComputerId { get; set; }

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

        public List<Connector> Connectors { get; set; }
        public int ConnectorCount { get; set; }


    }
}