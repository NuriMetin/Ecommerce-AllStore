using System;
using System.Collections.Generic;
using System.Text;

namespace Services.AdoNet.Helpers.Queries
{
    public class CategoryQueries : DbConnection
    {
        protected string GetAllCategoriesQuery { get { return "SELECT * FROM CATEGORIES"; } }

        protected string GetCategoryByIdQuery(int id)
        {
            return $@"SELECT * FROM CATEGORIES WHERE ID = {id}";
        }

        protected string CreateCategoryQuery
        {
            get
            {
                return $@"INSERT INTO CATEGORIES(Name) VALUES(@name)";
            }
        }

        protected string DeleteCategoryQuery
        {
            get
            {
                return $@"DELETE FROM CATEGORIES WHERE ID=@id";
            }
        }
    }
}
