using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using DocumentManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BlobStorage
{
    /// <summary>
    /// Class containing the methods to maniplulate files
    /// </summary>
    [Authorize]
    public class FileHandler
    {
        private readonly IConfiguration configuration;
        private readonly string blobConnectionString;
        private const int SasAccountLifetimeInMinutes = 10;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="configuration"></param>
        public FileHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.blobConnectionString = configuration.GetConnectionString("StorageConnectionString");
        }

        /// <summary>
        /// Creates a container for a given user
        /// </summary>
        public async Task CreateContainer(ApplicationUser appUser)
        {
            //used to create a container client
            //var blobServiceClient = new BlobServiceClient(connectionString);

            var containerClient = new BlobContainerClient(blobConnectionString, appUser.ContainerGuid);
            await containerClient.CreateIfNotExistsAsync();
        }

        /// <summary>
        /// Uploads a given file (blob) to a user's container
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        public async Task UploadFile(string filepath, ApplicationUser applicationUser)
        {
            ////get reference to storage account
            //BlobServiceClient blobServiceClient = new BlobServiceClient(configuration.GetConnectionString("StorageConnectionString"));
            ////crate and get the reference to a container
            //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync("filehandler2");
            await CreateContainer(applicationUser);

            var containerClient = new BlobContainerClient(blobConnectionString, applicationUser.ContainerGuid);
 
            //specify the blob name
            BlobClient blobClient = containerClient.GetBlobClient(Path.GetFileName(filepath));
            

            //upload files
            using FileStream uploadFileStream = File.OpenRead(filepath);
            await blobClient.UploadAsync(uploadFileStream, new BlobHttpHeaders { ContentType = Helper.GetFileType(filepath) }, conditions: null);
            uploadFileStream.Close();
        }

        /// <summary>
        /// Lists all the files(blobs) for a given user
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        public async Task<List<string>> ListFiles(ApplicationUser applicationUser)
        {
            List<string> blobNames = new List<string>();
            BlobContainerClient containerClient = new BlobContainerClient(blobConnectionString, applicationUser.ContainerGuid);
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                //Console.WriteLine("\t" + blobItem.Name);
                blobNames.Add(blobItem.Name);
            }
            return blobNames;
        }

        public void DownloadFile()
        {

        }

        public string GenerateBlobSasUrl(ApplicationUser applicationUser, string blobName)
        {
            BlobContainerClient containerClient = new(blobConnectionString, applicationUser.ContainerGuid);
            BlobSasBuilder blobSasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = containerClient.Name,
                BlobName = blobName,
                Resource = "b", //b = blob, c = container
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(SasAccountLifetimeInMinutes)
            };

            blobSasBuilder.SetPermissions(BlobSasPermissions.Read);

            StorageSharedKeyCredential storageSharedKeyCredential = new StorageSharedKeyCredential(containerClient.AccountName, configuration["ConnectionStrings:AccountKey"]);

            string sasToken = blobSasBuilder.ToSasQueryParameters(storageSharedKeyCredential).ToString();
            string sasUrl = Helper.GetBlobSasUrl(containerClient.AccountName, applicationUser.ContainerGuid, blobName, sasToken);

            return sasUrl;
        }
    }
}
