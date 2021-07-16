﻿using CleanArchMVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMVC.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetByIdAsync(int? id);

        //Task<Product> GetProductCategoryAsync(int? id);

        Task<Category> CreateAsync(Category category);
        Task<Category> RemoveAsync(Category category);
        Task<Category> UpdateAsync(Category category);
    }
}
