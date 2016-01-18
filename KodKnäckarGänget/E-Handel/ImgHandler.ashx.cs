using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace E_Handel
{
    /// <summary>
    /// Summary description for ImgHandler
    /// </summary>
    public class ImgHandler : IHttpHandler
    {
        private static readonly string connectionString =
        ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;

        public void ProcessRequest(HttpContext context)
        {
            string productIdString = context.Request.QueryString["productId"];
            string productIdThumbString = context.Request.QueryString["productIdThumb"];
            string categoryIdString = context.Request.QueryString["categoryId"];

            if (productIdString != null)
            {
                int productId;
                if (int.TryParse(productIdString, out productId))
                    GetImage(context, productId, "Image", "Products");
            }
            else if (productIdThumbString != null)
            {
                int productIdThumb;
                if (int.TryParse(productIdThumbString, out productIdThumb))
                    GetImage(context, productIdThumb, "Thumbnail", "Products");
            }
            else if (categoryIdString != null)
            {
                int categoryId;
                if(int.TryParse(categoryIdString, out categoryId))
                    GetImage(context, categoryId, "Image", "Categories");
            }

        }
        private void GetImage(HttpContext context, int id, string columnName, string tableName)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand($"SELECT {columnName} FROM {tableName} WHERE ID = {id}", connection);
            SqlDataReader reader = null;
            byte[] buffer = null;
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string extensionName = "jpg";
                    buffer = (byte[])reader[columnName];
                    context.Response.Clear();
                    context.Response.ContentType = "image/" + extensionName;
                    context.Response.BinaryWrite(buffer);
                    context.Response.Flush();
                    context.Response.Close();
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}