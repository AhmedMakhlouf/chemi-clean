using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ChemiCleanFiles.Models;
using System.CommandLine;
using System.CommandLine.Invocation;
using Microsoft.Extensions.Configuration;


namespace ChemiCleanFiles
{
    class Program
    {
        /// <summary>
		/// Refresh the DB
		/// </summary>
		/// <param name="outputDirPath"> The output path</param>
        static async Task Main(string outputDirPath)
        {
            if (Directory.Exists(outputDirPath) == false)
            {
                outputDirPath = AppDomain.CurrentDomain.BaseDirectory;
            }
            
            Console.WriteLine("Output directory: {0}\n", outputDirPath);

            await Load(outputDirPath);
        }
        public static async Task Load(string outputDirPath)
        {
            using masterContext context = new masterContext();
            var products = context.TblProducts;

            foreach (var product in products)
            {
                try
                {
                    using HttpClient client = new HttpClient();
                    var url = product.Url;

                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine(response.Content.Headers.ContentType.MediaType);

                    byte[] byteArray = await response.Content.ReadAsByteArrayAsync();

                    using SHA256 sha256 = SHA256.Create();

                    var hashBytes = sha256.ComputeHash(byteArray);
                    var hash = System.Text.Encoding.Default.GetString(hashBytes);

                    if (hash == product.ContentHash)
                        continue;

                    var date = DateTime.Now;

                    var path = Path.Combine(
                        outputDirPath,
                        product.ProductName,
                        date.ToString("yyyy-dd-M--HH-mm-ss")
                        );


                    var extension = string.Empty;

                    if (response.Content.Headers.ContentType.MediaType == "application/pdf")
                        extension = ".pdf";
                    else if (response.Content.Headers.ContentType.MediaType == "text/html")
                        extension = ".html";


                    var directory = Directory.CreateDirectory(path);
                    var uri = Path.Combine("Data", product.ProductName, date.ToString("yyyy-dd-M--HH-mm-ss"), "data" + extension);
                    

                    Console.WriteLine($"URI: {uri}");

                    product.Uri = uri;
                    product.ContentHash = hash;
                    product.Updated = date;

                    context.Update(product);

                    await File.WriteAllBytesAsync(Path.Combine(directory.FullName, "data" + extension), byteArray);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
