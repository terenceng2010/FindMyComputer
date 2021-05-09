using FindMyComputer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FindMyComputer.Manager
{
    public class ComputerManager
    {
        public Computer ReadComputer(string ramText, string harddiskText, string connectivityText,
                                     string graphicsCardText, string weightText, string powerSupplyWattText,
                               string cpuModelText)
        {
            var c =  new Computer();
            c.Ram = ComputerManagerUtility.ReadRam(ramText);
            c.HarddiskSize = ComputerManagerUtility.ReadHarddiskSize(harddiskText);
            c.HarddiskType = ComputerManagerUtility.ReadHarddiskType(harddiskText);
            c.Connectors = ComputerManagerUtility.ReadConnectivities(connectivityText);
            c.ConnectorCount = c.Connectors.Select(conn => conn.Quantity).Sum();
            c.GraphicsCardModel = graphicsCardText.Trim();
            c.TowerWeight = ComputerManagerUtility.ReadWeight(weightText);
            c.PowerSupplyWatt = ComputerManagerUtility.ReadPowerSupplyWatt(powerSupplyWattText);
            c.CPUModel = cpuModelText.Trim();
            c.CPUBrand = ComputerManagerUtility.ReadCPUBrand(cpuModelText);
            return c;
        }
    }
}
