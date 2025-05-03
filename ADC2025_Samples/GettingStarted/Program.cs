using System.ClientModel;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OpenAI;

//OpenAIClient client = new OpenAIClient(new ApiKeyCredential(Environment.GetEnvironmentVariable("OpenAI:ApiKey")));
//var chatService = client.GetChatClient("gpt-4o-mini");

//var result = await chatService.CompleteChatAsync("What is the color of water?");
//Console.WriteLine(result.Value);

//await foreach (var update in chatService.CompleteChatStreamingAsync("What is the color of the sky?"))
//{
//    foreach (var item in update.ContentUpdate)
//    {
//        Console.Write(item);
//    }
//} 

// Uninstall OpenAI Package

//IChatCompletionService chatService = new OpenAIChatCompletionService("gpt-4o-mini", Environment.GetEnvironmentVariable("OpenAI:ApiKey")); // Semantic Kernel Abstractions + OepnAI Connector
//IChatCompletionService chatService = new GoogleAIGeminiChatCompletionService("gemini-pro", Environment.GetEnvironmentVariable("Gemini:ApiKey")); // Semantic Kernel Abstractions + OepnAI Connector
//IChatCompletionService chatService = new MistralAIChatcompletionService("mistral-small", Environment.GetEnvironmentVariable("Mistral:ApiKey")); // Semantic Kernel Abstractions + OepnAI Connector
//var result = await chatService.GetChatMessageContentsAsync("What is the color of water?");
//Console.WriteLine(result);

// Chatbot
//IChatCompletionService chatService = new OpenAIChatCompletionService("gpt-4o-mini", Environment.GetEnvironmentVariable("OpenAI:ApiKey")); // Semantic Kernel Abstractions + OepnAI Connector

//ChatHistory history = new ();
//history.AddUserMessage("Stephan ist 42 years old");
//while (true)
//{
//    Console.Write("Q:  ");
//    history.AddUserMessage(Console.ReadLine());

//    var assistant = await chatService.GetChatMessageContentsAsync(history);
//    history.AddRange(assistant);
//    Console.WriteLine(assistant);
//}

// Name
// Alter


// Bestehende Application // DI

ServiceCollection c = new();
c.AddOpenAIChatCompletion("gpt-4o-mini", Environment.GetEnvironmentVariable("OpenAI:ApiKey"));
IServiceProvider serviceProvider = c.BuildServiceProvider();

IChatCompletionService chatService = serviceProvider.GetRequiredService<IChatCompletionService>();

Kernel kernel = serviceProvider.GetRequiredService<Kernel>();
kernel.ImportPluginFromType<Data>();


PromptExecutionSettings settings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

ChatHistory history = new();
while (true)
{
    Console.WriteLine("Q: ");
    history.AddUserMessage(Console.ReadLine());

    var assistant = await chatService.GetChatMessageContentsAsync(history, settings, kernel);
    history.AddRange(assistant);
    Console.WriteLine(assistant);

}

// How old ist Stephan
// How much older is Peter than Paul?


class Data
{
    public int GetAge(string name)
    {
        return name switch
        {
            "Stephan" => 42,
            "Peter" => 25,
            "Paul" => 30
        };
    }
}

