using Corelibs.Basic.Auth;
using Corelibs.Basic.Repository;
using FluentValidation;
using System.Security.Claims;

namespace Corelibs.Basic.UseCases;

public abstract class UserRequestValidator<TRequest> : AbstractValidator<TRequest>
{
    public UserRequestValidator(
        IAccessorAsync<ClaimsPrincipal> userAccessor)
    {
        RuleFor(person => userAccessor)
            .MustAsync(ValidateUserAccessorAsync).WithMessage("A user must be authorized.");
    }

    private async Task<bool> ValidateUserAccessorAsync(
        IAccessorAsync<ClaimsPrincipal> userAccessor, CancellationToken cancellationToken)
    {
        var guidString = await userAccessor.GetUserID();
        return Guid.TryParse(guidString, out _);
    }
}
