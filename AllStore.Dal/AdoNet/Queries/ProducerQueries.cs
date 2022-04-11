using System;
using System.Collections.Generic;
using System.Text;

namespace AllStore.Dal.AdoNet.Queries
{
    public class ProducerQueries : DbConnection
    {
        protected string GetAllProducersQuery { get { return "SELECT * FROM PRODUCERS"; } }

        protected string GetProducerByIdQuery(int id)
        {
            return $@"SELECT * FROM PRODUCERS WHERE ID = {id}";
        }

        protected string CreateProducerQuery
        {
            get
            {
                return $@"INSERT INTO PRODUCERS(Name) VALUES(@name)";
            }
        }

        protected string DeleteProducerQuery
        {
            get
            {
                return $@"DELETE FROM PRODUCERS WHERE ID=@id";
            }
        }
    }
}
