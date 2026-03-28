using Journals.Domain.Journals.Enums;

namespace Journals.API.Features.Shared;

public sealed record EditorDto(
    int Id,
    string FullName,
    string Affiliation,
    EditorRole Role = EditorRole.ChiefEditor);