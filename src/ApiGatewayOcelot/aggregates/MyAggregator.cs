using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ApiGatewayOcelot.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace ApiGatewayOcelot.aggregates;

public class MyAggregator : IDefinedAggregator
{

    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        var posts = await responses[0].Items.DownstreamResponse().Content.ReadFromJsonAsync<List<PostDto>>();
        var comments = await responses[1].Items.DownstreamResponse().Content.ReadFromJsonAsync<List<CommentDto>>();

        posts?.ForEach(post =>
        {
            post.comments.AddRange(comments?.Where(a => a.PostId == post.Id));
        });

        var jsonString = JsonConvert.SerializeObject(
           posts, Formatting.Indented,
           new JsonConverter[] { new StringEnumConverter() });

        var stringContent = new StringContent(jsonString)
        {
            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
        };

        return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
    }
}
