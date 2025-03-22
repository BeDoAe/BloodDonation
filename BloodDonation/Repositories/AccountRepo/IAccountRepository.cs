namespace BloodDonation.Repositories.AccountRepo
{
    public interface IAccountRepository
    {
        public Task<bool> UserExists(string username, string email);

    }
}