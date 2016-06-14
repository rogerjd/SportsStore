using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Interface
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
