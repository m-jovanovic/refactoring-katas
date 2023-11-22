namespace CreditCalculator.Before;

public class CustomerRepository
{
    private readonly List<Customer> _customers = [];

    public List<Customer> GetCustomers() => _customers.ToList();

    public void AddCustomer(Customer customer)
    {
        _customers.Add(customer);
    }
}