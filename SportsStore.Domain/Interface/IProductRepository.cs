﻿using System.Linq;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Interface
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);
    }
}
