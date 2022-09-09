using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workout_Tracker_API.Context;
using Workout_Tracker_API.Models;
using Workout_Tracker_API.Services;

namespace Workout_Tracker_API.Controllers
{
    // TODO: Make a method to split the array of exercises in the routine, then check if it's in Exercises DbSet, if true, add a make a method to return the exercise and its data by a url containing the exercise ID. If false, then just create a new exercise.

    [Authorize]
    [ApiController]
    [Route("Workout/[Controller]/[Action]")]
    public class ExerciseController : ControllerBase
    {
        private readonly WorkoutContext _context;
        
        public ExerciseController(WorkoutContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> All()
        {
            try
            {
                if (_context.Exercises is not null)
                {
                    List<Exercise> Exercises = await _context.Exercises.ToListAsync();
                    return Ok(Exercises);
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
        public async Task<ActionResult<Exercise>> GetExercise(int id)
        {
            try
            {
                if (_context.Exercises is not null)
                {
                    Exercise? Exercise = await _context.Exercises.FindAsync(id);

                return Exercise is null ? NotFound() : Ok(Exercise);
                }

                return NoContent();
            } 
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Exercise NewExercise)
        {
            if (_context.Exercises is not null)
            {
                await _context.Exercises.AddAsync(NewExercise);
                await _context.SaveChangesAsync();

                return Ok(NewExercise);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] Exercise Edited, int id)
        {
            if (id != Edited.ID)
                return BadRequest();
            
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
            if (_context.Exercises is not null)
            {
                Exercise? Exercise = await _context.Exercises.Where(e => id == e.ID).FirstOrDefaultAsync();

                if (Exercise is not null)
                {
                    _context.Exercises.Remove(Exercise);
                    await _context.SaveChangesAsync();

                    return Ok("Deleted succesfully");
                }

                return NotFound();
            }

            return BadRequest();
        }
    }
}