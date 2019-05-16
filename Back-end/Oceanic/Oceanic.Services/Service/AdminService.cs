using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oceanic.Core;
using Oceanic.Infrastructure.Interfaces;
using Oceanic.Services.Interface;

namespace Oceanic.Services.Service
{
    public class AdminService : IAdminService
    {
        private readonly IRepositoryAsync<City> _cityRepository;
        private readonly IRepositoryAsync<Size> _sizeRepository;
        private readonly IRepositoryAsync<ExtraFee> _extraFeeRepository;
        private readonly IRepositoryAsync<GoodsType> _goodsTypeRepository;
        private readonly IRepositoryAsync<Price> _priceRepository;
        public AdminService(IRepositoryAsync<City> cityRepository, IRepositoryAsync<Size> sizeRepository,
           IRepositoryAsync<ExtraFee> extraFeeRepository, IRepositoryAsync<GoodsType> goodsTypeRepository, IRepositoryAsync<Price> priceRepository) 
        {
            this._cityRepository = cityRepository;
            this._sizeRepository = sizeRepository;
            this._goodsTypeRepository = goodsTypeRepository;
            this._priceRepository = priceRepository;
            this._extraFeeRepository = extraFeeRepository;
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
            throw new NotImplementedException();
        }


        public IEnumerable<ExtraFee> LoadExtraFeeSettings()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GoodsType> LoadGoodsTypes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Price> LoadPriceSettings()
        {
            throw new NotImplementedException();
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
