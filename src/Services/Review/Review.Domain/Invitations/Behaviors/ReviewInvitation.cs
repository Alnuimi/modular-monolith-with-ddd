using System;
using Blocks.Domain;
using Review.Domain.Invitations.Enums;

namespace Review.Domain.Invitations;

public partial class ReviewInvitation
{
  
    public void Accept()
    {
        if(Status != InvitationStatus.Open)
        {
            throw new DomainException("Invitation is not open any more.");
        }
        if(IsExpired)
        {
            throw new DomainException("Invitation expired.");
        }

          // todo - consider adding an InvitationAccepted domain event
        Status = InvitationStatus.Accepted;
    }

    public void Decline()
    {
        
        if(Status != InvitationStatus.Open)
        {
            throw new DomainException("Invitation is not open any more.");
        }

        // todo - consider adding an InvitationDeclined domain event
        Status = InvitationStatus.Declined;
    }
}
