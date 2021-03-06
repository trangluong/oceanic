﻿using Oceanic.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Oceanic.Common.Model;

namespace Oceanic.Services.Interface
{
    public interface IAdminService
    {
        IEnumerable<Route> LoadRoutes();
        IEnumerable<City> LoadCity(bool IsActive);
        IEnumerable<GoodsType> LoadGoodsTypes();
        IEnumerable<Size> LoadSizeSettings();
        IEnumerable<Price> LoadPriceSettings();
        IEnumerable<ExtraFee> LoadExtraFeeSettings();
        void UpdateSizeSettings(Size size);
        void UpdatePriceSettings(Price price);
        void UpdateExtraFeeSettings(ExtraFee extraFee);
        void AddCity(City city);
        void UpdateCity(City city);
        string GetTypeNameById(int typeId);
        int GetTypeIdByName(string typeName);
        string GetGoodsTYpeNameById(int typeId);
        int GetIdIdByGoodsTypeName(string typeName);
        string GetCityCodeByID(int cityId);
        IList<CalculatePrice> CalculatePrices(IList<CalculatePriceViewModel> calculatePriceViewModel);
       

    }
}