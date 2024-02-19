using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Model.Dtos.Position;
using Riok.Mapperly.Abstractions;

namespace dotNet_wepApi_entityFrameWork.Model.Mapper
{

    [Mapper]
    public partial class PositionMapper
    {
        [MapProperty(nameof(Position.Code), nameof(PositionDTO.PositionCode))]
        [MapProperty(nameof(Position.Name), nameof(PositionDTO.PositionName))]
        public partial PositionDTO PositionToPositionDTO(Position position);

        [MapProperty(nameof(PositionDTO.PositionCode), nameof(Position.Code))]
        [MapProperty(nameof(PositionDTO.PositionName), nameof(Position.Name))]
        public partial Position PositionDTOPosition(PositionDTO positionDTO);
    }
}