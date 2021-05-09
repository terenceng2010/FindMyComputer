using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace FindMyComputer.Models
{
    [Index(nameof(Name))]
    public class Connector
    {
        public int ConnectorId { get; set; }
        /// <summary>
        /// e.g USB 3.0
        /// </summary>
        public string Name { get; set; }

        public int Quantity { get; set; }
        
        [JsonIgnore]
        public int ComputerId { get; set; }
        [JsonIgnore]
        public Computer Computer { get; set; }
    }
}