using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DocumentManagement.Models;
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
    public class FileHandler
    {
        private readonly IConfiguration configuration;
        private readonly string blobConnectionString;

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
            await blobClient.UploadAsync(uploadFileStream, true);
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

        //public async Task<IEnumerable<string>> ListFiles(ApplicationUser applicationUser)
        //{
        //    List<string> blobNames = new List<string>();
        //    BlobContainerClient containerClient = new BlobContainerClient(blobConnectionString, applicationUser.ContainerGuid);
        //    await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
        //    {
        //        //Console.WriteLine("\t" + blobItem.Name);
        //        blobNames.Add(blobItem.Name);
        //    }
        //    return blobNames;
        //}

        public void DownloadFile()
        {

        }
    }
}
