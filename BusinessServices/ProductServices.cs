using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class ProductServices : IProductServices
    {

        private readonly UnitOfWork _unitOfWork;

        /// <summary>  
        /// Public constructor.  
        /// </summary>  
        public ProductServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>  
        /// Fetches product details by id  
        /// </summary>  
        /// <param name="productId"></param>  
        /// <returns></returns>  
        public BusinessEntities.ProductEntity GetProductById(int productId)
        {
            var product = _unitOfWork.ProductRepository.GetByID(productId);
            if (product != null)
            {
                ProductEntity productModel = new ProductEntity();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductEntity>());
                var mapper = new Mapper(config);
                productModel = mapper.Map<ProductEntity>(product);
                return productModel;
            }
            return null;
        }

        /// <summary>  
        /// Fetches all the products.  
        /// </summary>  
        /// <returns></returns>  
        public IEnumerable<BusinessEntities.ProductEntity> GetAllProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAll().ToList();
            if (products.Any())
            {
                List<ProductEntity> productsModel = new List<ProductEntity>();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductEntity>());
                var mapper = new Mapper(config);
                productsModel = mapper.Map<List<ProductEntity>>(products);
                return productsModel;
            }
            return null;
        }

        /// <summary>  
        /// Creates a product  
        /// </summary>  
        /// <param name="productEntity"></param>  
        /// <returns></returns>  
        public int CreateProduct(BusinessEntities.ProductEntity productEntity)
        {
            using (var scope = new TransactionScope())
            {
                var product = new Product
                {
                    ProductName = productEntity.ProductName
                };
                _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.Save();
                scope.Complete();
                return product.ProductId;
            }
        }

        /// <summary>  
        /// Updates a product  
        /// </summary>  
        /// <param name="productId"></param>  
        /// <param name="productEntity"></param>  
        /// <returns></returns>  
        public bool UpdateProduct(int productId, BusinessEntities.ProductEntity productEntity)
        {
            var success = false;
            if (productEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {
                        product.ProductName = productEntity.ProductName;
                        _unitOfWork.ProductRepository.Update(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>  
        /// Deletes a particular product  
        /// </summary>  
        /// <param name="productId"></param>  
        /// <returns></returns>  
        public bool DeleteProduct(int productId)
        {
            var success = false;
            if (productId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {

                        _unitOfWork.ProductRepository.Delete(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
