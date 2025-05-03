using Microsoft.ML;

var context = new MLContext();
var dataView =
 context.Data.LoadFromTextFile<SecurityLog>("log.csv", separatorChar: ',');

var pipeline =
context.Transforms.Concatenate("Features", "LoginAttempts", "FailedAttempts")
    .Append(context.AnomalyDetection.Trainers.RandomizedPca());

var model = pipeline.Fit(dataView);
Console.WriteLine("Security anomaly detection model trained successfully!");

public class SecurityLog
{
    public int LoginAttempts  { get; set; }
    public int FailedAttempts { get; set; }
    public string Username { get; set; }
}