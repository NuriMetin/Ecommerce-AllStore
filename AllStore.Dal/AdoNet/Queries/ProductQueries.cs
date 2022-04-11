using System;
using System.Collections.Generic;
using System.Text;

namespace AllStore.Dal.AdoNet.Queries
{
	public class ProductQueries : DbConnection
	{
		protected string GetProductsByCategoryId(int categoryId)
		{
			return $@"
					SELECT TOP(10)  
							 TAR.ID
							,TAR.Name
							,TAR.Description
							,TAR.SubcategoryName
							,TAR.ProducerName
							,TAR.CompanyName
							,TAR.Price
							,TAR.DiscountPrice
							,MAX(TAR.Image) Image
					FROM 

					(SELECT PRDCT.ID
							,PRDCT.Name
							,ISNULL(PRDCT.Description,'') Description
							,SUB_CAT.Name SubcategoryName
							,PROD.Name ProducerName
							,COMP.Name CompanyName
							,PRDCT.Price
							,ISNULL(PRDCT.DiscountPrice,0) DiscountPrice
							,ISNULL(IMG.ImagePath,0) Image
					FROM PRODUCTS PRDCT
					LEFT JOIN SUBCATEGORIES SUB_CAT ON PRDCT.SubcategoryId=SUB_CAT.ID
					LEFT JOIN PRODUCERS PROD ON PROD.ID=PRDCT.ProducerId
					LEFT JOIN COMPANIES COMP ON COMP.ID=PRDCT.CompanyId
					LEFT JOIN IMAGES IMG ON IMG.ProductId=PRDCT.ID
					WHERE SUB_CAT.CategoryId={categoryId}) tar

					GROUP BY 
							 TAR.ID
							,TAR.Name
							,TAR.Description
							,TAR.SubcategoryName
							,TAR.ProducerName
							,TAR.CompanyName
							,TAR.Price
							,TAR.DiscountPrice";
		}

		protected string GetProductsBySubcategoryId(int subcategoryId)
		{
			return $@"
					SELECT TOP(10)  
							 TAR.ID
							,TAR.Name
							,TAR.Description
							,TAR.SubcategoryName
							,TAR.ProducerName
							,TAR.CompanyName
							,TAR.Price
							,TAR.DiscountPrice
							,MAX(TAR.Image) Image
					FROM 

					(SELECT PRDCT.ID
							,PRDCT.Name
							,ISNULL(PRDCT.Description,'') Description
							,SUB_CAT.Name SubcategoryName
							,PROD.Name ProducerName
							,COMP.Name CompanyName
							,PRDCT.Price
							,ISNULL(PRDCT.DiscountPrice,0) DiscountPrice
							,ISNULL(IMG.ImagePath,0) Image
					FROM PRODUCTS PRDCT
					LEFT JOIN SUBCATEGORIES SUB_CAT ON PRDCT.SubcategoryId=SUB_CAT.ID
					LEFT JOIN PRODUCERS PROD ON PROD.ID=PRDCT.ProducerId
					LEFT JOIN COMPANIES COMP ON COMP.ID=PRDCT.CompanyId
					LEFT JOIN IMAGES IMG ON IMG.ProductId=PRDCT.ID
					WHERE SUB_CAT.ID={subcategoryId}) tar

					GROUP BY 
							 TAR.ID
							,TAR.Name
							,TAR.Description
							,TAR.SubcategoryName
							,TAR.ProducerName
							,TAR.CompanyName
							,TAR.Price
							,TAR.DiscountPrice";
		}

		protected string GetProductById(int subcategoryId)
		{
			return $@"
					SELECT TOP(10)  
							 TAR.ID
							,TAR.Name
							,TAR.Description
							,TAR.SubcategoryName
							,TAR.ProducerName
							,TAR.CompanyName
							,TAR.Price
							,TAR.DiscountPrice
							,MAX(TAR.Image) Image
					FROM 

					(SELECT PRDCT.ID
							,PRDCT.Name
							,ISNULL(PRDCT.Description,'') Description
							,SUB_CAT.Name SubcategoryName
							,PROD.Name ProducerName
							,COMP.Name CompanyName
							,PRDCT.Price
							,ISNULL(PRDCT.DiscountPrice,0) DiscountPrice
							,ISNULL(IMG.ImagePath,0) Image
					FROM PRODUCTS PRDCT
					LEFT JOIN SUBCATEGORIES SUB_CAT ON PRDCT.SubcategoryId=SUB_CAT.ID
					LEFT JOIN PRODUCERS PROD ON PROD.ID=PRDCT.ProducerId
					LEFT JOIN COMPANIES COMP ON COMP.ID=PRDCT.CompanyId
					LEFT JOIN IMAGES IMG ON IMG.ProductId=PRDCT.ID
					WHERE PRDCT.ID={subcategoryId}) tar

					GROUP BY 
							 TAR.ID
							,TAR.Name
							,TAR.Description
							,TAR.SubcategoryName
							,TAR.ProducerName
							,TAR.CompanyName
							,TAR.Price
							,TAR.DiscountPrice";
		}
	}
}
