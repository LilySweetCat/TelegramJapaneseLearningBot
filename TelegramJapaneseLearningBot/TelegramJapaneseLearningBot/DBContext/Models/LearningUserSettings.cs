using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramJapaneseLearningBot.DBContext.Models
{
    public class LearningUserSettings
    {
        [Key, ForeignKey("LearningUser")]
        public int LearningUserId { get; set; }

        public bool IsSpeechTraining { get; set; }

        public bool IsTextTraining { get; set; }

        public TimeSpan Interval { get; set; }

        public virtual LearningUser LearningUser { get; set; }
    }
}