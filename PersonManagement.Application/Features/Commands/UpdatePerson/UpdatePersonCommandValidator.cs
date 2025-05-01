using FluentValidation;
using PersonManagment.Application.Resources;

namespace PersonManagment.Application.Features.Commands.UpdatePerson;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Length(2, 50)
            .Matches(@"^[a-zA-Zა-ჰ]+$")
            .WithMessage(ErrorMessages.FirstNameValidation);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(2, 50)
            .Matches(@"^[a-zA-Zა-ჰ]+$")
            .WithMessage(ErrorMessages.LastNameValidation);

        RuleFor(x => x.GenderId)
            .IsInEnum()
            .WithMessage(ErrorMessages.InvalidGender);

        RuleFor(x => x.PersonalN)
            .NotEmpty()
            .Length(11)
            .Matches(@"^\d+$")
            .WithMessage(ErrorMessages.PersonalNumberValidation);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .Must(date => date <= DateTime.Now.AddYears(-18))
            .WithMessage(ErrorMessages.MinimumAge);
            
        RuleFor(x => x.CityId)
            .GreaterThan(0)
            .WithMessage(ErrorMessages.IdShouldBeGreaterThanZero);

        RuleForEach(x => x.Phones)
            .ChildRules(phone =>
            {
                phone.RuleFor(p => p.Value)
                    .NotEmpty()
                    .Length(4, 50)
                    .WithMessage(ErrorMessages.PhoneNumberValidation);

                phone.RuleFor(p => p.Type)
                    .IsInEnum()
                    .WithMessage(ErrorMessages.InvalidPhoneType);
            });
    }
}