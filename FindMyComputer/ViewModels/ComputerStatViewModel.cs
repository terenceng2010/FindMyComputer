using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FindMyComputer.ViewModels
{
    [Keyless]
    public class ComputerStatViewModel
    {
        public decimal? MinRam { get; set; }
        public decimal? MaxRam { get; set; }

        public decimal? MinHarddiskSize { get; set; }
        public decimal? MaxHarddiskSize { get; set; }

        [NotMapped]
        public List<string> HarddiskTypes { get; set; }

        public decimal? MinTowerWeight { get; set; }
        public decimal? MaxTowerWeight { get; set; }

        public int? MinPowerSupplyWatt { get; set; }
        public int? MaxPowerSupplyWatt { get; set; }

        [NotMapped]
        public List<string> CPUBrandList { get; set; }
        [NotMapped]
        public List<string> ConnectorNames { get; set; }
    }
}
