using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagRepositories tagRepositories;

        public AdminTagController(ITagRepositories tagRepositories)
        {
            this.tagRepositories = tagRepositories;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepositories.AddAsync(tag);

            return RedirectToAction("List");

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepositories.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepositories.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var updatedTag = await tagRepositories.UpdateAsync(tag);
            if(updatedTag != null)
            {

            }
            else
            {

            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });


        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepositories.DeleteAsync(editTagRequest.Id);
          
          if(deletedTag != null)
            {
                return RedirectToAction("List");
            }
          else
            {
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }


        }

    }

}
