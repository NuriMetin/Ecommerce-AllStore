using System;
using System.Collections.Generic;
using System.Text;

namespace Services.AdoNet.Helpers.Queries
{
    public class ProductQueries : DbConnection
    {
        protected string GetProductsByCategoryId(int categoryId)
        {
            return $@" SELECT ITMCOMP.ID ID
								,ITM.Name Name
								,ISNULL(ITM.Description,'') Description
								,SBCT.Name SubcategoryName
								,PR.Name ProducerName, ITMCOMP.Price
								,ISNULL(ITMCOMP.DiscountPrice,0) DiscountPrice FROM ITEMSCOMPANIES ITMCOMP
							INNER JOIN ITEMS ITM ON ITM.ID=ITMCOMP.ItemId
							INNER JOIN PRODUCERS PR ON PR.ID=ITM.ProducerId
							LEFT JOIN SUBCATEGORIES SBCT ON ITM.SubcategoryId=SBCT.ID
							INNER JOIN CATEGORIES CT ON CT.ID=SBCT.CategoryId
						WHERE CT.ID={categoryId}";
        }
    }
}
