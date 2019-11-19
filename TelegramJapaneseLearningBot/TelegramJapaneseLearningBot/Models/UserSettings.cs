using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramJapaneseLearningBot.Models
{
    public class UserSettings
    {
        [Key]
        public string UserId { get; set; }

        public User User { get; set; }

        public bool IsSpeechTraining { get; set; }

        public bool IsTextTraining { get; set; }

        public TimeSpan Interval { get; set; }
    }
}
