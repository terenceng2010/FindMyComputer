using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindMyComputer.Data;
using FindMyComputer.Models;
using System.Linq.Expressions;
using FindMyComputer.ViewModels;

namespace FindMyComputer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly FindMyComputerContext _context;

        public ComputersController(FindMyComputerContext context)
        {
            _context = context;
        }

        // GET: api/Computers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Computer>>> GetComputer()
        {
            return await _context.Computers.Include(c => c.Connectors).ToListAsync();
        }


        // GET: api/Computers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Computer>> GetComputer(int id)
        {
            var computer = await _context.Computers.Include(c => c.Connectors).SingleOrDefaultAsync(x => x.ComputerId == id);

            if (computer == null)
            {
                return NotFound();
            }

            return computer;
        }

        [HttpGet("sort")]
        public async Task<ActionResult<IEnumerable<Computer>>> GetSortComputers(string key, bool isDesc, int limit)
        {
            IQueryable<Computer> query = Sort(_context.Computers, key, isDesc, limit);
            var computers = await query.Include(c => c.Connectors).ToListAsync();
            return computers;
        }

        [HttpPost("facetsearch")]
        public async Task<ActionResult<IEnumerable<Computer>>> PostFacetSearchComputers(ComputerFacetSearchViewModel computer)
        {
            FilterByConnectionNames(computer);
            IQueryable<Computer> query = Filter(_context.Computers, computer);
            var computers = await query.Include(c => c.Connectors).ToListAsync();
            return computers;
        }

        private void FilterByConnectionNames(ComputerFacetSearchViewModel computer)
        {
            List<int> computersWithConnectors = null;
            if (computer.ConnectorNames != null && computer.ConnectorNames.Count > 0)
            {
                foreach(string connectorName in computer.ConnectorNames)
                {
                    List<int> computersWithSpecficConnectors = _context.Connectors.Where(c => c.Name == connectorName).Select(a => a.ComputerId).ToList();
                    if(computersWithConnectors == null)
                    {
                        computersWithConnectors = computersWithSpecficConnectors;
                    }
                    computersWithConnectors = computersWithConnectors.Intersect(computersWithSpecficConnectors).ToList();
                }
            }
            if (computersWithConnectors != null && computersWithConnectors.Count > 0)
            {
                computer.ComputerIds = computersWithConnectors;
            }
        }

        [NonAction]
        public IQueryable<Computer> Sort(IQueryable<Computer> computers, string key, bool isDesc, int limit)
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

            if(limit > 0)
            {
                computers = computers.Take(limit);
            }

            return computers;
        }
        
        [NonAction]
        public IQueryable<Computer> Filter(IQueryable<Computer> computers, ComputerFacetSearchViewModel vm)
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
    }
}
