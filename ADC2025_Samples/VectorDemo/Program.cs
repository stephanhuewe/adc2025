using System.ClientModel;
using System.Linq.Expressions;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.InMemory;
using OpenAI;
using OpenAI.Embeddings;


public class Movie
{
    [VectorStoreRecordKey]
    public int MyKey { get; set; }

    [VectorStoreRecordData]
    public string Title { get; set; }

    [VectorStoreRecordVector(Dimensions: 384, DistanceFunction = DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float> Vector { get; set; }
}

public class Demo
{
    public static async Task Run()
    {
        Movie superHeroMovie = new Movie() { MyKey = 1, Title = "ADC Hero 2025" };
        Movie superHeroMovie2 = new Movie() { MyKey = 2, Title = "ADC SuperHero 2025" };

        List<Movie> movies = new List<Movie>
        {
            superHeroMovie,
            superHeroMovie2
        };

        // Vector
        var store = new InMemoryVectorStore();
        var collection = store.GetCollection<int, Movie>("movies");

        var generator = new OpenAIEmbeddingGenerator("gpt-4o-mini");


        foreach (Movie movie in movies)
        {
            movie.Vector = await generator.GenerateEmbeddingVectorAsync(movie.Title);
            await collection.UpsertAsync(movie);
        }

        var search = "superhero movie";
        var vector = await generator.GenerateEmbeddingVectorAsync(search);
        var results = await collection.VectorizedSearchAsync(vector, new VectorSearchOptions { Top = 1, VectorPropertyName = "Vector" });

        await foreach (var result in results.Results)
        {
            Console.WriteLine(result.Record.Title);
        }
    }
}