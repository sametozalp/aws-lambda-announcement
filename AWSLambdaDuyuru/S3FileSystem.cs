using Amazon;
using Amazon.S3.Model;
using Amazon.S3;

namespace AWSLambdaDuyuru
{
    public class S3FileSystem
    {
        // AWS kimlik bilgilerinizi ayarlayın veya IAM rollerini kullanın
        String awsAccessKey;
        String awsSecretKey;
        RegionEndpoint region;
        String bucketName;
        String keyName;
        // Yeni içerik
        //String newContent;
        AmazonS3Client client;
        PutObjectRequest request;

        public S3FileSystem()
        {
            awsAccessKey = "";
            awsSecretKey = "";
            region = RegionEndpoint.USEast1; // S3 bölgesini değiştirin
            client = new AmazonS3Client(awsAccessKey, awsSecretKey, region);
            bucketName = "files3s";
            keyName = "config.txt"; // S3'deki dosyanın adı
        }

        public async Task changeDataAsync(string duyuruIcerik)
        {
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = keyName,
                ContentBody = duyuruIcerik
            };

            try
            {
                // Dosyan�n i�eri�ini g�ncelleyin
                var response = await client.PutObjectAsync(request);
                Console.WriteLine("Dosya i�eri�i ba�ar�yla g�ncellendi.");
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Hata Olu�tu. Hata Kodu: {ex.ErrorCode}, Hata Mesaj�: {ex.Message}");
            }
        }
        
        public async Task readDataAsync()
        {
            // S3 nesnesinin içeriğini okumak için istek oluşturun
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = keyName
            };

            try
            {
                // S3 nesnesinin içeriğini okuyun
                using var response = await client.GetObjectAsync(request);

                // İçeriği bir akıştan okuyun
                using var responseStream = response.ResponseStream;
                using var reader = new StreamReader(responseStream);

                // Veriyi okuyun ve konsola yazın
                var content = reader.ReadToEnd();
                Function.readedAWSFileContent = content;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Hata Oluştu. Hata Kodu: {ex.ErrorCode}, Hata Mesajı: {ex.Message}");
            }
        }
    }
}
