namespace client.Services;

public class MapboxService : IMapboxService
{
    private readonly IConfiguration _configuration;

    public MapboxService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetToken()
    {
        return _configuration.GetValue<string>("MapboxToken");
    }
}
