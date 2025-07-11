using Frock_backend.IAM.Application.Internal.CommandServices;
using Frock_backend.IAM.Application.Internal.OutboundServices;
using Frock_backend.IAM.Domain.Model.Aggregates;
using Frock_backend.IAM.Domain.Model.Commands;
using Frock_backend.IAM.Domain.Model.ValueObjects;
using Frock_backend.IAM.Domain.Repositories;
using Frock_backend.shared.Domain.Repositories;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class SignUpSteps
{
    private readonly Mock<IUserRepository> _repo = new();
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<ITokenService> _tokenService = new();
    private readonly Mock<IHashingService> _hashingService = new();
    private UserCommandService _service;
    private SignUpCommand _cmd;

    [Given(@"no existe cuenta a crear")]
    public void GivenNoExistingCompanie()
    {
        _service = new UserCommandService(_repo.Object, _tokenService.Object, _hashingService.Object, _uow.Object);
    }

    [When(@"envío los datos para la nueva cuenta:")]
    public async Task WhenEnvioDatos(Table table)
    {
        var row = table.Rows[0];
        _cmd = new SignUpCommand {
            Email = row["email"],
            Username=row["username"],
            Password = row["password"],
            Role = (Role)int.Parse(row["role"])
        };
        await _service.Handle(_cmd);
    }

    [Then(@"el sistema no devuelve error")]
    public void ThenNoExceptionThrown()
    {
        // No necesitas hacer nada: si hay excepción, la prueba falla automáticamente
    }

    [Then(@"el repositorio guarda la nueva cuenta")]
    public void ThenRepositoryCalled()
    {
        _repo.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
    }
}