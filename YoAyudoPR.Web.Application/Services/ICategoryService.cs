using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Domain.Entities;

namespace YoAyudoPR.Web.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListResponse>> FindAll(CancellationToken cancellationToken);
    }
}
