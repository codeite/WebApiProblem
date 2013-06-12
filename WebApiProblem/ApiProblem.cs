namespace WebApiProblem
{
    public interface ApiProblem
    {
        string ProblemTypeUrl { get; }
        string Title { get; }
        string Detail { get; }
    }
}