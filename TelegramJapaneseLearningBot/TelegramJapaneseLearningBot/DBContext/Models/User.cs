using System.ComponentModel.DataAnnotations;

namespace TelegramJapaneseLearningBot.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        [Key] public int UserId { get; set; }

        public UserSettings UserSettings { get; set; }
    }
}