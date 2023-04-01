using System.ComponentModel.DataAnnotations;

namespace AuctionSystem.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}