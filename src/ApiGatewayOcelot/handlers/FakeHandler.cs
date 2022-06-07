namespace ApiGatewayOcelot.handlers;

public class FakeHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //do stuff and optionally call the base handler..
        return await base.SendAsync(request, cancellationToken);
    }
}
