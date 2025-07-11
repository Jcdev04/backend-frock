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
        var fakeUser = new User(
            row["email"],
            "user1", // o row["username"] si tienes ese dato en tu tabla de test
            "hashed-password", // simula el hash que esperas
            Role.Traveller     // o el rol que desees probar
        );
        // Configura los mocks
        _repo.Setup(r => r.FindByEmailAsync(row["email"])).ReturnsAsync(fakeUser);
        _hashingService.Setup(h => h.VerifyPassword(row["password"], fakeUser.PasswordHash)).Returns(true);
        _tokenService.Setup(t => t.GenerateToken(It.IsAny<User>())).Returns("fake-jwt-token");

        (_user, _token) = await _service.Handle(_cmd);
    }

    [Then(@"accedo a mi cuenta")]
    public void ThenUserAcceded()
    => Assert.NotNull(_user);

    [Then(@"el sistema retorna el id, username, role y token válidos")]
    public void ThenResponseFieldsAreValid()
    {
        Assert.True(_user.Id > -1);
        Assert.False(string.IsNullOrWhiteSpace(_user.Username));
        Assert.True(Enum.IsDefined(typeof(Role), _user.Role));
        Assert.False(string.IsNullOrWhiteSpace(_token));
    }

}