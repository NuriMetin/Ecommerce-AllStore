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
    public class ProducerService : ProducerQueries, IProducerService
    {
        public async Task<bool> CreateAsync(Producer producer)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(CreateProducerQuery, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@name", producer.Name);

                        await sqlCommand.ExecuteNonQueryAsync();
                        await sqlCommand.DisposeAsync();
                    }

                    await sqlConnection.CloseAsync();
                }
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Producer>> GetAllAsync()
        {
            List<Producer> producers = new List<Producer>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(GetAllProducersQuery, sqlConnection))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            Producer producer = new Producer
                            {
                                ID = Convert.ToInt32(row["ID"]),
                                Name = row["Name"].ToString()
                            };
                            producers.Add(producer);
                        }
                    }

                    await sqlConnection.CloseAsync();
                }
                return producers;
            }
            catch (Exception exp)
            {
                return producers;
            }
        }

        public Task<Producer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Producer data)
        {
            throw new NotImplementedException();
        }
    }
}
