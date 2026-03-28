namespace Journals.API.Features.Shared;

public sealed record JournalDto(
    int Id,
    string Abbreviation,
    string Name,
    string Description,
    string ISSN)
{
    public EditorDto Editor { get; set; } = null!;
}