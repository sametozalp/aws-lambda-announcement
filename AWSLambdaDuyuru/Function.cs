using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Amazon;
using Amazon.S3.Model;
using Amazon.S3;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambdaDuyuru;

public class Function
{
    public static string readedAWSFileContent = "";
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public void FunctionHandler(ILambdaContext context)
    {
        S3FileSystem s3 = new S3FileSystem();
        s3.readDataAsync();
        Thread.Sleep(4000); // işlemin bitmesini bekle
        
        Data data = new Data();
        data.getData();
        data.sendData(s3);
    }
}
