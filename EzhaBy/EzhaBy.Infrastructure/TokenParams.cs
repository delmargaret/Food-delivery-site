namespace EzhaBy.Infrastructure
{
    public class TokenParams
    {
        public const string Issuer = "AuthServer";
        public const string Audience = "AuthClient";
        public const string Key = "mysupersecret_secretkey!123";
        public const int Lifetime = 60; // in minutes
    }
}
