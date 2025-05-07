using OpenAI;
using System.ClientModel;

OpenAIClient client = new OpenAIClient(new ApiKeyCredential(Environment.GetEnvironmentVariable("OpenAI:ApiKey")));
var chat = client.GetChatClient("gpt-4o-mini");


await foreach (var update in chat.CompleteChatStreamingAsync("What is the color of water?"))
{
    foreach (var item in update.ContentUpdate)
    {
        Console.Write(item.Text);
    }
}