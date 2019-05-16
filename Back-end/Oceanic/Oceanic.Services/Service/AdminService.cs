using System;
using System.Collections.Generic;
using System.Text;
using Oceanic.Core;
using Oceanic.Infrastructure.Interfaces;
using Oceanic.Services.Interface;

namespace Oceanic.Services.Service
{
    public class AdminService : IAdminService
    {
        private readonly IRepositoryAsync<City> _cityRepository;
        private readonly IRepositoryAsync<GoodsType> _goodsTypeRepository;
        private readonly IRepositoryAsync<Size> _sizeRepository;
        public AdminService(IRepositoryAsync<City> cityRepository, 
            IRepositoryAsync<GoodsType> goodsTypeRepository,
            IRepositoryAsync<Size> sizeRepository) 
        {
            this._cityRepository = cityRepository;
            this._goodsTypeRepository = goodsTypeRepository;
            this._sizeRepository = sizeRepository;
        }
        public void AddCity(City city)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<City> LoadCity(bool IsActive)
        {
            return _cityRepository.Query(x=>x.IsAcitve == IsActive).Select();
        }

        public IEnumerable<ExtraFee> LoadExtraFeeSettings()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GoodsType> LoadGoodsTypes()
        {
            return _goodsTypeRepository.Query().Select();
        }

        public IEnumerable<Price> LoadPriceSettings()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Size> LoadSizeSettings()
        {
            throw new NotImplementedException();
        }

        public void UpdateCity(City city)
        {
            throw new NotImplementedException();
        }

        public void UpdateExtraFeeSettings(ExtraFee extraFee)
        {
            throw new NotImplementedException();
        }

        public void UpdatePriceSettings(Price price)
        {
            throw new NotImplementedException();
        }

        public void UpdateSizeSettings(Size size)
        {
            this._sizeRepository.Update(size);
        }
    }
}
