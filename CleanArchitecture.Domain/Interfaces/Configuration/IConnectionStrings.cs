namespace CleanArchitecture.Domain.Interfaces.Configuration
{
    public interface IConnectionStrings
    {
        string ReaderConnection { get; set; }
        string WriterConnection { get; set; }
        string FootballLatteConnection { get; set; }
    }
}
