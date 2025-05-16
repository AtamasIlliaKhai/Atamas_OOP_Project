using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Atamas_OOP_Project.Models
{
    [JsonObject(IsReference = false)]
    public abstract class User
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [JsonProperty("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; } = new DateTime(2000, 1, 1);

        [JsonProperty("phone")]
        public string Phone { get; set; } = "+380000000000";

        [JsonIgnore]
        public List<Notification> Notifications { get; set; } = new();

        protected User() { }

        public virtual bool Authenticate(string email, string password)
        {
            return Email.Equals(email, StringComparison.OrdinalIgnoreCase)
                && PasswordHash == password;
        }
    }
}