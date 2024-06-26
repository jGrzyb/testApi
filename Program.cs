﻿using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class Program {
    public static string token = "BRAK";
    public static HttpClient client = new HttpClient();

    public static void Main(string[] args) {
        GetToken(new User("u1", "p1")).Wait();

        User u2 = new User(1, "u1", "p1");
        putUser(1, u2).Wait();

        List<User> users = GetUsers().Result.ToList();
        users.ForEach(user => Console.WriteLine(user.ToString()));
    }

    public static async Task<string> GetToken(User user) {
        string json = JsonSerializer.Serialize(user);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5000/api/auth", data);
        if(response.IsSuccessStatusCode) {
            var content = await response.Content.ReadAsStringAsync();
            token = JsonSerializer.Deserialize<List<string>>(content)![0];
            Console.WriteLine("Token: YES");
            return token;
        }
        else {
            Console.WriteLine("Token: NO");
            return response.StatusCode.ToString();
        }
    }

    public static async Task<IEnumerable<User>> GetUsers() {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("http://localhost:5000/api/user");
        if(response.IsSuccessStatusCode) {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(content) ?? new List<User>();
        }
        else {
            return new List<User>();
        }
    }

    public static async Task putUser(int id, User user) {
        string json = JsonSerializer.Serialize(new {
            id = user.id,
            email = user.email,
            password = user.password
        });
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.PutAsync($"http://localhost:5000/api/user/{id}", data);
        if(response.IsSuccessStatusCode) {
            Console.WriteLine("User updated");
        }
        else {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(errorContent);
        }
    }

    public static async Task addUser(User user) {
        string json = JsonSerializer.Serialize(user);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.PostAsync("http://localhost:5000/api/user", data);
        if(response.IsSuccessStatusCode) {
            Console.WriteLine("User added");
        }
        else {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(errorContent);
        }
    }

}