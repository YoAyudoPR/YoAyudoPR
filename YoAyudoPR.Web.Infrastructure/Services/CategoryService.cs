using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Domain.Entities;
using YoAyudoPR.Web.Domain.Repositories;
using YoAyudoPR.Web.Infrastructure.Repositories;

namespace YoAyudoPR.Web.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryService;

        public CategoryService(railwayContext context)
        {
            _categoryService = new GenericRepository<Category>(context);
        }

        public async Task<IEnumerable<CategoryListResponse>> FindAll(CancellationToken cancellationToken)
        {
            var categories = await _categoryService.FindAllAsync(cancellationToken);

            var categoryList = categories.Select(x => new CategoryListResponse
            {
                CategoryName = x.Name,
                CategoryId = x.Id
            });

            return categoryList;
        }
    }
}
