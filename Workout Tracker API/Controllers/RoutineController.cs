using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Workout_Tracker_API.Context;
using Workout_Tracker_API.Models;
using Workout_Tracker_API.Services;
using Workout_Tracker_API.Services.Interfaces;

namespace Workout_Tracker_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Workout/[Controller]/[Action]")]
    public class RoutineController : ControllerBase
    {
        private readonly WorkoutContext _context;
        private readonly IRoutineService _service;        
        public RoutineController(WorkoutContext context, IRoutineService service)
        {
            _context = context;
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Routine>>> All()
        {
            try
            {
                if (_context.Routines is not null)
                {
                    Utilities U = new(_context);
                    List<Routine> Routines = await _context.Routines.ToListAsync();

                    List<string> Exercises = await _service.GetExerciseLink(Routines);

                    for(int i = 0 ; i < Routines.Count ; i++)
                    {
                        Routines[i].RoutineExercises = Exercises[i];
                    }

                    return Ok(Routines);
                }

                return NoContent();
            }
            catch (System.Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Routine>> GetRoutine(int id)
        {
            try
            {
                if (_context.Routines is not null)
                {
                    Routine? Routines = await _context.Routines.FindAsync(id);

                    return Routines is null ? NotFound() : Ok(Routines);
                }

                return NoContent();
            }
            catch (System.Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Routine NewRoutine)
        {
            try
            {
                await _context.AddAsync(NewRoutine);
                await _context.SaveChangesAsync();

                return Ok(_context.Routines);
            }
            catch (System.Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromBody] Routine Edited, int id)
        {
            if (id != Edited.ID)
                return NotFound();
            
            try
            {
				_context.Update(Edited);
				await _context.SaveChangesAsync();
            
            }
            catch (DbUpdateConcurrencyException)
            {
                Utilities U = new(_context);
                if (!U.Exists(id)) 
                    return NotFound();
                
                else
                    throw;
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Routines is not null)
            {
                Routine? Routine = await _context.Routines.Where(e => id == e.ID).FirstOrDefaultAsync();

                if (Routine is not null)
                {
                    _context.Routines.Remove(Routine);
                    await _context.SaveChangesAsync();

                    return Ok("Deleted succesfully");
                }

                return NotFound();
            }

            return BadRequest();
        }

    }
}