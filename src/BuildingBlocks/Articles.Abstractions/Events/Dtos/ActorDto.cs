using Articles.Abstractions.Enums;

namespace Articles.Abstractions.Events.Dtos;

public sealed record ActorDto(
    UserRoleType Role,
    HashSet<ContributionArea>  ContributionAreas,
    PersonDto  Person);