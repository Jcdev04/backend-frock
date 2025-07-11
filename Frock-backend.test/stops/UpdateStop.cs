using Frock_backend.shared.Domain.Repositories;
using Frock_backend.stops.Application.Internal.CommandServices;
using Frock_backend.stops.Domain.Model.Aggregates;
using Frock_backend.stops.Domain.Model.Commands;
using Frock_backend.stops.Domain.Repositories;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class UpdateStopSteps
{
    private readonly Mock<IStopRepository> _repo = new();
    private readonly Mock<IUnitOfWork> _uow = new();
    private StopCommandService _service;
    private UpdateStopCommand _cmd;
    private Stop _result;

    [Given(@"no existe el paradero a modificar")]
    public void GivenNoExisteStop()
    {
        _service = new StopCommandService(_repo.Object, _uow.Object);
    }

    [When(@"envio el id del paradero con los datos a modificar:")]
    public async Task WhenEnvioId(Table table)
    {
        var row = table.Rows[0];
        _cmd = new UpdateStopCommand(
            int.Parse(row["id"]),
            row["name"],
            row["googleMapsUrl"],
            row["imageUrl"],
            row["phone"],
            int.Parse(row["fkIdCompany"]),
            row["address"],
            row["reference"],
            int.Parse(row["fkIdDistrict"])
        );
        _result = await _service.Handle(_cmd);
    }
    
    [Then(@"el sistema actualiza el paradero")]
    public void ThenStopDeleted()
        => Assert.Null(_result);
    
    [Then(@"los campos del paradero coinciden exactamente con los nuevos datos")]
    public void ThenFieldsMatch()
    {
        Assert.Equal(_result.Id, _cmd.Id);
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