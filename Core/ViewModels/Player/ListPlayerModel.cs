using Infrastructure.Models.Enums;

namespace Core.ViewModels.Player
{
    public class ListPlayerModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public Position Position { get; set; }
    }
}
