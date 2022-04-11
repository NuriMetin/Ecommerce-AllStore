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
    public class SubCategoryRepository : SubCategoryQueries, ISubCategoryRepository
    {
        public async Task<bool> CreateAsync(SubCategory category)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(CreateSubCategoryQuery, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@name", category.Name);

                        await sqlCommand.ExecuteNonQueryAsync();
                        await sqlCommand.DisposeAsync();
                    }

                    await sqlConnection.CloseAsync();
                }
                return true;
            }

            catch (Exception exp)
            {
               throw new Exception(exp.Message);
            }
        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(GetAllSubCategoriesQuery, sqlConnection))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            SubCategory subCategory = new SubCategory
                            {
                                ID = Convert.ToInt32(row["ID"]),
                                Name = row["Name"].ToString(),
                                CategoryId = Convert.ToInt32(row["CategoryId"])
                            };
                            subCategories.Add(subCategory);
                        }
                    }

                    await sqlConnection.CloseAsync();
                }
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

            return subCategories;
        }

        public async Task<SubCategory> GetByIdAsync(int id)
        {
            SubCategory subCategory = new SubCategory();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand(GetSubCategoryByIdQuery(id), sqlConnection))
                    {
                        SqlDataReader dataReader = sqlCommand.ExecuteReader();
                        while (dataReader.Read())
                        {
                            subCategory.ID = Convert.ToInt32(dataReader["ID"]);
                            subCategory.Name = dataReader["Name"].ToString();
                        }
                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.CloseAsync();
                }
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

            return subCategory;
        }

        public async Task<bool> UpdateAsync(SubCategory category)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("udp_update_category", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        SqlParameter categoryId = new SqlParameter();
                        categoryId.ParameterName = "@ID";
                        categoryId.SqlDbType = SqlDbType.Int;
                        categoryId.Value = category.ID;

                        SqlParameter categoryName = new SqlParameter();
                        categoryName.ParameterName = "@Name";
                        categoryName.SqlDbType = SqlDbType.NVarChar;
                        categoryName.Value = category.Name;

                        SqlParameter categoryRow = new SqlParameter();
                        categoryRow.ParameterName = "@Row";
                        categoryRow.SqlDbType = SqlDbType.Int;

                        sqlCommand.Parameters.Add(categoryId);
                        sqlCommand.Parameters.Add(categoryName);
                        sqlCommand.Parameters.Add(categoryRow);
                        SqlDataReader dataReader = sqlCommand.ExecuteReader();
                        dataReader.Close();
                        sqlCommand.Dispose();
                    }
                    await sqlConnection.CloseAsync();
                }
                return true;
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(DeleteSubCategoryQuery, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@id", id);

                        await sqlCommand.ExecuteNonQueryAsync();
                        await sqlCommand.DisposeAsync();
                    }

                    await sqlConnection.CloseAsync();
                }
                return true;
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public async Task<List<SubCategory>> GetSubcategoriesByCategoryIdAsync(int categoryId)
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(GetSubcategoryByCategoryIdQuery(categoryId), sqlConnection))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            SubCategory subCategory = new SubCategory
                            {
                                ID = Convert.ToInt32(row["ID"]),
                                Name = row["Name"].ToString(),
                                CategoryId = Convert.ToInt32(row["CategoryId"])
                            };
                            subCategories.Add(subCategory);
                        }
                    }

                    await sqlConnection.CloseAsync();
                }
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

            return subCategories;
        }
    }
}
