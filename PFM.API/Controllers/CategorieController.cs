using Microsoft.AspNetCore.Mvc;
using PFM.Application.DTO;
using PFM.Application.IServices;

namespace PFM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly ICategorieServices _categorieServices;
        public CategorieController(ICategorieServices categorieServices)
        {
            _categorieServices = categorieServices;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var categorie = _categorieServices.GetAll();
            return Ok(categorie);
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var categorie = _categorieServices.GetById(Id);

            if (categorie == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(categorie);
        }

        [HttpPost("Create")]
        public IActionResult Create(CategorieDTO categorieDTO)
        {
            _categorieServices.Create(categorieDTO);

            return Ok("Create Sucessfully");
        }

        [HttpPut("Update")]
        public IActionResult Update(int Id, CategorieDTO categorieDTO)
        {
            categorieDTO.CategoriesId = Id;

           _categorieServices.Update(categorieDTO);

            return Ok("Category updated successfully.");
        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            var category = _categorieServices.GetById(Id);

            if (category == null)
            {
                return NotFound("Category not found.");
            }

            _categorieServices.Delete(Id);
            return Ok("Delete Sucessfully");
        }
    }
}
