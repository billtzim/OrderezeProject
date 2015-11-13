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
        DatabaseStorageFunctions dsf = new DatabaseStorageFunctions();

        public int AddNewImage(Image image)
        {
            image.ImagePath = bsf.uploadFileToBlob(image);
            image.Id = dsf.insertImageToDb(image);

            return image.Id;
        }

        public void DeleteImage(int id)
        {
            bsf.deleteBlobFile(dsf.deleteImagefromDB(id));
        }

        public List<Image> GetImages()
        {
            //var blobclient = bsf.blobClientConnect("StorageConnectionString");
            //var blobcontainer = bsf.blobGetContainerRef(blobclient, "imagecontainer");


            //return bsf.getBlobFiles();
            return dsf.getImagesFromDB();
        }
    }
}