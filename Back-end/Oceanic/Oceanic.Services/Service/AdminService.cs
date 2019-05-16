using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oceanic.Common.Model;
using Oceanic.Core;
using Oceanic.Infrastructure.Interfaces;
using Oceanic.Services.Interface;

namespace Oceanic.Services.Service
{
    public class AdminService : IAdminService
    {
        private readonly IRepositoryAsync<Route> _routeRepository;
        private readonly IRepositoryAsync<City> _cityRepository;
        private readonly IRepositoryAsync<Size> _sizeRepository;
        private readonly IRepositoryAsync<ExtraFee> _extraFeeRepository;
        private readonly IRepositoryAsync<GoodsType> _goodsTypeRepository;
        private readonly IRepositoryAsync<Price> _priceRepository;
        IUnitOfWorkAsync _unitOfWork;

        public AdminService(IRepositoryAsync<City> cityRepository, IRepositoryAsync<Size> sizeRepository,
           IRepositoryAsync<ExtraFee> extraFeeRepository, IRepositoryAsync<GoodsType> goodsTypeRepository, IRepositoryAsync<Price> priceRepository,
            IRepositoryAsync<Route> routeRepository, IUnitOfWorkAsync unitOfWork) 
        {
            this._cityRepository = cityRepository;
            this._sizeRepository = sizeRepository;
            this._goodsTypeRepository = goodsTypeRepository;
            this._priceRepository = priceRepository;
            this._extraFeeRepository = extraFeeRepository;
            this._routeRepository = routeRepository;
            this._unitOfWork = unitOfWork;
        }
        public void AddCity(City city)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<City> LoadCity(bool IsActive)
        {
            return _cityRepository.Query(x=>x.IsAcitve == IsActive).Select();
        }
        public void UpdateCity(City city)
        {
            this._cityRepository.Update(city);
            this._unitOfWork.SaveChanges();
        }


        public IEnumerable<ExtraFee> LoadExtraFeeSettings()
        {
            return _extraFeeRepository.Query().Select();
        }

        public IEnumerable<GoodsType> LoadGoodsTypes()
        {
            return _goodsTypeRepository.Query().Select();
        }

        public IEnumerable<Price> LoadPriceSettings()
        {
            return _priceRepository.Query().Select();
        }

        public IEnumerable<Size> LoadSizeSettings()
        {
            return _sizeRepository.Query().Select();
        }

        public string GetTypeNameById(int typeId)
        {
            return _sizeRepository.Query(x => x.Id == typeId).Select(x => x.Type).FirstOrDefault();
        }

        public int GetTypeIdByName(string typeName)
        {
            return _sizeRepository.Query(x => x.Type == typeName).Select(x => x.Id).FirstOrDefault();
        }


        public void UpdateExtraFeeSettings(ExtraFee extraFee)
        {
            this._extraFeeRepository.Update(extraFee);
            this._unitOfWork.SaveChanges();
        }

        public void UpdatePriceSettings(Price price)
        {
            this._priceRepository.Update(price);
            this._unitOfWork.SaveChanges();
        }

        public void UpdateSizeSettings(Size size)
        {
            this._sizeRepository.Update(size);
            _unitOfWork.SaveChanges();
        }

        public string GetGoodsTYpeNameById(int typeId)
        {
            return _goodsTypeRepository.Query(x => x.Id == typeId).Select(x => x.Name).FirstOrDefault();
        }

        public int GetIdIdByGoodsTypeName(string typeName)
        {
            return _goodsTypeRepository.Query(x => x.Name == typeName).Select(x => x.Id).FirstOrDefault();
        }

        public IEnumerable<Route> LoadRoutes()
        {
            return _routeRepository.Query().Select();
        }

        public string GetCityNameById(int cityId)
        {
            return _cityRepository.Query(x => x.Id == cityId).Select(x => x.Name).FirstOrDefault();
        }

        private CalculatePrice CalulatePriceForASegment(CalculatePriceViewModel model, IList<Size> sizes, IList<GoodsType> goodsTypes, IList<Price> prices)
        {
            Size size = new Size();
            foreach (var s in sizes)
            {
                if (model.height <= s.MaxHeight && model.length <= s.MaxDepth && model.width <= s.MaxBreadth)
                {
                    size = s;
                    break;
                }
            }

            if (size == null)
            {
                return new CalculatePrice()
                {
                    price = 0,
                    status = 0
                };
            }
            Price price = new Price();
            foreach (var x in prices)
            {
                if (x.SizeId == size.Id && model.weight <= x.MaxWeight)
                {
                    price = x;
                    break;
                }

            }
            return  new CalculatePrice()
            {
                price = price.Fee,
                status = 1
            };


        }
        public IList<CalculatePrice> CalculatePrices(IList<CalculatePriceViewModel> calculatePriceViewModel)
        {
            IList<Size> sizes = _sizeRepository.Query().Select().ToList();
            IList<GoodsType> goodsTypes = _goodsTypeRepository.Query().Select().ToList();
            IList<Price> prices = _priceRepository.Query().Select().ToList();
            IList<CalculatePrice> result = new List<CalculatePrice>();
            
            foreach (var item in calculatePriceViewModel)
            {
                result.Add(CalulatePriceForASegment(item, sizes, goodsTypes, prices));
            }
            return result;
        }


    }
}
