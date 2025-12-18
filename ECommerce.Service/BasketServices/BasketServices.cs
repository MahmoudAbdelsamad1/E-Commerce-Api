using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModule;
using ECommerce.Service.Abstraction;
using ECommerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.BasketServices
{
    internal class BasketServices : IBasketServices
    {
        private readonly IBasketRepository _repo;
        private readonly IMapper _mapper;

        public BasketServices(IBasketRepository repo , IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
           var customerBasket = _mapper.Map<CustomerBasket>(basket);
            var CreateOrUpdaetBasket = _mapper.Map<BasketDTO>(await _repo.CreateOrUpdateBasket(customerBasket));

            return CreateOrUpdaetBasket ;
        }

        public Task<bool> DeleteBasketAsync(string id) => _repo.DeleteBasket(id);
        public async Task<BasketDTO> GetBasketAsync(string id)
        {
           return _mapper.Map<BasketDTO>(await _repo.GetBasket(id));
        }
    }
}
