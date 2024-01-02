namespace QuickJob.Cabinet.BusinessLogic.Managers.Factors;

public interface IFactorsManager
{
    Task InitSetUserEmail(string email);
    Task ConfirmSetUserEmail(string email, string code);
}