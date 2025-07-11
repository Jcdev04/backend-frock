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
        int stopId = int.Parse(row["id"]);

        // 1. Prepara un objeto Stop simulado (fake) con datos originales
        var fakeStop = new Stop(
            stopId,
            "Paradero Original",  // nombre original, será reemplazado
            "Direccion original",
            int.Parse(row["fkIdCompany"]),
            int.Parse(row["fkIdDistrict"])
        );

        // 2. Configura el mock del repositorio para retornar el stop simulado
        _repo.Setup(r => r.FindByIdAsync(stopId)).ReturnsAsync(fakeStop);

        // 3. Crea el comando con los nuevos datos que vienen de la tabla de tu BDD
        _cmd = new UpdateStopCommand(
            stopId,
            row["name"],
            row["googleMapsUrl"],
            row["imageUrl"],
            row["phone"],
            int.Parse(row["fkIdCompany"]),
            row["address"],
            row["reference"],
            int.Parse(row["fkIdDistrict"])
        );

        // 4. Llama a tu servicio normalmente
        _result = await _service.Handle(_cmd);
    }


    [Then(@"el sistema actualiza el paradero")]
    public void ThenStopDeleted()
        => Assert.NotNull(_result);
    
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