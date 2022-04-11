using System;
using System.Collections.Generic;
using System.Text;

namespace AllStore.Dal.AdoNet.Queries
{
    public class SubCategoryQueries : DbConnection
    {
        protected string GetAllSubCategoriesQuery { get { return "SELECT * FROM SUBCATEGORIES"; } }

        protected string GetSubCategoryByIdQuery(int id)
        {
            return $@"SELECT * FROM SUBCATEGORIES ROLES WHERE ID = {id}";
        }

        protected string CreateSubCategoryQuery
        {
            get
            {
                return $@"INSERT INTO SUBCATEGORIES(Name) VALUES(@name) WHERE CategoryId=@categoryId";
            }
        }

        protected string DeleteSubCategoryQuery
        {
            get
            {
                return $@"DELETE FROM SUBCATEGORIES WHERE ID=@id";
            }
        }

        protected string GetSubcategoryByCategoryIdQuery(int categoryId)
        {
            return $@"SELECT * FROM SUBCATEGORIES WHERE CategoryId={categoryId}";
        }
    }
}
