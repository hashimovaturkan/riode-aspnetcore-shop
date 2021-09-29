﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using Riode_WebUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.CategoriesModule
{
    public class CategoryPagedQuery : IRequest<PagedViewModel<Category>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public class CategoryPagedQueryHandler : IRequestHandler<CategoryPagedQuery, PagedViewModel<Category>>
        {
            readonly RiodeDbContext db;
            public CategoryPagedQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<Category>> Handle(CategoryPagedQuery request, CancellationToken cancellationToken)
            {
                var query = db.Categories
                    .Include(c => c.Parent)
                    .Include(c => c.Children)
                    .ThenInclude(c => c.Children)
                    .Where(s => s.DeletedByUserId == null && s.ParentId == null).AsQueryable();
                
                return new PagedViewModel<Category>(query, request.PageIndex, request.PageSize);
            }
        }
    }
}
