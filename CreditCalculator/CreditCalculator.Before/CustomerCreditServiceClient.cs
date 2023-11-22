namespace CreditCalculator.Before;

public class CustomerCreditServiceClient
{
    public decimal GetCreditLimit(string firstName, string lastName, DateTime dateOfBirth)
    {
        if (firstName == "John" && lastName == "Doe")
        {
            return 400.0m;
        }

        if (DateTime.Now.AddYears(dateOfBirth.Year).Year > 40)
        {
            return 600.0m;
        }

        return 251.0m;
    }
}