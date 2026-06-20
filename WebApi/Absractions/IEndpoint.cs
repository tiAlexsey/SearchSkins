namespace WebApi.Absractions;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}