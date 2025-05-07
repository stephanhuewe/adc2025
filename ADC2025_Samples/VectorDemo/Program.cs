
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.InMemory;
using OpenAI.VectorStores;

#pragma warning disable SKEXP0020 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

public class Movie
{
    [VectorStoreRecordKey]
    public int MyKey { get; set; }

    [VectorStoreRecordData]
    public string Title { get; set; }

    [VectorStoreRecordVector(Dimensions: 384, DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float> Vector { get; set; }
}

public class Demo
{
    public static async Task Main()
    {
        Movie superHeroMovie = new Movie() { MyKey = 1, Title = "ADC Comedy 2025" };
        Movie superHeroMovie2 = new Movie() { MyKey = 2, Title = "ADC Titanic 2025" };
        Movie superHeroMovie3 = new Movie() { MyKey = 2, Title = "ADC Batman 2025" };

        List<Movie> movies = new List<Movie>
        {
            superHeroMovie,
            superHeroMovie2,
            superHeroMovie3
        };

       

        // Vector

        var store = new InMemoryVectorStore();
        var collection = store.GetCollection<int, Movie>("movies");
        var generator =  new OpenAIEmbeddingGenerator(new OpenAI.OpenAIClient(Environment.GetEnvironmentVariable("OpenAI:ApiKey")), "text-embedding-3-large");
        
        await collection.CreateCollectionIfNotExistsAsync();


        foreach (Movie movie in movies)
        {
            movie.Vector = await generator.GenerateEmbeddingVectorAsync(movie.Title);
            await collection.UpsertAsync(movie);
        }

        var searchOptions = new VectorSearchOptions()
        {
            Top = 1,
            VectorPropertyName = "Vector",
        };

        var search = "superhero movie";
        var vector = await generator.GenerateEmbeddingVectorAsync(search);
        var results = await collection.VectorizedSearchAsync(vector, searchOptions);

        await foreach (var result in results.Results)
        {
            Console.WriteLine(result.Record.Title);
        }
    }
}