using DataAccess.Models;
using Services.Abstract;
using Services.AdoNet.Helpers.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Services.AdoNet
{
    public class ProductService : ProductQueries, IProductService
    {
        public Task<bool> CreateAsync(Product data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(GetProductsByCategoryId(categoryId), sqlConnection))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            Product product = new Product
                            {
                                ID = Convert.ToInt32(dataRow["ID"]),
                                Name = dataRow["Name"].ToString(),
                                Description = dataRow["Description"].ToString(),
                                ProducerName = dataRow["ProducerName"].ToString(),
                                SubcategoryName = dataRow["SubcategoryName"].ToString(),
                                Price = Convert.ToDecimal(dataRow["Price"]),
                                DiscountPrice = Convert.ToDecimal(dataRow["DiscountPrice"])
                            };
                            products.Add(product);
                        }
                    }

                    await sqlConnection.CloseAsync();
                }
            }
            catch (Exception exp)
            {

            }
            return products;
        }

        public Task<bool> UpdateAsync(Product data)
        {
            throw new NotImplementedException();
        }
    }
}
