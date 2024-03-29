using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Data;
using dotNet_wepApi_entityFrameWork.Helpers;
using dotNet_wepApi_entityFrameWork.Model.Dtos.Position;
using dotNet_wepApi_entityFrameWork.Repository.PositionRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_wepApi_entityFrameWork.Services.PositionService
{
    public class PositionService(IPositionRepository repository) : IPositionService
    {
        public async Task<ServiceResponse<IList<PositionDTO>>> GetAllPositions(QueryObject query)
        {
            var serviceResponse = new ServiceResponse<IList<PositionDTO>>();
            try
            {
                var positions = await repository.GetAllAsync(query);

                var result = positions
                    .Select(p => new PositionDTO { PositionName = p.Name, PositionCode = p.Code })
                    .ToList();
                serviceResponse.Data = result;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<PositionDTO?>> GetPositionByCode(int code)
        {
            var serviceResponse = new ServiceResponse<PositionDTO?>();
            try
            {
                var position =
                    await repository.GetByCodeAsync(code)
                    ?? throw new Exception($"position not found.");
                serviceResponse.Data = new PositionDTO
                {
                    PositionName = position.Name,
                    PositionCode = position.Code
                };
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
