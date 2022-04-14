#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trucks_backend.Data;
using trucks_backend.Model;

namespace trucks_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrucksController : ControllerBase
    {
        private readonly trucks_backendContext _context;

        public TrucksController(trucks_backendContext context)
        {
            _context = context;
        }

        private ActionResult ValidateTruck(Truck truck)
        {
            if (!truck.ModelName.StartsWith("FH") && !truck.ModelName.StartsWith("FM"))
                return BadRequest("The truck model must be FH or FM only");

            else if (truck.ManufacturingYear != DateTime.Today.Year)
                return BadRequest("The truck manufacturing year must be the current year");

            else if (truck.ModelYear != DateTime.Today.Year && truck.ModelYear != DateTime.Today.Year + 1)
                return BadRequest("The truck model year must be the current or next year");

            return Ok();
        }

        // GET: api/Trucks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Truck>>> GetTruck()
        {
            return await _context.Truck.ToListAsync();
        }

        // GET: api/Trucks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Truck>> GetTruck(int id)
        {
            var truck = await _context.Truck.FindAsync(id);

            if (truck == null)
            {
                return NotFound();
            }

            return truck;
        }

        // PUT: api/Trucks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTruck(int id, Truck truck)
        {
            if (id != truck.Id)
            {
                return BadRequest();
            }

            ActionResult validatedResult = ValidateTruck(truck);
            if (validatedResult.GetType() != typeof(OkResult)) return validatedResult;


            _context.Entry(truck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trucks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Truck>> PostTruck(Truck truck)
        {
            ActionResult validatedResult = ValidateTruck(truck);
            if (validatedResult.GetType() != typeof(OkResult)) return validatedResult;

            _context.Truck.Add(truck);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTruck", new { id = truck.Id }, truck);
        }

        // DELETE: api/Trucks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(int id)
        {
            var truck = await _context.Truck.FindAsync(id);
            if (truck == null)
            {
                return NotFound();
            }

            _context.Truck.Remove(truck);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TruckExists(int id)
        {
            return _context.Truck.Any(e => e.Id == id);
        }
    }
}
