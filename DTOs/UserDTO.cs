﻿using Playmor_Asp.Enums;

namespace Playmor_Asp.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole UserRole { get; set; }
    }
}
