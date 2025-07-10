using Frock_backend.shared.Domain.Repositories;
using Frock_backend.stops.Application.Internal.CommandServices;
using Frock_backend.stops.Domain.Model.Aggregates;
using Frock_backend.stops.Domain.Model.Commands;
using Frock_backend.stops.Domain.Repositories;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class CreateStopSteps
{
    private readonly Mock<IStopRepository> _repo = new();
    private readonly Mock<IUnitOfWork> _uow = new();
    private StopCommandService _service;
    private CreateStopCommand _cmd;
    private Stop _result;

    [Given(@"no existe un paradero llamado ""(.*)""")]
    public void GivenNoExistingStop(string name)
    {
        _service = new StopCommandService(_repo.Object, _uow.Object);
    }

    [When(@"envío los datos del paradero:")]
    public async Task WhenEnvioDatos(Table table)
    {
        var row = table.Rows[0];
        _cmd = new CreateStopCommand(
            row["name"],
            row["googleMapsUrl"],   // <–– debe existir este header
            row["imageUrl"],
            row["phone"],
            int.Parse(row["fkIdCompany"]),
            row["address"],
            row["reference"],
            int.Parse(row["fkIdDistrict"])
        );
        _result = await _service.Handle(_cmd);
    }

    [Then(@"el sistema crea un nuevo paradero")]
    public void ThenStopCreated()
      => Assert.NotNull(_result);

    [Then(@"el paradero tiene un Id numérico válido")]
    public void ThenValidId()
      => Assert.True(_result.Id > -1);

    [Then(@"los campos del paradero coinciden exactamente con los enviados")]
    public void ThenFieldsMatch()
    {
        Assert.Equal(_cmd.Name, _result.Name);
        Assert.Equal(_cmd.GoogleMapsUrl, _result.GoogleMapsUrl);
        Assert.Equal(_cmd.ImageUrl, _result.ImageUrl);
        Assert.Equal(_cmd.Phone, _result.Phone);
        Assert.Equal(_cmd.FkIdCompany, _result.FkIdCompany);
        Assert.Equal(_cmd.Address, _result.Address);
        Assert.Equal(_cmd.Reference, _result.Reference);
        Assert.Equal(_cmd.FkIdDistrict, _result.FkIdDistrict);
    }
}
