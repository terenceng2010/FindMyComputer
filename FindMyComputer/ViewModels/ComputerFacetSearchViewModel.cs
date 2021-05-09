using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FindMyComputer.ViewModels
{
    public class ComputerFacetSearchViewModel
    {
        public decimal? MinRam { get; set; }
        public decimal? MaxRam { get; set; }

        public decimal? MinHarddiskSize { get; set; }
        public decimal? MaxHarddiskSize { get; set; }

        public List<string> HarddiskTypes { get; set; }

        public string GraphicsCardModelKeyword { get; set; }

        public decimal? MinTowerWeight { get; set; }
        public decimal? MaxTowerWeight { get; set; }

        public int? MinPowerSupplyWatt { get; set; }
        public int? MaxPowerSupplyWatt { get; set; }

        public string CPUModelKeyword { get; set; }

        public List<string> CPUBrandList { get; set; }
        public List<string> ConnectorNames { get; set; }

        [JsonIgnore]
        public List<int> ComputerIds { get; set; }
    }
}
