using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderezeTask;
using System.IO;

namespace OrderezeImagePanel
{
    public class ImageService : OrderezeTask.IImagesService
    {
        BlobStorageFunctions bsf = new BlobStorageFunctions();

        public int AddNewImage(ref Image image)
        {
            var blobclient = bsf.blobClientConnect("StorageConnectionString");
            var blobcontainer = bsf.blobGetContainerRef(blobclient, "imagecontainer");
            var blob = bsf.blobGetBlobRef(blobcontainer, Path.GetFileName(image.ImagePath));


            string imageContentType = System.Web.MimeMapping.GetMimeMapping(image.ImagePath);

            using (var fileStream = System.IO.File.OpenRead(image.ImagePath))
            {
                blob.UploadFromStream(fileStream);
            }

            bsf.addBlobMetadata(blob, "id", image.Id.ToString());
            bsf.addBlobMetadata(blob, "name", image.Name);
            bsf.addBlobMetadata(blob, "description", image.Description);
            blob.SetMetadata();

            blob.Properties.ContentType = imageContentType;
            blob.SetProperties();

            image.ImagePath = blob.Uri.AbsolutePath;

            return image.Id;
        }

        public void DeleteImage(int id)
        {
            var blobclient = bsf.blobClientConnect("StorageConnectionString");
            var blobcontainer = bsf.blobGetContainerRef(blobclient, "imagecontainer");
            var blob = bsf.blobGetBlobRef(blobcontainer, id.ToString());

            blob.DeleteIfExists();
        }

        public List<Image> GetImages()
        {
            var blobclient = bsf.blobClientConnect("StorageConnectionString");
            var blobcontainer = bsf.blobGetContainerRef(blobclient, "imagecontainer");

            List<Image> imageList = new List<Image>();

            foreach (var blobItem in blobcontainer.ListBlobs())
            {
                var aBlob = bsf.blobGetBlobRef(blobcontainer, blobItem.Uri.AbsoluteUri);
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
    }
}