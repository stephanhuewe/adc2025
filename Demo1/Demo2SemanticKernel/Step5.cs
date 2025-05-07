//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.SemanticKernel;
//using Microsoft.SemanticKernel.ChatCompletion;
//using Microsoft.SemanticKernel.Connectors.Google;
//using Microsoft.SemanticKernel.Connectors.OpenAI;
//using System.ComponentModel;

//ChatHistory history = new ChatHistory();

//ServiceCollection c = new ServiceCollection();
//c.AddOpenAIChatCompletion("gpt-4o-mini", Environment.GetEnvironmentVariable("OpenAI:ApiKey"));
//c.AddKernel();
//IServiceProvider serviceProvider = c.BuildServiceProvider();

//Kernel kernel = serviceProvider.GetService<Kernel>();
//kernel.ImportPluginFromType<UserData>();

//PromptExecutionSettings settings = new OpenAIPromptExecutionSettings()
//{
//    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
//};

//IChatCompletionService chatservice = kernel.GetRequiredService<IChatCompletionService>();


//while (true)
//{
//    Console.Write("Q:");
//    history.AddUserMessage(Console.ReadLine());

//    var assistant = await chatservice.GetChatMessageContentAsync(history, settings, kernel);

//    history.Add(assistant);
//    Console.WriteLine(assistant.Content);
//}

//class UserData
//{
//    [KernelFunction]
//    [Description("Get Age of Persons")]
//    public int GetAge(string name)
//    {
//        return name switch
//        {
//            "Stephan" => 42,
//            "Peter" => 25,
//            "Paul" => 30
//        };
//    }
//}