using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Oceanic.Common;
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
        public void UpdatePrice([FromBody] IList<PriceViewModel> priceViewModel)
        {
            foreach (var m in priceViewModel)
            {
                Price price = new Price()
                {
                    Id = m.id,
                    SizeId = _adminService.GetTypeIdByName(m.sizeType),
                    MaxWeight = m.maxWeight,
                    Fee = m.price
                };
                _adminService.UpdatePriceSettings(price);
            }
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
        [HttpGet]
        public IEnumerable<RoutesViewModel> ExportRoutes()
        {
            return _adminService.LoadRoutes().Select(x => new RoutesViewModel
            {
                from_city = _adminService.GetCityCodeByID(x.FromCityId),
                to_city = _adminService.GetCityCodeByID(x.ToCityId),
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
                else
                {
                   result = _adminService.CalculatePrices(calculatePriceViewModel);
                }
            }
            return result;
        }

        [Route("api/external/routes/{transportType}")]
        [HttpGet]

        public IEnumerable<RoutesViewModel> ExportRoutes(string transportType)
        {
            HttpWebRequestHandler task = new HttpWebRequestHandler();
             
            if(transportType == "sea")
            {
               return task.GetReleases("https://wa-eitvn.azurewebsites.net/index.php?r=api/routes");
            }

            if (transportType == "car")
            {
                return task.GetReleases("https://wa-tlvn.azurewebsites.net/api/public/configuredRoutes");
            }
            return null;
        }

        [Route("api/external/calculatePrices/{transportType}")]
        [HttpPost]

        public IList<CalculatePrice> CalculatePriceExternal([FromBody] IList<CalculatePriceViewModel> calculatePriceViewModel, string transportType)
        {

            HttpWebRequestHandler task = new HttpWebRequestHandler();

            if (transportType == "sea")
            {
                return task.PostMethod("https://wa-eitvn.azurewebsites.net/index.php?r=api/price", 
                    calculatePriceViewModel).Result;
            }

            if (transportType == "car")
            {
                return task.PostMethod("https://wa-tlvn.azurewebsites.net/api/public/caculatePrices", 
                    calculatePriceViewModel).Result;

            }
            return null;

           
        }

    }
}