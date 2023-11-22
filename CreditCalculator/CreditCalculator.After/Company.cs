namespace CreditCalculator.After;

public class Company
{
    public static Company RegularClient = new() { Id = 1, Type = CompanyType.Regular };
    public static Company ImportantClient = new() { Id = 2, Type = CompanyType.ImportantClient };
    public static Company VeryImportantClient = new() { Id = 3, Type = CompanyType.VeryImportantClient };

    public int Id { get; set; }

    public CompanyType Type { get; set; }
}