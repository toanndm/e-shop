using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecification(string? sort, int? brandId, int? typeId)
            : base(p =>
                (!brandId.HasValue || p.ProductBrandId == brandId)
                && (!typeId.HasValue || p.ProductTypeId == typeId)
            )
        {
            AddInclude(x => x.ProductType!);
            AddInclude(x => x.ProductBrand!);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDes(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name!);
                        break;
                }
            }
        }

        public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(x => x.ProductType!);
            AddInclude(x => x.ProductBrand!);
        }
    }
}