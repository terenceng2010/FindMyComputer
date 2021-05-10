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
            IQueryable<Computer> query = FindMyComputer.Manager.ComputerManagerUtility.Sort(_context.Computers, key, isDesc, limit);
            var computers = await query.Include(c => c.Connectors).ToListAsync();
            return computers;
        }

        [HttpPost("facetsearch")]
        public async Task<ActionResult<IEnumerable<Computer>>> PostFacetSearchComputers(ComputerFacetSearchViewModel computer)
        {
            FilterByConnectionNames(computer);
            IQueryable<Computer> query = FindMyComputer.Manager.ComputerManagerUtility.Filter(_context.Computers, computer);
            var computers = await query.Include(c => c.Connectors).ToListAsync();
            return computers;
        }

        [HttpGet("stat")]
        public async Task<ActionResult<ComputerStatViewModel>> GetStatComputers()
        {
            string sqlStr = @"SELECT 
                  Max([Ram]) MaxRam,
	              Min([Ram]) MinRam,
	              Max([HarddiskSize]) MaxHarddiskSize,
	              Min([HarddiskSize]) MinHarddiskSize,
	              Max([TowerWeight]) MaxTowerWeight,
	              Min([TowerWeight]) MinTowerWeight,
	              Max([PowerSupplyWatt]) MaxPowerSupplyWatt,
	              Min([PowerSupplyWatt]) MinPowerSupplyWatt
              FROM [dbo].[Computer]";
            var q = _context.ComputerStat
                .FromSqlRaw(sqlStr);

            var stat =  await q.FirstOrDefaultAsync();

            stat.HarddiskTypes = await _context.Computers.Select(c => c.HarddiskType).Distinct().ToListAsync();
            stat.CPUBrandList = await _context.Computers.Select(c => c.CPUBrand).Distinct().ToListAsync();
            stat.ConnectorNames = await _context.Connectors.Select(c => c.Name).Distinct().ToListAsync();

            return stat;

            //var stat = new ComputerStatViewModel();
            //stat.MinRam = _context.Computers.Min(c => c.Ram);
            //stat.MaxRam = _context.Computers.Max(c => c.Ram);
            //return stat;
        }

        /// <summary>
        /// add Apple M1 to the computer list (to further illustrate the front-end facet search panel)
        /// </summary>
        /// <returns></returns>
        [HttpGet("addapple")]
        public async Task<int> GetAddApple()
        {
            var m = new Manager.ComputerManager();
            var appleMachine = m.ReadComputer("8 GB", "1 TB SSD", "2 x USB C, 1 x Thunderbolt, 1 x HDMI", "Apple Graphics Card", "0.7 kg", "50 W PSU", "Apple M1");
            _context.Computers.Add(appleMachine);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// remove Apple M1 from the computer list
        /// </summary>
        /// <returns></returns>
        [HttpGet("removeapple")]
        public async Task<int> GetRemoveApple()
        {

            var appleComputers = await _context.Computers.Where(c => c.CPUBrand == "APPLE").ToListAsync();
            foreach(var c in appleComputers)
            {
                _context.Computers.Remove(c);
            }
             
            return await _context.SaveChangesAsync();
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
            if (computersWithConnectors != null)
            {
               computer.ComputerIds = computersWithConnectors;
            }
        }


    }
}
