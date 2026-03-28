using Articles.Abstractions;
using Auth.Domain.Persons.ValueObjects;
using Blocks.Domain.Entities;

namespace Auth.Domain.Persons;

public partial class Person : IEntity
{
    public static Person Create(IPersonCreationInfo personInfo)
    {
        var person = new Person
        {
            Email = personInfo.Email,
            FirstName = personInfo.FirstName,
            LastName = personInfo.LastName,
            Gender = personInfo.Gender,
            PictureUrl = personInfo.PictureUrl,
            Honorific = HonorificTitle.FromEnum(personInfo.Honorific),
            ProfessionalProfile = ProfessionalProfile.Create(personInfo.Position, personInfo.CompanyName, personInfo.Affiliation),
        };
        
        // todo - create domain event
        return person;
    }
}