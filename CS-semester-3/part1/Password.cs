using System.Text.RegularExpressions;

namespace CS_semester_3; 


public partial class Password {
    private readonly string _password;
    
    public override bool Equals(object? obj) {
        if (obj is Password p) return _password == p._password;
        return false;
    }
    
    public override int GetHashCode() {
        return _password.GetHashCode();
    }

    public override string ToString() {
        return _password;
    }

    public Password(string password) {
        _password = password;
    }

    public static Password operator -(Password p, char c) {
        return new Password(p._password.Remove(p._password.Length - 1, 1) + c);
    }
    
    public static bool operator >(Password p1, Password p2) {
        return p1._password.Length > p2._password.Length;
    }
    
    public static bool operator <(Password p1, Password p2) {
        return p1._password.Length < p2._password.Length;
    }
    
    public static bool operator ==(Password p1, Password p2) {
        return p1._password == p2._password;
    }
    
    public static bool operator !=(Password p1, Password p2) {
        return p1._password == p2._password;
    }
    
    public static Password operator ++(Password _) {
        return new Password("Qwerty123");
    }
    
    [GeneratedRegex("[0-9]")]
    private static partial Regex HasNumbers();
    
    [GeneratedRegex("[A-ZА-Я]")]
    private static partial Regex HasUppercase();
    
    public static bool operator true(Password p) {
        return HasNumbers().Match(p._password).Success
               && HasUppercase().Match(p._password).Success;
    }
    
    public static bool operator false(Password p) {
        return !HasNumbers().Match(p._password).Success
               || !HasUppercase().Match(p._password).Success;
    }
}