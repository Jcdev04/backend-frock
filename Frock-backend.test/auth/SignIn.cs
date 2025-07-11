using Frock_backend.IAM.Application.Internal.CommandServices;
using Frock_backend.IAM.Application.Internal.OutboundServices;
using Frock_backend.IAM.Domain.Model.Aggregates;
using Frock_backend.IAM.Domain.Model.Commands;
using Frock_backend.IAM.Domain.Repositories;
using Frock_backend.shared.Domain.Repositories;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class SignInSteps
{
    private readonly Mock<IUserRepository> _repo = new();
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<ITokenService> _tokenService = new();
    private readonly Mock<IHashingService> _hashingService = new();
    private UserCommandService _service;
    private SignInCommand _cmd;
    private User _user;
    private String _token;

    [Given(@"no existe cuenta a acceder")]
    public void GivenNoExistingCompanie()
    {
        _service = new UserCommandService(_repo.Object, _tokenService.Object, _hashingService.Object, _uow.Object);
    }

    [When(@"envío los datos para acceder:")]
    public async Task WhenEnvioDatos(Table table)
    {
        var row = table.Rows[0];
        _cmd = new SignInCommand(
            row["email"],
            row["password"]
        );
        (_user, _token) = await _service.Handle(_cmd);
    }

    [Then(@"accedo a mi cuenta")]
    public void ThenUserAcceded()
        => Assert.NotNull(_user);
}