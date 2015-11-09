using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

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

    }
}