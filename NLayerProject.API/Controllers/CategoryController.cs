using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.API.DTOs;
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
    }
}
