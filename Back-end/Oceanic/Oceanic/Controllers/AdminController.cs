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
                sizeType = _adminService.GetTypeNameById(x.Id),
                maxWeight = x.MaxWeight,
                price = x.Fee
  
            }); ;
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
        public IEnumerable<ExtraFee> LoadExtraFee()
        {
            return _adminService.LoadExtraFeeSettings();
        }

        [Route("api/extraFeeSettings")]
        [HttpPut]
        public void UpdateExtraFee([FromBody]SizeViewModel sizeModel)
        {
            Size size = new Size()
            {
                Id = sizeModel.id,
                Type = sizeModel.type,
                MaxHeight = sizeModel.maxHeight,
                MaxBreath = sizeModel.maxBreath,
                MaxDepth = sizeModel.maxDepth
            };

            _adminService.UpdateSizeSettings(size);

        }

    }
}