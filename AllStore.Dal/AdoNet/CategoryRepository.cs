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
    public class CategoryRepository : CategoryQueries, ICategoryRepository
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public CategoryRepository()
        {
            _subCategoryRepository = new SubCategoryRepository();
        }

        public async Task<bool> CreateAsync(Category category)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(CreateCategoryQuery, sqlConnection))
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

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(DeleteCategoryQuery, sqlConnection))
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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            List<Category> categories = new List<Category>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(GetAllCategoriesQuery, sqlConnection))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            Category category = new Category
                            {
                                ID = Convert.ToInt32(row["ID"]),
                                Name = row["Name"].ToString()
                            };

                            category.SubCategories = await _subCategoryRepository.GetSubcategoriesByCategoryIdAsync(category.ID);

                            categories.Add(category);
                        }
                    }

                    await sqlConnection.CloseAsync();
                }
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            return categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            Category category = new Category();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand(GetCategoryByIdQuery(id), sqlConnection))
                    {
                        SqlDataReader dataReader = sqlCommand.ExecuteReader();
                        while (dataReader.Read())
                        {
                            category.ID = Convert.ToInt32(dataReader["ID"]);
                            category.Name = dataReader["Name"].ToString();
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

            return category;
        }

        public async Task<bool> UpdateAsync(Category category)
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
    }
}
