using Articles.Abstractions.Enums;

namespace Articles.IntegrationEvents.Contracts.Dtos;

public sealed record ActorDto(
    UserRoleType Role,
    HashSet<ContributionArea>  ContributionAreas,
    PersonDto  Person);