namespace WebApiProblem
{
    public interface ApiProblem
    {
        string ProblemTypeUrl { get; }
        string Title { get; }
        string Detail { get; }
        string HttpStatus { get; }
        string Details { get; }
        string ProblemInstance { get; }
    }
}