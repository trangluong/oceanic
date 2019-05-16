using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oceanic.Common.Model;
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
                maxBreadth = x.MaxBreadth,
                maxDepth = x.MaxDepth
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
                MaxBreadth = sizeViewModel.maxBreadth,
                MaxDepth = sizeViewModel.maxDepth,
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

        [Route("api/exportedRoutes")]
        [HttpPut]
        public IEnumerable<RoutesViewModel> ExportRoutes()
        {
            return _adminService.LoadRoutes().Select(x => new RoutesViewModel
            {
                from_city = _adminService.GetCityNameById(x.FromCityId),
                to_city = _adminService.GetCityNameById(x.ToCityId),
                hours = x.LongHour,
                segment = x.Segments

            });

        }


        [Route("api/calculatePrices")]
        [HttpPost]
        public IList<CalculatePrice> CalculatePrice([FromBody] IList<CalculatePriceViewModel> calculatePriceViewModel)
        {
            IList<CalculatePrice> result = new List<CalculatePrice>();
            foreach (var item in calculatePriceViewModel)
            {
                int goodsType = _adminService.LoadGoodsTypes().Select(x => x.Code == item.goods_type).Count();
                if (goodsType == 0 || item.height > 200 || item.width > 200 || item.length > 200)
                {
                    CalculatePrice response = new CalculatePrice()
                    {
                        price = 0,
                        status = 0
                    };
                    result.Add(response);
                }
            }
            return result;
            
            

        }

    }
}