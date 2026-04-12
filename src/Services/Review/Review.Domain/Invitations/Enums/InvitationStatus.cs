namespace Review.Domain.Invitations.Enums;

public enum InvitationStatus
{
    Open,
    Accepted,
    Declined,
    Expired // todo - create a background job to mark invitations as expired 
}
