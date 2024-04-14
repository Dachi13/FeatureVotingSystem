using FeatureVotingSystem.Core.Users.Features;
using FeatureVotingSystem.Core.Users.Features.LoginUser;
using FeatureVotingSystem.Core.Users.Features.RegisterUser;
using Shouldly;

namespace FeatureVotingSystem.Core.Tests.Users;

[TestFixture]
public class UserValidationsTests
{
    public UserValidations Validations;

    [SetUp]
    public void SetUp()
    {
        Validations = new UserValidations();
    }

    [Test]
    public void ShouldNotThrowAnErrorWhenValidUserRequestIsBeingPassed()
    {
        var request = new RegisterUserRequest
        {
            Name = "ValidName",
            Email = "validEmail@gmail.com",
            Password = "StrongPassword123",
            Surname = "ValidSurname"
        };

        var validation = UserValidations.ValidateCreateUserRequest(request);
        
        validation.IsValid.ShouldBeTrue();
    }
    
    [Test]
    public void ShouldThrowAnErrorWhenTooLongNameIsBeingPassedInRegisterUserRequest()
    {
        var request = new RegisterUserRequest
        {
            Name = new string('A', 31),
            Email = "validEmail@gmail.com",
            Password = "StrongPassword123",
            Surname = "ValidSurname"
        };
        var validation = UserValidations.ValidateCreateUserRequest(request);
        validation.IsValid.ShouldBeFalse();
    }
    
    [Test]
    public void ShouldThrowAnErrorWhenTooShortNameIsBeingPassedInRegisterUserRequest()
    {
        var request = new RegisterUserRequest
        {
            Name = "A",
            Email = "validEmail@gmail.com",
            Password = "StrongPassword123",
            Surname = "ValidSurname"
        };
        var validation = UserValidations.ValidateCreateUserRequest(request);
        validation.IsValid.ShouldBeFalse();
    }
    
    [TestCase("!JohnDoe")]
    [TestCase("@JaneDoe")]
    public void ShouldThrowAnErrorWhenInvalidNameIsBeingPassedInRegisterUserRequest(string name)
    {
        var request = new RegisterUserRequest
        {
            Name = name,
            Email = "validEmail@gmail.com",
            Password = "StrongPassword123",
            Surname = "ValidSurname"
        };
        var validation = UserValidations.ValidateCreateUserRequest(request);
        validation.IsValid.ShouldBeFalse();
    }
    
    [Test]
    public void ShouldThrowAnErrorWhenTooLongEmailIsBeingPassedInRegisterUserRequest()
    {
        var email = new string('A', 256);
        var request = new RegisterUserRequest
        {
            Name = "ValidName",
            Email = email + "@gmail.com",
            Password = "StrongPassword123",
            Surname = "ValidSurname"
        };
        var validation = UserValidations.ValidateCreateUserRequest(request);
        
        validation.IsValid.ShouldBeFalse();
    }
    
    [TestCase("!dachi@gmail.com")]
    [TestCase("@gmail.com")]
    [TestCase("")]
    [TestCase("dachi@gmail")]
    [TestCase("dachi@.com")]
    [TestCase("dachigmail.com")]
    [TestCase("12dachigmail.com")]
    public void ShouldThrowAnErrorWhenInvalidEmailIsBeingPassedInRegisterUserRequest(string email)
    {
        var request = new RegisterUserRequest
        {
            Name = "ValidName",
            Email = email,
            Password = "StrongPassword123",
            Surname = "ValidSurname"
        };
        var validation = UserValidations.ValidateCreateUserRequest(request);
        
        validation.IsValid.ShouldBeFalse();
    }

    [TestCase("ValidSurname")]
    public void ShouldPassWhenRegisterUserRequestHasValidSurname(string surname)
    {
        var request = new RegisterUserRequest
        {
            Name = "ValidName",
            Email = "ValidEmail@gmail.com",
            Password = "StrongPassword123",
            Surname = surname
        };
        var validation = UserValidations.ValidateCreateUserRequest(request);
        
        validation.IsValid.ShouldBeTrue();
    }

    [TestCase("")]
    [TestCase("!Surname")]
    [TestCase("Surnam!e")]
    [TestCase("Surname12")]
    public void ShouldBeFalseWhenInvalidSurnameIsBeingPassed(string surname)
    {
        var request = new RegisterUserRequest
        {
            Name = "ValidName",
            Email = "ValidEmail@gmail.com",
            Password = "StrongPassword123",
            Surname = surname
        };
        var validation = UserValidations.ValidateCreateUserRequest(request);
        
        validation.IsValid.ShouldBeFalse();
    }

    [Test]
    public void ShouldBeTrueWhenPassingValidEmailToLoginRequestValidator()
    {
        var request = new LoginRequest
        {
            Email = "ValidEmail@gmail.com"
        };

        var validation = UserValidations.ValidateLoginRequest(request);
        
        validation.IsValid.ShouldBeTrue();
    }    
    
    [TestCase("!dachi@gmail.com")]
    [TestCase("@gmail.com")]
    [TestCase("")]
    [TestCase("dachi@gmail")]
    [TestCase("dachi@.com")]
    [TestCase("dachigmail.com")]
    public void ShouldBeFalseWhenPassingInvalidEmailToLoginRequestValidator(string email)
    {
        var request = new LoginRequest
        {
            Email = email
        };

        var validation = UserValidations.ValidateLoginRequest(request);
        
        validation.IsValid.ShouldBeFalse();
    }
}