using Frock_backend.shared.Domain.Repositories;
using Frock_backend.stops.Application.Internal.CommandServices;
using Frock_backend.stops.Domain.Model.Aggregates;
using Frock_backend.stops.Domain.Model.Commands;
using Frock_backend.stops.Domain.Repositories;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class DeleteStopSteps
{
    private readonly Mock<IStopRepository> _repo = new();
    private readonly Mock<IUnitOfWork> _uow = new();
    private StopCommandService _service;
    private DeleteStopCommand _cmd;
    private Stop _result;

    [Given(@"no existe el paradero")]
    public void GivenNoExisteStop()
    {
        _service = new StopCommandService(_repo.Object, _uow.Object);
    }

    [When(@"envio el id del paradero:")]
    public async Task WhenEnvioId(Table table)
    {
        var row = table.Rows[0];
        _cmd = new DeleteStopCommand(
            int.Parse(row["id"])
            );
        _result = await _service.Handle(_cmd);
    }
    
    [Then(@"el sistema elimina el paradero")]
    public void ThenStopDeleted()
        => Assert.Null(_result);
}