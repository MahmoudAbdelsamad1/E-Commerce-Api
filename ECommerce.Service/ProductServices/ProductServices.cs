using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Service.Abstraction.IProductServices;
using ECommerce.Service.Specifications;
using ECommerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GenerateRepository<ProductBrand,int>().GetAllAsync();

            return _mapper.Map<IEnumerable<BrandDTO>>(brands);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var specification = new  ProductWithBrandAndTypeSpecifications();
            var products = await _unitOfWork.GenerateRepository<Product, int>().GetAllAsync(specification);

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var types  = await _unitOfWork.GenerateRepository<ProductType, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<TypeDTO>>(types);
        }

        public async Task<ProductDTO?> GetProductsByIdAsync(int Id)
        {
            var product = await _unitOfWork.GenerateRepository<Product, int>().GetByIdAsync(Id);

            return _mapper.Map<ProductDTO>(product);
        }
    }
}
