using EuroPredApi.DTOs;
using EuroPredApi.Models;

public class UpdateUserDTO
{
    public string? Username { get; set; }
    public string? FavouriteTeam { get; set; }
    public TournamentPredictionUpdateDTO? TournamentPredictions { get; set; }
    public PlayerPredictionUpdateDTO? PlayerPredictions { get; set; }
    public TournamentPredictionUpdateDTO? TeamPredictions { get; set; }
}

public class UpdateUserProfileDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public NationalTeam? FavouriteTeam { get; set; }
}

public class ChangePasswordDTO
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
public class UpdateProfilePictureDTO
{
    public string ProfilePicRef { get; set; }
}

public class AddCommentDTO
{
    public int AuthorId { get; set; }
    public string Recipient { get; set; }
    public string Text { get; set; }
}

public class CommentsDTO
{
    public string Author { get; set; }
    public string Recipient { get; set; }
    public DateTime Timestamp { get; set;}
    public string Comment { get; set; }
}

public class SendInviteDTO
{
    public int TeamId { get; set; }
    public string Recipient { get; set; }

}
