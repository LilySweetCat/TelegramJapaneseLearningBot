using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramJapaneseLearningBot.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        [Key]
        public string UserId { get; set; }

        public UserSettings UserSettings { get; set; }
    }
}
