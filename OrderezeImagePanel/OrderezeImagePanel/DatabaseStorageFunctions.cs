using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderezeImagePanel
{
    public class DatabaseStorageFunctions
    {
        string connstring = ConfigurationManager.AppSettings["dbconnstring"].ToString();

        public int insertImageToDb(OrderezeTask.Image image)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                SqlCommand CmdSql = new SqlCommand("INSERT INTO [imagesTable] (Name, Description,imagepath) VALUES (@Name, @Description, @Imagepath);SELECT SCOPE_IDENTITY();", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Name", image.Name);
                CmdSql.Parameters.AddWithValue("@Description", image.Description);
                CmdSql.Parameters.AddWithValue("@Imagepath", image.ImagePath);

                object newimageid = CmdSql.ExecuteScalar();
                conn.Close();

                return int.Parse(newimageid.ToString());
            }
        }

        public string deleteImagefromDB(int id)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                SqlCommand CmdSql = new SqlCommand("SELECT imagepath from [imagesTable] where id=@ID",conn);
                CmdSql.Connection = conn;
                conn.Open();
                CmdSql.Parameters.AddWithValue("@ID", id);
                CmdSql.ExecuteNonQuery();

                SqlDataReader myReader = CmdSql.ExecuteReader();

                myReader.Read();
                string imagepath = myReader["imagepath"].ToString();

                CmdSql.CommandText = "DELETE FROM [imagesTable] WHERE ID=@ID;";
                myReader.Close();
                CmdSql.ExecuteNonQuery();

                conn.Close();

                return imagepath;
            }
        }


        public List<OrderezeTask.Image> getImagesFromDB()
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [imagesTable]", conn))
                {
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        List<OrderezeTask.Image> imagesdata = new List<OrderezeTask.Image>();
                        while (sdr.Read())
                        {
                            imagesdata.Add(new OrderezeTask.Image());
                            imagesdata[imagesdata.Count - 1].Id = int.Parse(sdr["id"].ToString());
                            imagesdata[imagesdata.Count - 1].Name = sdr["name"].ToString();
                            imagesdata[imagesdata.Count - 1].Description = sdr["description"].ToString();
                            imagesdata[imagesdata.Count - 1].ImagePath = sdr["imagepath"].ToString();
                        }
                        conn.Close();
                        return imagesdata;
                    }
                }
            }
        }

    }
}