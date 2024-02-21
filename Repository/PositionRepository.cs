using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Data;
using dotNet_wepApi_entityFrameWork.Helpers;
using Microsoft.EntityFrameworkCore;

namespace dotNet_wepApi_entityFrameWork.Repository.PositionRepository
{
    public class PositionRepository(DataContext dataContext) : IPositionRepository
    {
        public async Task<List<Position>> GetAllAsync(QueryObject query)
        {
            var positions = dataContext.Positions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    positions = query.IsDescending
                        ? positions.OrderByDescending(s => s.Code)
                        : positions.OrderBy(s => s.Code);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await positions.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Position?> GetByCodeAsync(int code)
        {
            return await dataContext.Positions.FirstOrDefaultAsync(p => p.Code == code);
        }
    }
}
