using FastEndpoints;
using FluentValidation;
using Journals.API.Features.Shared;
using Journals.Domain.Journals;

namespace Journals.API.Features.Journals.Search;

public sealed record SearchJournalsQuery(string? Search, int Page = 1, int PageSize = 20);

public sealed record SearchJournalsResponse(int Page, int PageSize, int TotalCount, IEnumerable<JournalDto> Journals);

public class SearchJournalsQueryValidator : Validator<SearchJournalsQuery>
{
    public SearchJournalsQueryValidator()
    {
        RuleFor(s => s.Page).GreaterThan(0);
        RuleFor(s => s.PageSize).InclusiveBetween(5, 100);
    }
}