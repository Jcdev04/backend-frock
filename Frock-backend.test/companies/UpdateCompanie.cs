using Frock_backend.shared.Domain.Repositories;
using Frock_backend.transport_Company.Application.Internal.CommandServices;
using Frock_backend.transport_Company.Domain.Model.Aggregates;
using Frock_backend.transport_Company.Domain.Model.Commands;
using Frock_backend.transport_Company.Domain.Repositories;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class UpdateCompanieSteps
{
    private readonly Mock<ICompanyRepository> _repo = new();
    private readonly Mock<IUnitOfWork> _uow = new();
    private CompanyCommandService _service;
    private UpdateCompanyCommand _cmd;
    private Company _result;

    [Given(@"no existe la compañia a modificar")]
    public void GivenNoExistingCompanie()
    {
        _service = new CompanyCommandService(_repo.Object, _uow.Object);
    }

    [When(@"envio el id de la compañia con los datos a modificar:")]
    public async Task WhenEnvioDatos(Table table)
    {
        var row = table.Rows[0];
        int companyId = int.Parse(row["id"]);
        // nuevo 
        // Prepara un objeto simulado (fake) con los datos originales
        var fakeCompany = new Company
        (
            companyId,
            "Empresa Original",  // nombre original, será sobreescrito por el update
            "logo.png",          // logo original, será sobreescrito por el update
            int.Parse(row["fkIdUser"])
        );

        // Configura el mock para devolver el objeto cuando se busque por ID
        _repo.Setup(r => r.FindByIdAsync(companyId)).ReturnsAsync(fakeCompany);

        // (Opcional) Si tu servicio guarda cambios, puedes mockear SaveChanges/Update si aplica

        // Crea el comando con los nuevos datos (del test)
        _cmd = new UpdateCompanyCommand(
            companyId,
            row["name"],
            row["logoUrl"],
            int.Parse(row["fkIdUser"])
        );

        _result = await _service.Handle(_cmd);
    }

    [Then(@"el sistema actualiza la compañia")]
    public void ThenCompanieUpdated()
        => Assert.NotNull(_result);

    [Then(@"los campos de la compañia coinciden exactamente con los nuevos datos")]
    public void ThenFieldsMatch()
    {
        Assert.Equal(_cmd.Id, _result.Id);
        Assert.Equal(_cmd.Name, _result.Name);
        Assert.Equal(_cmd.LogoUrl, _result.LogoUrl);
        Assert.Equal(_cmd.FkIdUser, _result.FkIdUser);
    }
}