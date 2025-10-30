using OpenAI.Chat;

namespace Taller.Services;

public class AiService(IConfiguration configuration)
{
    public async Task<string> GetTasksSummaryAsync(List<string> descriptions)
    {
        if (descriptions.Count == 0)
            return "";

        var apiKey = configuration["OpenAI:ApiKey"];
        var model = configuration["OpenAI:Model"];

        var client = new ChatClient(model, apiKey);

        var prompt = $"Generate a concise summary of the following task descriptions:\n\n{string.Join("\n", descriptions)}";

        var completion = await client.CompleteChatAsync(prompt);

        return completion.Value.Content[0].Text;
    }
}
