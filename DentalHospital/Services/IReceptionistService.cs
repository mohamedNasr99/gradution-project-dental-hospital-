namespace DentalHospital.Services
{
    public interface IReceptionistService
    {
        bool CheckPay(string code);
        Task<string?> CheckCode(string name);
    }
}