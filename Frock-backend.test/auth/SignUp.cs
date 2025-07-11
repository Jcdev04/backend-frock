using Frock_backend.IAM.Application.Internal.CommandServices;
using Frock_backend.IAM.Application.Internal.OutboundServices;
using Frock_backend.IAM.Domain.Model.Aggregates;
using Frock_backend.IAM.Domain.Model.Commands;
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
    private User _result;

    [Given(@"no existe cuenta a crear")]
    public void GivenNoExistingCompanie()
    {
        _service = new UserCommandService(_repo.Object, _tokenService.Object, _hashingService.Object, _uow.Object);
    }

    [When(@"envío los datos para la nueva cuenta:")]
    public async Task WhenEnvioDatos(Table table)
    {
        var row = table.Rows[0];
        _cmd = new SignUpCommand(
            row["email"],
            row["username"],
            row["password"],
            int.Parse(row["role"])
        );
        _result = await _service.Handle(_cmd);
    }

    [Then(@"el sistema crea una nueva cuenta")]
    public void ThenUserCreated()
        => Assert.NotNull(_result);

    [Then(@"la cuenta tiene un Id numérico válido")]
    public void ThenValidId()
        => Assert.True(_result.Id > -1);

    [Then(@"el nombre y correo de la cuenta creada coincide con lo ingresado")]
    public void ThenFieldsMatch()
    {
        Assert.Equal(_cmd.Email, _result.Email);
        Assert.Equal(_cmd.Username, _result.Username);
    }
}