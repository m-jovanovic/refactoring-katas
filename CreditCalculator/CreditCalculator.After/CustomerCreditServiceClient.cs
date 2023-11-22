namespace CreditCalculator.After;

public class CustomerCreditServiceClient
{
    public decimal GetCreditLimit(string firstName, string lastName, DateTime dateOfBirth)
    {
        if (firstName == "John" && lastName == "Doe")
        {
            return 500.0m;
        }

        if (DateTime.Now.Year - dateOfBirth.Year > 40)
        {
            return 600.0m;
        }

        return 249.9m;
    }
}