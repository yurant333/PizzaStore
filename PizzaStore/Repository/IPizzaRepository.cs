using PizzaStore.Models;

namespace PizzaStore.Repository;

public interface IPizzaRepository
{
    Task<List<Pizza>> GetPizzasAsync();
    Task<Pizza> GetPizzaByIdAsync(int id);
    Task AddPizzaAsync(Pizza pizza);
    Task UpdatePizzaAsync(Pizza pizza);
    Task DeletePizzaAsync(int id);
}
