public class User {
    public int id { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }

    public User() {}

    public User(string email, string password) {
        this.email = email;
        this.password = password;
    }
    public User(int id, string email, string password) {
        this.id = id;
        this.email = email;
        this.password = password;
    }

    public override string ToString() {
        return $"Id: {id}, Email: {email}, Password: {password}";
    }
}