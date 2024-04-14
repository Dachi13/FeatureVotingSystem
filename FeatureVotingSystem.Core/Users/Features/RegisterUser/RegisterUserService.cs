using FeatureVotingSystem.Core.Shared.Features.GetUser;
using FeatureVotingSystem.Shared.Entities.Exceptions;

namespace FeatureVotingSystem.Core.Users.Features.RegisterUser;

public class RegisterUserService : IRegisterUserService
{
    private readonly IRegisterUserRepository _registerUser;
    private readonly IGetUserByEmailRepository _getUserByEmail;
    private readonly IAddRoleToUserRepository _addRoleToUserRepository;

    public RegisterUserService(IRegisterUserRepository registerUser, IGetUserByEmailRepository getUserByEmail,
        IAddRoleToUserRepository addRoleToUserRepository)
    {
        _registerUser = registerUser;
        _getUserByEmail = getUserByEmail;
        _addRoleToUserRepository = addRoleToUserRepository;
    }

    public async Task RegisterAsync(RegisterUserRequest request)
    {
        var validation = UserValidations.ValidateCreateUserRequest(request);

        if (!validation.IsValid) throw new UserBadRequestException(validation.Errors.First().ErrorMessage);

        var user = await _getUserByEmail.FindByEmailAsync(request.Email);

        if (user is not null) throw new UserBadRequestException("Such e-mail is already in use!");

        var entity = new UserEntity
        {
            UserName = request.Name,
            Surname = request.Surname,
            Email = request.Email
        };

        var result = await _registerUser.CreateAsync(entity, request.Password);

        if (!result.Succeeded) throw new UserBadRequestException(result.Errors.First().Description);

        await _addRoleToUserRepository.AddToRoleAsync(entity, "user");
    }
}