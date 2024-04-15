namespace EuroPredApi.Models;

public class User {
    public int Id { get; set;}
    public string Username { get; set;}
    public string Email {get; set;}
    public byte[] PasswordHash { get; set;}
    public byte[] PasswordSalt {get; set;}
    public ICollection<PlayerPrediction> PlayerPredictions { get; set;}
    public ICollection<TeamPrediction> TeamPredictions { get; set;}
    public ICollection<TournamentPrediction> TournamentPredictions { get; set;}
    public int? TeamId {get; set;}
    public Team? Team {get; set;}

    public void CreatePasswordHash(string password) {
        
        using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
            
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt) {

        using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt)) {

            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++) {

                if (computedHash[i] != storedHash[i]) return false;
            }

            return true;
        }
    }
}