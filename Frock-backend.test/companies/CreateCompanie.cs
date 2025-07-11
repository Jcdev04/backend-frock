using Frock_backend.shared.Domain.Repositories;
using Frock_backend.transport_Company.Application.Internal.CommandServices;
using Frock_backend.transport_Company.Domain.Model.Aggregates;
using Frock_backend.transport_Company.Domain.Model.Commands;
using Frock_backend.transport_Company.Domain.Repositories;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class CreateCompanieSteps
{
    private readonly Mock<ICompanyRepository> _repo = new();
    private readonly Mock<IUnitOfWork> _uow = new();
    private CompanyCommandService _service;
    private CreateCompanyCommand _cmd;
    private Company _result;

    [Given(@"no existe la compañia a crear")]
    public void GivenNoExistingCompanie()
    {
        _service = new CompanyCommandService(_repo.Object, _uow.Object);
    }

    [When(@"envío los datos de la compañia:")]
    public async Task WhenEnvioDatos(Table table)
    {
        var row = table.Rows[0];
        _cmd = new CreateCompanyCommand(
            row["name"],
            row["logoUrl"],
            int.Parse(row["fkIdUser"])
        );
        _result = await _service.Handle(_cmd);
    }

    [Then(@"el sistema crea la compañia")]
    public void ThenCompanieCreated()
      => Assert.NotNull(_result);

    [Then(@"la compañia tiene un Id numérico válido")]
    public void ThenValidId()
      => Assert.True(_result.Id > -1);

    [Then(@"los campos de la compañia coinciden exactamente con los enviados")]
    public void ThenFieldsMatch()
    {
        Assert.Equal(_cmd.Name, _result.Name);
        Assert.Equal(_cmd.LogoUrl, _result.LogoUrl);
        Assert.Equal(_cmd.FkIdUser, _result.FkIdUser);
    }
}
