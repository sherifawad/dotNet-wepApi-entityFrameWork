using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Helpers;
using dotNet_wepApi_entityFrameWork.Model.Dtos.Position;

namespace dotNet_wepApi_entityFrameWork.Services.PositionService
{
    public interface IPositionService
    {
        Task<ServiceResponse<IList<PositionDTO>>> GetAllPositions(QueryObject query);
        Task<ServiceResponse<PositionDTO?>> GetPositionByCode(int code);
    }
}
