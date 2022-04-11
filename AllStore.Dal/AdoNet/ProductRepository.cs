using AllStore.Dal.AdoNet.Queries;
using AllStore.Domain.Abstractions.Repositories;
using AllStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Dal.AdoNet
{
    public class ProductRepository : ProductQueries, IProductRepository
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

        public async Task<Product> GetByIdAsync(int id)
        {
            Product product = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(GetProductById(id), sqlConnection))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            product = new Product
                            {
                                ID = Convert.ToInt32(dataRow["ID"]),
                                Name = dataRow["Name"].ToString(),
                                Description = dataRow["Description"].ToString(),
                                ProducerName = dataRow["ProducerName"].ToString(),
                                CompanyName = dataRow["CompanyName"].ToString(),
                                SubcategoryName = dataRow["SubcategoryName"].ToString(),
                                Price = Convert.ToDecimal(dataRow["Price"]),
                                DiscountPrice = Convert.ToDecimal(dataRow["DiscountPrice"]),
                                Image = dataRow["Image"].ToString()
                            };
                        }
                    }

                    await sqlConnection.CloseAsync();
                }
            }
            catch (Exception exp)
            {

            }
            return product;
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
                                CompanyName = dataRow["CompanyName"].ToString(),
                                SubcategoryName = dataRow["SubcategoryName"].ToString(),
                                Price = Convert.ToDecimal(dataRow["Price"]),
                                DiscountPrice = Convert.ToDecimal(dataRow["DiscountPrice"]),
                                Image = dataRow["Image"].ToString()
                            };
                            products.Add(product);
                        }
                    }

                    await sqlConnection.CloseAsync();
                }
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsBySubcategoryIdAsync(int subcategoryId)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(GetProductsBySubcategoryId(subcategoryId), sqlConnection))
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
                                CompanyName = dataRow["CompanyName"].ToString(),
                                SubcategoryName = dataRow["SubcategoryName"].ToString(),
                                Price = Convert.ToDecimal(dataRow["Price"]),
                                DiscountPrice = Convert.ToDecimal(dataRow["DiscountPrice"]),
                                Image = dataRow["Image"].ToString()
                            };
                            products.Add(product);
                        }
                    }

                    await sqlConnection.CloseAsync();
                }
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

            return products;
        }


        public Task<bool> UpdateAsync(Product data)
        {
            throw new NotImplementedException();
        }
    }
}
