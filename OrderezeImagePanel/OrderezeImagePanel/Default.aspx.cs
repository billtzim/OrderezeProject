using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderezeImagePanel
{
    public partial class Default : Page
    {
        ImageService imgsrv = new ImageService();

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

            newimage.Description = textboxDescription.Text;
            newimage.ImagePath = FileUploadControl.PostedFile.FileName;

            imgsrv.AddNewImage(newimage);

            textboxName.Text = "";
            textboxDescription.Text = "";
            UpdateFileList();
        }

        private void UpdateFileList()
        {
            fileView.DataSource = imgsrv.GetImages();
            fileView.DataBind();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int test = int.Parse((sender as LinkButton).Text);
            imgsrv.DeleteImage(int.Parse((sender as LinkButton).Text));
            UpdateFileList();
        }
    }
}