namespace CreditCalculator.Before;

public class Company
{
    public static Company Regular = new() { Id = 1, Type = "Regular" };
    public static Company ImportantClient = new() { Id = 2, Type = "ImportantClient" };
    public static Company VeryImportantClient = new() { Id = 3, Type = "VeryImportantClient" };

    public int Id { get; set; }

    public string Type { get; set; }
}