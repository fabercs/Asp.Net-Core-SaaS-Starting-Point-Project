using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EMSApp.Infrastructure.Data
{
    public class MutliLangErrorDescriber : IdentityErrorDescriber
    {
        private readonly ILocalizationService _L;

        public MutliLangErrorDescriber(ILocalizationService localizationService)
        {
            _L = localizationService;
        }
        public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = _L.GetLocalizedValue($"An unknown failure has occurred.") }; }
        public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = _L.GetLocalizedValue("Optimistic concurrency failure, object has been modified.") }; }
        public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = _L.GetLocalizedValue("Incorrect password.") }; }
        public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = _L.GetLocalizedValue("Invalid token.") }; }
        public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = _L.GetLocalizedValue("A user with this login already exists.") }; }
        public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = _L["User name {0} is invalid, can only contain letters or digits.", userName] }; }
        public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = _L["Email {0} is invalid.", email] }; }
        public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = _L["User Name {0} is already taken.", userName] }; }
        public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = _L["Email {0} is already taken.", email] }; }
        public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = _L["Role name {0} is invalid.", role] }; }
        public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = _L["Role name {0} is already taken.", role] }; }
        public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = _L.GetLocalizedValue("User already has a password set.") }; }
        public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = _L.GetLocalizedValue("Lockout is not enabled for this user.") }; }
        public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = _L["User already in role {0}.", role] }; }
        public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = _L["User is not in role {0}.", role] }; }
        public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = _L["Passwords must be at least {length} characters.", length] }; }
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = _L.GetLocalizedValue("Passwords must have at least one non alphanumeric character.") }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = _L.GetLocalizedValue("Passwords must have at least one digit ('0'-'9').") }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = _L.GetLocalizedValue("Passwords must have at least one lowercase ('a'-'z').") }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = _L.GetLocalizedValue("Passwords must have at least one uppercase ('A'-'Z').") }; }
    }

}
