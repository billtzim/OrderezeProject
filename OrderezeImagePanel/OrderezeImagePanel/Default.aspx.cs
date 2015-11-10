using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderezeImagePanel
{
    public partial class Default : System.Web.UI.Page
    {
        ImageService imgsrv = new ImageService();
        List<OrderezeTask.Image> imagesList = new List<OrderezeTask.Image>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateFileList();
            }
        }

        protected void uploadBlobImage(object sender, EventArgs e)
        {
            OrderezeTask.Image newimage = new OrderezeTask.Image();

            if (!string.IsNullOrWhiteSpace(textboxName.Text))
                newimage.Name = textboxName.Text;
            else
                newimage.Name = FileUploadControl.PostedFile.FileName;

            newimage.Id = Guid.NewGuid().GetHashCode();
            newimage.Description = textboxDescription.Text;
            newimage.ImagePath = FileUploadControl.PostedFile.FileName;

            imgsrv.AddNewImage(ref newimage);
            //imagesList.Add(newimage);

            textboxName.Text = "";
            textboxDescription.Text = "";
        }

        private void UpdateFileList()
        {
            //OrderezeTask.Image image = new OrderezeTask.Image();
            List<ListItem> imagelist = new List<ListItem>();

            foreach (OrderezeTask.Image image in imgsrv.GetImages())
            {
                imagelist.Add(new ListItem(image.Name, image.Id.ToString()));
            }

            fileView.DataSource = imagelist;
            fileView.DataBind();

        }

        protected void RowCommandHandler(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                imgsrv.DeleteImage(int.Parse(e.CommandArgument.ToString()));
            }

            // Update the UI
            UpdateFileList();
        }
    }
}