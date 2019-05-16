using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oceanic.Core;
using Oceanic.Services.Interface;

namespace Oceanic.Controllers
{
    [Produces("application/json")]
    public class AdminController : ControllerBase
    {
        //private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("api/cities")]
        [HttpGet]
        public IEnumerable<CityViewModel> LoadCity()
        {
            return _adminService.LoadCity(true).Select(x => new CityViewModel
            {
                id = x.Id,
                name = x.Name
            });
        }

        [Route("api/goodsTypes")]
        [HttpGet]
        public IEnumerable<GoodsTypeModel> LoadGoodsType()
        {
            return _adminService.LoadGoodsTypes().Select(x => new GoodsTypeModel
            {
                id = x.Id,
                name = x.Name,

            });
        }

        [Route("api/sizeSettings")]
        [HttpGet]
        public IEnumerable<SizeViewModel> LoadSize()
        {
            return _adminService.LoadSizeSettings().Select(x => new SizeViewModel
            {
                id = x.Id,
                type = x.Type,
                maxHeight = x.MaxHeight,
                maxBreath = x.MaxBreath,
                maxDepth = x.MaxBreath
            });
        }

        [Route("api/sizeSettings")]
        [HttpPut]
        public void UpdateSize([FromBody] SizeViewModel sizeViewModel)
        {
            Size size = new Size()
            {
                Id = sizeViewModel.id,
                Type = sizeViewModel.type,
                MaxHeight = sizeViewModel.maxHeight,
                MaxBreath = sizeViewModel.maxBreath,
                MaxDepth = sizeViewModel.maxDepth
            };
            _adminService.UpdateSizeSettings(size);    
        }


        [Route("api/priceSettings")]
        [HttpGet]
        public IEnumerable<PriceViewModel> LoadPrice()
        {
            return _adminService.LoadPriceSettings().Select(x => new PriceViewModel
            {
                id = x.Id,
                sizeType = _adminService.GetTypeNameById(x.SizeId),
                maxWeight = x.MaxWeight,
                price = x.Fee
  
            });
        }

        [Route("api/priceSettings")]
        [HttpPut]
        public void UpdatePrice([FromBody] PriceViewModel priceViewModel)
        {
            Price price = new Price()
            {
                Id = priceViewModel.id,
                SizeId = _adminService.GetTypeIdByName(priceViewModel.sizeType),
                MaxWeight = priceViewModel.maxWeight,
              
            };
            _adminService.UpdatePriceSettings(price);

        }


        [Route("api/extraFeeSettings")]
        [HttpGet]
        public IEnumerable<ExtraFeeViewModel> LoadExtraFee()
        {

            return _adminService.LoadExtraFeeSettings().Select(x => new ExtraFeeViewModel
            {
                id = x.Id,
                extraFee = x.ExtraPercent,
                goodsType = _adminService.GetGoodsTYpeNameById(x.GoodsTypeId)
                

            });
        }

        [Route("api/extraFeeSettings")]
        [HttpPut]
        public void UpdateExtraFee([FromBody]ExtraFeeViewModel extraFeeViewModel)
        {
            ExtraFee extraFee = new ExtraFee()
            {
                Id = extraFeeViewModel.id,
                ExtraPercent = extraFeeViewModel.extraFee,
                GoodsTypeId = _adminService.GetIdIdByGoodsTypeName(extraFeeViewModel.goodsType)

            };

            _adminService.UpdateExtraFeeSettings(extraFee);

        }

    }
}