using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramJapaneseLearningBot.DBContext.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class LearningUser
    {
        public int LearningUserId { get; set; }

        public string Username { get; set; }

        public virtual LearningUserSettings LearningUserSettings { get; set; }
    }
}