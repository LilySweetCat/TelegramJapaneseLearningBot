using System;
using System.ComponentModel.DataAnnotations;

namespace TelegramJapaneseLearningBot.Models
{
    public class UserSettings
    {
        [Key] public int UserId { get; set; }

        public User User { get; set; }

        public bool IsSpeechTraining { get; set; }

        public bool IsTextTraining { get; set; }

        public TimeSpan Interval { get; set; }
    }
}