using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderezeImagePanel
{
    public partial class Default : System.Web.UI.Page
    {
        ImageService imgsrv = new ImageService();
        //List<OrderezeTask.Image> imagesList = new List<OrderezeTask.Image>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateFileList();
            }
        }

        protected void uploadImage(object sender, EventArgs e)
        {
            OrderezeTask.Image newimage = new OrderezeTask.Image();

            if (!string.IsNullOrWhiteSpace(textboxName.Text))
                newimage.Name = textboxName.Text;
            else
                newimage.Name = FileUploadControl.PostedFile.FileName;

            //newimage.Id = Guid.NewGuid().GetHashCode();
            newimage.Description = textboxDescription.Text;
            newimage.ImagePath = FileUploadControl.PostedFile.FileName;

            imgsrv.AddNewImage(newimage);
            //imagesList.Add(newimage);

            textboxName.Text = "";
            textboxDescription.Text = "";
            UpdateFileList();
        }

        private void UpdateFileList()
        {
            //OrderezeTask.Image image = new OrderezeTask.Image();
            //List<ListItem> imagelist = new List<ListItem>();

            //foreach (OrderezeTask.Image image in imgsrv.GetImages())
            //{
            //    imagelist.Add(new ListItem(image.Name, image.Id.ToString()));
            //}

            //fileView.DataSource = imagelist;
            //fileView.DataBind();

            fileView.DataSource = imgsrv.GetImages();
            fileView.DataBind();

        }

//        protected void DeleteFile(object sender, EventArgs e)
//       {
//            int imageid = int.Parse((sender as LinkButton).CommandArgument);
//            imgsrv.DeleteImage(imageid);
//            Response.Redirect(Request.Url.AbsoluteUri);
//        }


        protected void DownloadFile(object sender, EventArgs e)
        {
            string fileuriPath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileuriPath);
            Response.WriteFile(fileuriPath);
            Response.End();
        }


        protected void RowCommandHandler(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteImageById")
            {
                imgsrv.DeleteImage(int.Parse(e.CommandArgument.ToString()));
            }

            // Update the UI
            UpdateFileList();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int test = int.Parse((sender as LinkButton).Text); // .CommandArgument;
            imgsrv.DeleteImage(int.Parse((sender as LinkButton).Text));
        }
    }
}