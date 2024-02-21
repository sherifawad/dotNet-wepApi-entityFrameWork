using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Helpers;

namespace dotNet_wepApi_entityFrameWork.Repository.PositionRepository
{
    public interface IPositionRepository
    {
        Task<List<Position>> GetAllAsync(QueryObject query);
        Task<Position?> GetByCodeAsync(int code);
    }
}
