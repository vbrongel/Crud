namespace Crud.Core.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Address { get; private set; }

        public User()
        {
            
        }
        public User(string name, string email, DateTime dateOfBirth, string address)
        {
            Name = name;
            Email = email;
            DateOfBirth = dateOfBirth;
            Address = address;
        }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

    }
}
