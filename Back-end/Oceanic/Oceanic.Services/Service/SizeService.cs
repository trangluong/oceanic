using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oceanic.Core;
using Oceanic.Infrastructure.Interfaces;
using Oceanic.Services.Interface;

namespace Oceanic.Services.Service
{
    public class SizeService : BaseService<Size>, ISizeService
    {
        private readonly IRepositoryAsync<Size> _repository;

        public SizeService(IRepositoryAsync<Size> repository) : base(repository)
        {
            this._repository = repository;
        }
        public IEnumerable<Size> LoadSize()
        {
            return _repository.Query().Select().ToList();
        }

       
    }
}
