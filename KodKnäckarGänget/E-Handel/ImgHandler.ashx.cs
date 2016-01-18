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
            if (productIdString != null)
            {
                int productId;
                if (int.TryParse(productIdString, out productId))
                    GetProductImage(context, productId);
            }

        }

        private static void GetProductImage(HttpContext context, int productId)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand($"SELECT Image FROM Products WHERE ID = {productId}", connection);
            SqlDataReader reader = null;
            byte[] buffer = null;
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string extensionName = "jpg";
                    buffer = (byte[])reader["Image"];
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