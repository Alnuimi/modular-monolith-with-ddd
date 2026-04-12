using System;
using System.Security.Cryptography;
using Blocks.Core.Security;
using Blocks.Domain.ValueObjects;

namespace Review.Domain.Invitations.ValueObjects;

public class InvitationToken : StringValueObject
{
    private InvitationToken(string value) => Value = value;

    public static InvitationToken CreateNew() 
    {
        return new InvitationToken(Base64UrlTokenGenerator.Generate());
    }

   
}
