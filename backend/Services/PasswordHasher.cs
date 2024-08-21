namespace EuroPredApi.Services;

public class PasswordHasher {
    
    private readonly int _workFactor;

    public PasswordHasher(int workFactor = 12) {
       
        _workFactor = workFactor;
    }

    public string HashPassword(string password) {

        if (password == null) {

            throw new ArgumentNullException("password");
        }
        return BCrypt.Net.BCrypt.HashPassword(password, _workFactor);
    }

    public bool VerifyPassword(string password, string storedHash) {

        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }
}