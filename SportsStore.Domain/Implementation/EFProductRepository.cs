using SportsStore.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;

namespace SportsStore.Domain.Implementation
{
    public class EFProductRepository : IProductRepository
    {
        EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }
    }
}
