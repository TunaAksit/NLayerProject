using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerProject.Core.Models;
using NLayerProject.Core.Services;
using NLayerProject.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Filters
{

    public class CategorytNotFoundFilter:ActionFilterAttribute
    {
        private readonly ICategoryService _categoryService;
        //private readonly IService<Product>
        public CategorytNotFoundFilter(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id =(int) context.ActionArguments.Values.FirstOrDefault();
            var product = await _categoryService.GetByIdAsync(id);
            if(product!=null)
            {
                //gelen requesti bir sonraki adıma atla
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Errors.Add($"id'si {id} olan kategori veri tabanında bulunamadı");
                context.Result = new RedirectToActionResult("Error", "Home", errorDto);
                   
            }


        }

    }
}
