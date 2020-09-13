using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.API.DTOs;
using NLayerProject.Core.Models;
using NLayerProject.Core.Services;

namespace NLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService,IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task <IActionResult> GetAll()
        {

            var categories = await _categoryService.GetAllAsync();
            //return Ok(categories);
            //mapper uzerinden nesneleri dönüştürdüm C
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));


        }
        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            //idleri kontrol etmek için filter kullanıcaz
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(_mapper.Map<CategoryDto>(category));
        }
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProductsById(int id)
        {
            var category = await _categoryService.GetWithProductByIdAsync(id);

            return Ok(_mapper.Map<CategoryWithProductsDto>(category));

        }

        [HttpPost]
        public async Task <IActionResult> Save(CategoryDto categoryDto)
        {
          var newCategory =  await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));
            return Created(string.Empty, _mapper.Map<CategoryDto>(newCategory));
        }

        [HttpPut]
        public IActionResult Update (CategoryDto categoryDto)
        {
            var category = _categoryService.Update(_mapper.Map<Category>(categoryDto));
            
            return NoContent();
            
        }
        [HttpDelete("{id}")]
        public IActionResult Remove (int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(category);
            return NoContent();

        }

        

    }
}
