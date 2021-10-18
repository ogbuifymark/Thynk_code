using System;
using Thynk_Code.Data.Interfaces;
using Thynk_Code.Service.Interfaces;

namespace Thynk_Code.Service.Implementations
{
    
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        //protected readonly IMapper _mapper;
        protected readonly IServiceFactory _serviceFactory;

        public BaseService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory)
        {
            _unitOfWork = unitOfWork;
            //_mapper = mapper;
            _serviceFactory = serviceFactory;
        }
    }
}
