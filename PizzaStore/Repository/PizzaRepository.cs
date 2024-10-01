using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore.Repository;

public class PizzaRepository : IPizzaRepository
{
    private readonly PizzaDb _context;

    public PizzaRepository(PizzaDb context)
    {
        _context = context;
    }

    public async Task<List<Pizza>> GetPizzasAsync()
    {
        return await _context.Pizzas.ToListAsync();
    }

    public async Task<Pizza> GetPizzaByIdAsync(int id)
    {
        return await _context.Pizzas.FindAsync(id);
    }

    public async Task AddPizzaAsync(Pizza pizza)
    {
        await _context.Pizzas.AddAsync(pizza);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePizzaAsync(Pizza pizza)
    {
        _context.Pizzas.Update(pizza);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePizzaAsync(int id)
    {
        var pizza = await _context.Pizzas.FindAsync(id);
        if (pizza != null)
        {
            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
        }
    }
}
