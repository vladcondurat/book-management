using MediatR;

namespace BookManagement.Models
{
    public class UpdateBookRequest : IRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}