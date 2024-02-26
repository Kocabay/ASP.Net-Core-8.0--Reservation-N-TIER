using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using TraversalCoreProje.Areas.Admin.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Role")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }


        [HttpGet]
        [Route("CreateRole")]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createroleViewModel)
        {
            AppRole role = new AppRole()
            {
                Name = createroleViewModel.RoleName,
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            return View();
        }
        [Route("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            await _roleManager.DeleteAsync(values);
            return RedirectToAction("Index");

        }


        [HttpGet]
        [Route("UpdateRole/{id}")]
        public IActionResult UpdateRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            UpdateViewRoleModel updateViewRoleModel = new UpdateViewRoleModel()
            {
                RoleID = values.Id,
                RoleName = values.Name

            };
            return View(values);
        }
        [HttpPost]
        [Route("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateRole(UpdateViewRoleModel updateViewRoleModel)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == updateViewRoleModel.RoleID);
            values.Name = updateViewRoleModel.RoleName;
            await _roleManager.UpdateAsync(values);
            return RedirectToAction("Index");
        }
        [Route("UserList")]
        public IActionResult UserList()
        {
            var values = _userManager.Users.ToList();
            return View(values);    
        }
        [Route("AssignRole/{id}")]
        public async Task<IActionResult> AssignRole(int id)
        {
            var users = _userManager.Users.FirstOrDefault(x => x.Id == id);
            TempData["UserId"] = users.Id;
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(users);
            List<RoleAssingViewModel> roleAssingViewModels = new List<RoleAssingViewModel>();
            foreach (var item in roles)
            {
                RoleAssingViewModel model = new RoleAssingViewModel();
                model.RoleID = item.Id;
                model.RoleName = item.Name;
                model.RoleExist = userRoles.Contains(item.Name);
                roleAssingViewModels.Add(model);
            }
            return View(roleAssingViewModels);
        }
        [HttpGet]
        [Route("AssignRole/{id}")]
        public async Task<IActionResult> AssignRole(List<RoleAssingViewModel> model)
        {
            //var userıd =(int) TempData["UserId"];
            var user = _userManager.Users.FirstOrDefault();
            foreach (var item in model)
            {
                if (item.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("UserList");
        }
    }
}
