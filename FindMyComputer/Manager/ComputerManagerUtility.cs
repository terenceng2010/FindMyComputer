using FindMyComputer.Models;
using FindMyComputer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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


        public static IQueryable<Computer> Sort(IQueryable<Computer> computers, string key, bool isDesc, int limit)
        {
            Expression<Func<Computer, object>> sortExpression;
            switch (key)
            {
                case "HarddiskSize":
                    sortExpression = (q => q.HarddiskSize);
                    break;
                case "ConnectorCount":
                    sortExpression = (q => q.ConnectorCount);
                    break;
                case "TowerWeight":
                    sortExpression = (q => q.TowerWeight);
                    break;
                default:
                    sortExpression = (q => q.ComputerId);
                    break;
            }

            // sort direction
            computers = isDesc
                      ? computers.OrderByDescending(sortExpression)
                      : computers.OrderBy(sortExpression);

            if (limit > 0)
            {
                computers = computers.Take(limit);
            }

            return computers;
        }


        public static IQueryable<Computer> Filter(IQueryable<Computer> computers, ComputerFacetSearchViewModel vm)
        {
            computers = FilterByRam(computers, vm);
            computers = FilterByHarddiskSize(computers, vm);
            computers = FilterByHarddiskType(computers, vm);
            computers = FilterByGraphicsCardModelKeyword(computers, vm);
            computers = FilterByTowerWeight(computers, vm);
            computers = FilterByPowerSupplyWatt(computers, vm);
            computers = FilterByCPUBrand(computers, vm);
            computers = FilterByCPUModelKeyword(computers, vm);
            computers = FilterByComputerIds(computers, vm);
            return computers;
        }


        //might also opt for reflection with LINQ to abstract common logic in the below filter by methods
        #region filterByKeyword
        
        private static IQueryable<Computer> FilterByGraphicsCardModelKeyword(IQueryable<Computer> computers, ComputerFacetSearchViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(vm.GraphicsCardModelKeyword))
            {
                //Full-text search should be used instead in a real app, as using % in prefix means a table scan has to be done.
                computers = computers.Where(c => EF.Functions.Like(c.GraphicsCardModel, "%" + vm.GraphicsCardModelKeyword + "%"));
            }

            return computers;
        }
        private static IQueryable<Computer> FilterByCPUModelKeyword(IQueryable<Computer> computers, ComputerFacetSearchViewModel vm)
        {
            if (!string.IsNullOrWhiteSpace(vm.CPUModelKeyword))
            {
                //Full-text search should be used instead in a real app, as using % in prefix means a table scan has to be done.
                computers = computers.Where(c => EF.Functions.Like(c.CPUModel, "%" + vm.CPUModelKeyword + "%"));
            }

            return computers;
        }
        #endregion
        #region filterByMinMaxSize
        private static IQueryable<Computer> FilterByHarddiskSize(IQueryable<Computer> computers, ComputerFacetSearchViewModel vm)
        {
            if (vm.MinHarddiskSize != null || vm.MaxHarddiskSize != null)
            {
                if (vm.MinHarddiskSize != null && vm.MaxHarddiskSize != null)
                {
                    computers = computers.Where(c => c.HarddiskSize >= vm.MinHarddiskSize && c.HarddiskSize <= vm.MaxHarddiskSize);
                }
                else if (vm.MinHarddiskSize != null)
                {
                    computers = computers.Where(c => c.HarddiskSize >= vm.MinHarddiskSize);
                }
                else if (vm.MaxHarddiskSize != null)
                {
                    computers = computers.Where(c => c.HarddiskSize <= vm.MaxHarddiskSize);
                }
            }

            return computers;
        }

        private static IQueryable<Computer> FilterByRam(IQueryable<Computer> computers, ComputerFacetSearchViewModel vm)
        {
            if (vm.MinRam != null || vm.MaxRam != null)
            {
                if (vm.MinRam != null && vm.MaxRam != null)
                {
                    computers = computers.Where(c => c.Ram >= vm.MinRam && c.Ram <= vm.MaxRam);
                }
                else if (vm.MinRam != null)
                {
                    computers = computers.Where(c => c.Ram >= vm.MinRam);
                }
                else if (vm.MaxRam != null)
                {
                    computers = computers.Where(c => c.Ram <= vm.MaxRam);
                }
            }

            return computers;
        }

        private static IQueryable<Computer> FilterByTowerWeight(IQueryable<Computer> computers, ComputerFacetSearchViewModel vm)
        {
            if (vm.MinTowerWeight != null || vm.MaxTowerWeight != null)
            {
                if (vm.MinTowerWeight != null && vm.MaxTowerWeight != null)
                {
                    computers = computers.Where(c => c.TowerWeight >= vm.MinTowerWeight && c.TowerWeight <= vm.MaxTowerWeight);
                }
                else if (vm.MinTowerWeight != null)
                {
                    computers = computers.Where(c => c.TowerWeight >= vm.MinTowerWeight);
                }
                else if (vm.MaxTowerWeight != null)
                {
                    computers = computers.Where(c => c.TowerWeight <= vm.MaxTowerWeight);
                }
            }

            return computers;
        }

        private static IQueryable<Computer> FilterByPowerSupplyWatt(IQueryable<Computer> computers, ComputerFacetSearchViewModel vm)
        {
            if (vm.MinPowerSupplyWatt != null || vm.MaxPowerSupplyWatt != null)
            {
                if (vm.MinPowerSupplyWatt != null && vm.MaxPowerSupplyWatt != null)
                {
                    computers = computers.Where(c => c.PowerSupplyWatt >= vm.MinPowerSupplyWatt && c.PowerSupplyWatt <= vm.MaxPowerSupplyWatt);
                }
                else if (vm.MinPowerSupplyWatt != null)
                {
                    computers = computers.Where(c => c.PowerSupplyWatt >= vm.MinPowerSupplyWatt);
                }
                else if (vm.MaxPowerSupplyWatt != null)
                {
                    computers = computers.Where(c => c.PowerSupplyWatt <= vm.MaxPowerSupplyWatt);
                }
            }

            return computers;
        }
        #endregion
        #region filterByInClause
        private static IQueryable<Computer> FilterByCPUBrand(IQueryable<Computer> computers, ComputerFacetSearchViewModel vm)
        {
            if (vm.CPUBrandList != null && vm.CPUBrandList.Count > 0)
            {
                computers = computers.Where(c => vm.CPUBrandList.Contains(c.CPUBrand));
            }

            return computers;
        }
        private static IQueryable<Computer> FilterByHarddiskType(IQueryable<Computer> computers, ComputerFacetSearchViewModel vm)
        {
            if (vm.HarddiskTypes != null && vm.HarddiskTypes.Count > 0)
            {
                computers = computers.Where(c => vm.HarddiskTypes.Contains(c.HarddiskType));
            }

            return computers;
        }

        private static IQueryable<Computer> FilterByComputerIds(IQueryable<Computer> computers, ComputerFacetSearchViewModel vm)
        {
            if (vm.ComputerIds != null)
            {
                computers = computers.Where(c => vm.ComputerIds.Contains(c.ComputerId));
            }

            return computers;
        }
        #endregion
    }
}
