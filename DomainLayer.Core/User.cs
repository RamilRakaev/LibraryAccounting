using Domain.Interfaces;

namespace Domain.Model
{
    public class User: IElement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public void Accept(IVisitor<User> visitor)
        {
            visitor.Visit(this);
        }
    }
}
