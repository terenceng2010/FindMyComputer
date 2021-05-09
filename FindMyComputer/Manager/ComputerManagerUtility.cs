using FindMyComputer.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FindMyComputer.Manager
{
    public class ComputerManagerUtility
    {
        public static decimal ReadRam(string ramText)
        {
            if (ramText == null) { return 0; }
            string[] ramTextSplit = ramText.ToUpperInvariant().Split(' ');
            if (ramTextSplit.Length != 2)
            {
                return 0;
            }
            decimal ramSize;
            if (decimal.TryParse(ramTextSplit[0], out ramSize) == false)
            {
                return 0;
            }
            if (ramTextSplit[1] == "GB")
            {
                return ramSize * 1000;
            }
            else if (ramTextSplit[1] == "MB")
            {
                return ramSize;
            }
            else
            {
                return 0;
            }
        }
        public static decimal ReadHarddiskSize(string harddiskText)
        {
            if (harddiskText == null) { return 0; }
            string[] harddiskTextSplit = harddiskText.ToUpperInvariant().Split(' ');
            if (harddiskTextSplit.Length < 2)
            {
                return 0;
            }
            decimal harddiskSize;
            if (decimal.TryParse(harddiskTextSplit[0], out harddiskSize) == false)
            {
                return 0;
            }
            if (harddiskTextSplit[1] == "TB")
            {
                return harddiskSize * 1000;
            }
            else if (harddiskTextSplit[1] == "GB")
            {
                return harddiskSize;
            }
            else
            {
                return 0;
            }
        }
        public static string ReadHarddiskType(string harddiskText)
        {
            if (harddiskText == null) { return ""; }
            string[] harddiskTextSplit = harddiskText.ToUpperInvariant().Split(' ');
            if (harddiskTextSplit.Length == 3)
            {
                return harddiskTextSplit[2];
            }
            else
            {
                return "";
            }
        }
        public static Connector ReadConnectivity(string connectivityText)
        {
            if (String.IsNullOrWhiteSpace(connectivityText)) { return null; }

            string[] connectivityTextSplitByX = connectivityText.Split('x');
            if(connectivityTextSplitByX.Length != 2) { return null;  }

            int connectorQuantity;
            if (int.TryParse(connectivityTextSplitByX[0], out connectorQuantity) == false)
            {
                return null;
            }

            var c = new Connector { Name = connectivityTextSplitByX[1].Trim(), Quantity = connectorQuantity };
            return c;
        }
        public static List<Connector> ReadConnectivities(string connectivityText)
        {
            var connectors = new List<Connector>();
            if (String.IsNullOrWhiteSpace(connectivityText))
            {
                return connectors;
            }
            string[] connectivityTextSplit = connectivityText.Split(',');
            foreach(string text in connectivityTextSplit)
            {
               Connector c = ReadConnectivity(text.Trim());
               if (c != null) connectors.Add(c);
            }
            return connectors;
        }

        public static decimal ReadWeight(string weightText)
        {
            if (weightText == null) { return 0; }
            string[] weightTextSplit = weightText.ToUpperInvariant().Split(' ');
            if (weightTextSplit.Length != 2)
            {
                return 0;
            }
            decimal weight;
            if (decimal.TryParse(weightTextSplit[0], out weight) == false)
            {
                return 0;
            }
            if (weightTextSplit[1] == "LB")
            {
                return weight * 0.45359237M;
            }
            else if (weightTextSplit[1] == "KG")
            {
                return weight;
            }
            else
            {
                return 0;
            }
        }

        public static int ReadPowerSupplyWatt(string powerSupplyWattText)
        {
            if (String.IsNullOrWhiteSpace(powerSupplyWattText)){
                return 0;
            }
            string[] powerSupplyWattTextSplit = powerSupplyWattText.Split(' ');
            if (powerSupplyWattTextSplit.Length != 3)
            {
                return 0;
            }
            int powerSupplyWatt;
            if (int.TryParse(powerSupplyWattTextSplit[0], out powerSupplyWatt) == false)
            {
                return 0;
            }
            else
            {
                return powerSupplyWatt;
            }
        }

        public static string ReadCPUBrand(string cpuModelText)
        {
            if (String.IsNullOrWhiteSpace(cpuModelText))
            {
                return "";
            }
            //remove special characters
            string cpuModelTextNoSpecialChar = Regex.Replace(cpuModelText.ToUpperInvariant(), @"[^0-9a-zA-Z ]+", "");
            string[] cpuModelTextSplit = cpuModelTextNoSpecialChar.Split(' ');
            return cpuModelTextSplit[0];
        }


    }
}
