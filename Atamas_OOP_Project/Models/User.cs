using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atamas_OOP_Project.Models
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public List<Notification> Notifications { get; set; } = new();

        public virtual bool Authenticate(string email, string password)
        {
            return Email == email && PasswordHash == password;
        }

        public void AddNotification(string title, string message)
        {
            Notifications.Add(new Notification
            {
                Title = title,
                Message = message,
                CreatedAt = DateTime.Now,
                IsRead = false,
                UserId = this.Id
            });
        }
    }
}