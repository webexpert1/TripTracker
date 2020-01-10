﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripTracker.BackService.Data;
using TripTracker.BackService.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TripTracker.BackService.Controllers
{
    [Produces("application/json")]
    [Route("api/Trips")]
    public class TripsController : Controller
    {
        //private readonly Repository _repository;
        TripContext _context;
        public TripsController(TripContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var trips =  await _context.Trips.AsNoTracking().ToListAsync();
            return Ok(trips);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Trip Get(int id)
        {
            return _context.Trips.Find(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Trip value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Trips.Add(value);
            _context.SaveChanges();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]Trip value)
        {
            if(!_context.Trips.Any(t => t.Id == id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Trips.Update(value);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var myTrip = _context.Trips.Find(id);
            if(myTrip == null)
            {
                return NotFound();
            }
            _context.Trips.Remove(myTrip);
            _context.SaveChanges();

            return NoContent();
        }
    }
}