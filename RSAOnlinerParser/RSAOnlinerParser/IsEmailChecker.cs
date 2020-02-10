using System.ComponentModel.DataAnnotations;

namespace RSAOnlinerParser
{
    public static class IsEmailChecker
    {
        public static bool IsEmail(string emailAddress)
        {
            return new EmailAddressAttribute().IsValid(emailAddress);
        }
    }
}
