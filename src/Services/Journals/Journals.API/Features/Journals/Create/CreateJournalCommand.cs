using FastEndpoints;
using FluentValidation;

namespace Journals.API.Features.Journals.Create;

public sealed record CreateJournalCommand(
    string Name,
    string Abbreviation,
    string Description,
    string ISSN,
    int ChiefEditorId
);

public class CreateJournalCommandValidator : Validator<CreateJournalCommand>
{
    public CreateJournalCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Abbreviation).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.ISSN).NotEmpty().Matches(@"^\d{4}-\d{3}[\dX]$").WithMessage("Invalid ISSN format");
        RuleFor(c => c.ChiefEditorId).GreaterThan(0);
    }
}