using Microsoft.ML;
using Microsoft.ML.Data;

namespace MLNET
{
    public class TransportData
    {
        public float Distance { get; set; }
        public Single Passengers { get; set; }
        public float Price { get; set; }
    }

    public class TransportPricePrediction
    {
        [ColumnName("Score")]
        public float Price { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create MLContext
            var mlContext = new MLContext();

            // Sample data
            var data = new List<TransportData>
            {
                new TransportData { Distance = 5, Passengers = 1, Price = 2.5f },
                new TransportData { Distance = 10, Passengers = 1, Price = 5.0f },
                new TransportData { Distance = 20, Passengers = 2, Price = 10.0f },
                new TransportData { Distance = 15, Passengers = 2, Price = 7.5f },
                new TransportData { Distance = 25, Passengers = 3, Price = 12.5f },
            };

            // Convert data to IDataView
            var trainingDataView = mlContext.Data.LoadFromEnumerable(data);

            // Define training pipeline
            var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Distance", "Passengers" })
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));

            // Train the model
            var model = pipeline.Fit(trainingDataView);

            // Create prediction engine
            var pricePredictionEngine = mlContext.Model.CreatePredictionEngine<TransportData, TransportPricePrediction>(model);

            // Sample prediction
            var sampleInput = new TransportData { Distance = 12, Passengers = 2 };
            var prediction = pricePredictionEngine.Predict(sampleInput);

            Console.WriteLine($"Predicted price for {sampleInput.Distance} km and {sampleInput.Passengers} passengers: {prediction.Price:C2}");
        }
    }
}
