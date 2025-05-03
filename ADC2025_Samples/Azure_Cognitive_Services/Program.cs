using Azure;
using Azure.AI.TextAnalytics;

var client = new TextAnalyticsClient(new Uri("https://xyz.cognitiveservices.azure.com"),
        new AzureKeyCredential("ABC"));

string text = "AI is amazing";
DocumentSentiment sentiment = client.AnalyzeSentiment(text);
Console.WriteLine($"Sentiment: {sentiment.Sentiment}");