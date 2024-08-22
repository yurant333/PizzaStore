using PizzaStore.Models;
    
namespace PizzaStore.Service;

public class PizzaService
{
    private readonly List<Pizza> _pizzas = new();

    public IEnumerable<Pizza> GetAllPizzas()
    {
        return _pizzas;
    }

    public Pizza AddPizza(string name, string description)
    {
        var newPizza = new Pizza
        {
            Id = _pizzas.Count + 1,
            Name = name,
            Description = description
        };
        _pizzas.Add(newPizza);
        return newPizza;
    }

    public Pizza? GetPizzaById(int id)
    {
        return _pizzas.FirstOrDefault(p => p.Id == id);
    }
}