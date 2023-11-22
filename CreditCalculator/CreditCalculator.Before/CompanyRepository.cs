namespace CreditCalculator.Before;

public class CompanyRepository
{
    private readonly List<Company> _companies = new()
    {
        Company.Regular,
        Company.Regular,
        Company.Regular
    };

    public Company GetById(int companyId)
    {
        return _companies.Single(c => c.Id == companyId);
    }
}