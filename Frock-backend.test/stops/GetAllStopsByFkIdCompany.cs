using Frock_backend.shared.Domain.Repositories;
using Frock_backend.stops.Application.Internal.QueryServices;
using Frock_backend.stops.Domain.Model.Aggregates;
using Frock_backend.stops.Domain.Model.Queries;
using Frock_backend.stops.Domain.Repositories;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class GetAllStopsByFkIdCompanySteps
{
    private readonly Mock<IStopRepository> _repo = new();
    private StopQueryService _service;
    private GetAllStopsByFkIdCompanyQuery _qry;
    private IEnumerable<Stop> _results;

    [Given(@"no existe el paradero a obtener por id de compañia")]
    public void GivenNoExisteStop()
    {
        _service = new StopQueryService(_repo.Object);
    }

    [When(@"envio el id de la compañia:")]
    public async Task WhenEnvioId(Table table)
    {
        var row = table.Rows[0];
        _qry = new GetAllStopsByFkIdCompanyQuery(
            int.Parse(row["id"])
        );
        _results = await _service.Handle(_qry);
    }
    
    [Then(@"el sistema muestra todos los parederos de la compañia")]
    public void ThenStopDeleted()
        => Assert.Empty(_results);
}