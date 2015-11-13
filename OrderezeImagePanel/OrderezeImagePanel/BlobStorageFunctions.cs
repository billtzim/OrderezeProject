using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using OrderezeTask;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace OrderezeImagePanel
{
    public class BlobStorageFunctions
    {
        public CloudBlobClient blobClientConnect(string connstring)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings[connstring].ToString());
            return storageAccount.CreateCloudBlobClient();
        }

        public CloudBlobContainer blobGetContainerRef(CloudBlobClient blobclient, string containerName)
        {
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobclient.GetContainerReference(containerName);

            container.CreateIfNotExists();
            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Container
                });

            return container;
        }

        public CloudBlockBlob blobGetBlobRef(CloudBlobContainer container, string blobname)
        {
            // Retrieve reference to a blob named
            return container.GetBlockBlobReference(blobname);
        }

        public void setPublicPermissions(CloudBlobContainer container, BlobContainerPublicAccessType Level)
        {
            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = Level
                });
        }

        public void addBlobMetadata(CloudBlockBlob blob, string metadataKey, string metadataValue)
        {
            blob.Metadata.Add(metadataKey, metadataValue);
        }

        public string uploadFileToBlob(OrderezeTask.Image imageforupload)
        {
            var blobcontainer = blobGetContainerRef(blobClientConnect("StorageConnectionString"), "imagecontainer");
            var blob = blobGetBlobRef(blobcontainer, Guid.NewGuid().ToString()+Path.GetExtension(imageforupload.ImagePath));

            using (var fileStream = System.IO.File.OpenRead(imageforupload.ImagePath))
            {
                blob.UploadFromStream(fileStream);
            }

            blob.Properties.ContentType = System.Web.MimeMapping.GetMimeMapping(imageforupload.ImagePath);
            blob.SetProperties();

            return blob.Uri.ToString();
        }

        public List<Image> getBlobFiles()
        {
            List<Image> imageList = new List<Image>();
            var blobcontainer = blobGetContainerRef(blobClientConnect("StorageConnectionString"), "imagecontainer");
            
            foreach (var blobItem in blobcontainer.ListBlobs())
            {
                var aBlob = blobGetBlobRef(blobcontainer, blobItem.Uri.AbsoluteUri);
                aBlob.FetchAttributes();

                imageList.Add(new Image()
                {
                    Id = int.Parse(aBlob.Metadata["id"]),
                    Name = aBlob.Metadata["name"],
                    Description = aBlob.Metadata["description"],
                    ImagePath = aBlob.Uri.AbsoluteUri
                });
            }

            return imageList;

        }

        public void deleteBlobFile(string bloburi)
        {
            var blobcontainer = blobGetContainerRef(blobClientConnect("StorageConnectionString"), "imagecontainer");
            var blob = blobGetBlobRef(blobcontainer, Path.GetFileName(bloburi));

            blob.DeleteIfExists();

        }
    }
}