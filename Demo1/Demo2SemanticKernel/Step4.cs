//using Microsoft.SemanticKernel;
//using Microsoft.SemanticKernel.ChatCompletion;
//using Microsoft.SemanticKernel.Connectors.Google;
//using Microsoft.SemanticKernel.Connectors.OpenAI;


//IChatCompletionService service = new OpenAIChatCompletionService("gpt-4o-mini", Environment.GetEnvironmentVariable("OpenAI:ApiKey"));
//ChatHistory history = new ChatHistory();

////history.AddUserMessage("Stephan is 42 years old");

//while (true)
//{
//    Console.Write("Q:");
//    history.AddUserMessage(Console.ReadLine());

//    var assistant = await service.GetChatMessageContentAsync(history);

//    history.Add(assistant);
//    Console.WriteLine(assistant.Content);
//}