namespace DataLayer.Data
{
    public class ExternalServiceUser
    {
        public int Id { get; set; }
        public int ExternalServiceId { get; set; }
        public int UserId { get; set; }
        public string? ClientId { get; set; }
        public string? State { get; set; }
        public DateTimeOffset? ExternalTokenExpiration { get; set; }
        public virtual Token? AccessToken { get; set; } = null!;
        public virtual Token? RefreshToken { get; set; } = null!;
        public virtual ExternalService ExternalService { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}