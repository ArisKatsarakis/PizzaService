using C_Projects.Models;
using C_Projects.Services;
using Microsoft.AspNetCore.Mvc;


namespace C_Projects.Controllers;


[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
        
    }
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
        PizzaService.GetAll();
        
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza == null)
                return NotFound();
            
            return pizza;
        }

    /*
    *This Post Http Request uses PizzaService Class
    * to add Pizza in the cache using the Add function
    *
    */
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Create), new {id = pizza.Id}, pizza);

    }

    /*
    * This PUT Request is using The PizzaService
     Get Function to get the Current Piizza
     After using the Update function we update current Pizza 
     with the new payload
    */
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();
        var existingPizza = PizzaService.Get(id);
        if(existingPizza is null)
            return NotFound();
        
        PizzaService.Update(pizza);

        return NoContent();

    }

    /*
        This DELETE Http Request is using The PizzaService 
        First to Get the Pizza using id
        and then if it does exist 
        Delete using the synonnymous function from the Service
    */
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if(pizza is null)
            return NotFound();
        
        PizzaService.Delete(id);

        return NoContent();

    }
}