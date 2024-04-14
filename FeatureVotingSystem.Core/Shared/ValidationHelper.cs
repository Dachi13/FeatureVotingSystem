using System.Text.RegularExpressions;

namespace FeatureVotingSystem.Core.Shared;

public static class ValidationHelper
{
    public static bool BeValidName(this string name)
    {
        var namePattern = @"^[A-Za-z][A-Za-z0-9_\s]+$";

        return Regex.IsMatch(name, namePattern);
    }
    
    public static bool BeAValidEmail(this string email)
    {
        var emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        
        return Regex.IsMatch(email, emailPattern);
    }
}