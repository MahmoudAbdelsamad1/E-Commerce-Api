using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Service.Abstraction.IProductServices;
using ECommerce.Service.CustomExceptions;
using ECommerce.Service.Specifications;
using ECommerce.Shared;
using ECommerce.Shared.CommonResults;
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

        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var specification = new  ProductWithBrandAndTypeSpecifications(queryParams);
            var products = await _unitOfWork.GenerateRepository<Product, int>().GetAllAsync(specification);

            var data =  _mapper.Map<IEnumerable<ProductDTO>>(products);
            int count = data.Count();
            var countSpec = new ProductCountSpecifications(queryParams);
            var totalCount = await _unitOfWork.GenerateRepository<Product, int>().GetCountAsync(countSpec);
            return new PaginatedResult<ProductDTO>(queryParams.PageIndex, queryParams.PageSize, totalCount, data);
        }



        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var types  = await _unitOfWork.GenerateRepository<ProductType, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<TypeDTO>>(types);
        }

        public async Task<Result<ProductDTO>?> GetProductsByIdAsync(int Id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(Id);
            var product = await _unitOfWork.GenerateRepository<Product, int>().GetByIdAsync(spec);

            if (product == null) return Result<ProductDTO>.Failed(Error.NotFound("ProductNotFound", $"Product with {Id} not found"));

            return Result<ProductDTO>.Ok(_mapper.Map<ProductDTO>(product));
        }
    }
}
