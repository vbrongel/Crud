﻿namespace Crud.Core.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        public User()
        {
            
        }

    }
}
