namespace WorkHive.Application.Abstraction
{
    public interface IEmployeeIdProvider
    {
        Task<string> NextIdAsync();
    }
}