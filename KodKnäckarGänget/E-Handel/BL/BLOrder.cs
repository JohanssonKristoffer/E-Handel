using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace E_Handel.BL
{
    public class BLOrder
    {
        public int Id { get; private set; }
        public double Postage { get; set; }
        public double TotalPrice { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string PaymentOptions { get; set; }
        public string DeliveryOptions { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<BLCartProduct> CartProducts { get; set; }

        public BLOrder(double postage, double totalPrice, string address, string postalCode, string city, string country, string email, string telephone, string paymentOptions, string deliveryOptions, string name, string surname, List<BLCartProduct> cartProducts)
        {
            Id = -1; //Not assigned until inserted into the database
            Postage = postage;
            TotalPrice = totalPrice;
            Address = address;
            PostalCode = postalCode;
            City = city;
            Country = country;
            Email = email;
            Telephone = telephone;
            PaymentOptions = paymentOptions;
            DeliveryOptions = deliveryOptions;
            Name = name;
            Surname = surname;
            CartProducts = cartProducts;
        }

        public void InsertIntoDB(string databaseConnectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlInsertOrder = new SqlCommand("INSERT INTO Orders (Postage,TotalPrice,Address,PostalCode,City,Country,Email,Telephone,PaymentOptions,DeliveryOptions,Name,Surname)OUTPUT INSERTED.ID VALUES (@postage, @totalPrice, @address, @postalCode, @city, @country, @email,@telephone, @paymentoptions, @deliveryoptions, @name, @surname)", sqlConnection);
            SqlCommand sqlInsertOrderProduct = null;
            try
            {
                sqlConnection.Open();

                sqlInsertOrder.Parameters.Add("@postage", SqlDbType.Float).Value = Postage;
                sqlInsertOrder.Parameters.Add("@totalPrice", SqlDbType.Float).Value = TotalPrice;
                sqlInsertOrder.Parameters.Add("@address", SqlDbType.NVarChar, 255).Value = Address;
                sqlInsertOrder.Parameters.Add("@postalCode", SqlDbType.NVarChar, 50).Value = PostalCode;
                sqlInsertOrder.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value = City;
                sqlInsertOrder.Parameters.Add("@country", SqlDbType.NVarChar, 50).Value = Country;
                sqlInsertOrder.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = Email;
                sqlInsertOrder.Parameters.Add("@telephone", SqlDbType.NVarChar, 50).Value = Telephone;
                sqlInsertOrder.Parameters.Add("@paymentoptions", SqlDbType.NVarChar, 50).Value = PaymentOptions;
                sqlInsertOrder.Parameters.Add("@deliveryoptions", SqlDbType.NVarChar, 50).Value = DeliveryOptions;
                sqlInsertOrder.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = Name;
                sqlInsertOrder.Parameters.Add("@surname", SqlDbType.NVarChar, 50).Value = Surname;

                Id = (int)sqlInsertOrder.ExecuteScalar();

                foreach (BLCartProduct cartProduct in CartProducts)
                {
                    sqlInsertOrderProduct = new SqlCommand($"INSERT INTO OrderProducts (OrderID,ProductID,Quantity) VALUES ('{Id}','{cartProduct.Id}', '{cartProduct.Quantity}')", sqlConnection);
                    sqlInsertOrderProduct.ExecuteNonQuery();
                    sqlInsertOrder.Dispose();
                }
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlInsertOrder.Dispose();
                if (sqlInsertOrderProduct != null)
                    sqlInsertOrderProduct.Dispose();
            }
        }
    }
}