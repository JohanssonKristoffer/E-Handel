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
        public int AccountId { get; set; }

        public BLOrder(double postage, double totalPrice, string address, string postalCode, string city, string country, string email, string telephone, string paymentOptions, string deliveryOptions, string name, string surname, List<BLCartProduct> cartProducts, int accountId = -1)
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
            AccountId = accountId;
        }
        public BLOrder(string databaseConnectionString, int id)
        {
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlGetOrder = new SqlCommand($"SELECT AccountID, TotalPrice, Postage, Address, PostalCode, City, Country, Email, Telephone, PaymentOptions, DeliveryOptions, Name, Surname FROM Orders WHERE ID = {id}", sqlConnection);
            SqlCommand sqlGetOrderProducts = new SqlCommand($"SELECT ProductID, Quantity FROM OrderProducts WHERE OrderID = {id}", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlConnection.Open();

                sqlReader = sqlGetOrder.ExecuteReader();
                while (sqlReader.Read())
                {
                    AccountId = -1;
                    int accountId;
                    if (int.TryParse(sqlReader["AccountID"].ToString(), out accountId))
                        AccountId = accountId;
                    Id = id;
                    Postage = double.Parse(sqlReader["Postage"].ToString());
                    TotalPrice = double.Parse(sqlReader["TotalPrice"].ToString());
                    Address = sqlReader["Address"].ToString();
                    PostalCode = sqlReader["PostalCode"].ToString();
                    City = sqlReader["City"].ToString(); ;
                    Country = sqlReader["Country"].ToString(); ;
                    Email = sqlReader["Email"].ToString(); ;
                    Telephone = sqlReader["Telephone"].ToString(); ;
                    PaymentOptions = sqlReader["PaymentOptions"].ToString(); ;
                    DeliveryOptions = sqlReader["DeliveryOptions"].ToString(); ;
                    Name = sqlReader["Name"].ToString(); ;
                    Surname = sqlReader["Surname"].ToString(); ;
                }
                sqlReader.Close();
                sqlReader.Dispose();

                CartProducts = new List<BLCartProduct>();
                sqlReader = sqlGetOrderProducts.ExecuteReader();
                while (sqlReader.Read())
                {
                    CartProducts.Add(new BLCartProduct(id: int.Parse(sqlReader["ProductID"].ToString()), quantity: int.Parse(sqlReader["Quantity"].ToString())));
                }
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                    sqlReader.Dispose();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlGetOrder.Dispose();
            }
        }

        public void InsertIntoDB(string databaseConnectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            SqlCommand sqlInsertOrder = null;
            SqlCommand sqlInsertOrderProduct = null;
            if (AccountId < 0)
                sqlInsertOrder =
                    new SqlCommand(
                        "INSERT INTO Orders (Postage,TotalPrice,Address,PostalCode,City,Country,Email,Telephone,PaymentOptions,DeliveryOptions,Name,Surname)OUTPUT INSERTED.ID VALUES (@postage, @totalPrice, @address, @postalCode, @city, @country, @email,@telephone, @paymentoptions, @deliveryoptions, @name, @surname)",
                        sqlConnection);
            else
                sqlInsertOrder =
                    new SqlCommand(
                        "INSERT INTO Orders (AccountID,Postage,TotalPrice,Address,PostalCode,City,Country,Email,Telephone,PaymentOptions,DeliveryOptions,Name,Surname)OUTPUT INSERTED.ID VALUES (@accountID,@postage, @totalPrice, @address, @postalCode, @city, @country, @email,@telephone, @paymentoptions, @deliveryoptions, @name, @surname)",
                        sqlConnection);
            try
            {
                sqlConnection.Open();

                if (AccountId >= 0)
                    sqlInsertOrder.Parameters.Add("@accountID", SqlDbType.Int).Value = AccountId;
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

        public override string ToString() => $"Id = {Id}, Postage = {Postage}, TotalPrice = {TotalPrice}, Address = {Address}, PostalCode = {PostalCode}, City = {City}, Country = {Country}, Email = {Email}, Telephone = {Telephone}, PaymentOptions = {PaymentOptions}, DeliveryOptions = {DeliveryOptions}, Name = {Name}, Surname = {Surname}";
    }
}