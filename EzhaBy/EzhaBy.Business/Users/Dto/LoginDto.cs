using System;

namespace EzhaBy.Business.Users.Dto
{
    public class LoginDto
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }
    }
}
