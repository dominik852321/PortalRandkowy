using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController: ControllerBase
    {
        private readonly DataContext _dataContext;
        
       public ValueController(DataContext context)
       {
           _dataContext=context;
       }

        [HttpGet]
        public async Task<IEnumerable<Value>> GetAll()
            => await _dataContext.Values.ToListAsync();


        [HttpGet("{id}")]
        public async Task<Value> Get(int id)
          => await _dataContext.Values.FirstOrDefaultAsync(s => s.id == id);


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Value value)
        {

            _dataContext.Values.Add(value);
            await _dataContext.SaveChangesAsync();
            return Ok(value);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Value value)
        {
            var model = await _dataContext.Values.FirstOrDefaultAsync(s => s.id == id);
            model.name = value.name;
            _dataContext.Values.Update(model);
            await _dataContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _dataContext.Values.FirstOrDefaultAsync(s => s.id == id);

            if (model != null)
            {
                _dataContext.Values.Remove(model);
                await _dataContext.SaveChangesAsync();
                return Ok();
            }
            return NoContent();
        }


    }
}