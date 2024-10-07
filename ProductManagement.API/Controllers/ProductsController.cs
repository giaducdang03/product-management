using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.API.ViewModels.ResponseModels;
using ProductManagement.Repository.Commons;
using ProductManagement.Repository.Models;
using ProductManagement.Service.BussinessModels;
using ProductManagement.Service.Interfaces;
using ProductManagement.Service.Services;

namespace ProductManagement.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper) 
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationParameter paginationParameter, ProductFilter productFilter)
        {
            try
            {
                var result = await _productService.GetPagingProductsAsync(paginationParameter, productFilter);
                var metadata = new
                {
                    result.TotalCount,
                    result.PageSize,
                    result.CurrentPage,
                    result.TotalPages,
                    result.HasNext,
                    result.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new ResponseModel
                    {
                        HttpCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message.ToString()
                    }
               );
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var result = await _productService.GetProductByIdAsync(id);
                if (result == null) 
                { 
                    return NotFound(new ResponseModel
                    {
                        HttpCode = StatusCodes.Status404NotFound,
                        Message = "Not found product."
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel
                {
                    HttpCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateProduct(CreateProductModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.CreateProductAsync(productModel);
                    return Created("Create product successfully", result);
                }
                return ValidationProblem(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel
                {
                    HttpCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateProduct(UpdateProductModel updateProductModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.UpdateProductAsync(updateProductModel);
                    return Ok(result);
                }
                return ValidationProblem(ModelState);

            }
            catch (Exception ex)
            {
                return BadRequest(
                    new ResponseModel
                    {
                        HttpCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message.ToString()
                    }
               );
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                if (result != null)
                {
                    return Ok(new ResponseModel
                    {
                        HttpCode = StatusCodes.Status200OK,
                        Message = $"Delete product '{result.ProductName}' successfully."
                    });
                }
                else
                {
                    throw new Exception("Can not delete product.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(
                    new ResponseModel
                    {
                        HttpCode = StatusCodes.Status400BadRequest,
                        Message = ex.Message.ToString()
                    }
               );
            }
        }
    }
}
