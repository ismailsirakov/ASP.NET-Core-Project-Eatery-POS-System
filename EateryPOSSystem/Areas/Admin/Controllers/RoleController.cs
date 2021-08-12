namespace EateryPOSSystem.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using EateryPOSSystem.Areas.Admin.Models.Role;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Data.Models;
    using static AdminConstants;
    using static WebConstants;

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IDbService dbService;

        public RoleController(IDbService dbService,
                              RoleManager<IdentityRole> roleManager,
                              UserManager<User> userManager)
        {
            this.dbService = dbService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Create()
            => View();

        [HttpPost]
        public async Task<IActionResult> Create(RoleFormModel role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }

            var roleExist = await roleManager.RoleExistsAsync(role.Name);

            if (roleExist)
            {
                ModelState.AddModelError(nameof(role.Name), ЕxistingRoleInDb);

                return View(role);
            }

            await roleManager.CreateAsync(new IdentityRole { Name = role.Name });

            TempData[GlobalMessageKey] = $"В база данни успешно се добави роля '{role.Name}'.";

            return Redirect("/Home/Index");
        }

        public IActionResult AddRoleToUser()
        {
            var users = dbService.GetUsers();

            var roles = roleManager.Roles.ToList();

            var user = new AddRemoveRoleFromUserFormModel
            {
                Users = users,
                Roles = roles
            };

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(AddRemoveRoleFromUserFormModel userRole)
        {
            userRole.Users = dbService.GetUsers();

            userRole.Roles = roleManager.Roles.ToList();

            var user = await userManager.FindByIdAsync(userRole.UserId);

            var role = await roleManager.FindByIdAsync(userRole.RoleId);


            var roleName = await roleManager.GetRoleNameAsync(role);

            await userManager.AddToRoleAsync(user, roleName);

            TempData[GlobalMessageKey] = $"В база данни успешно се добави роля '{roleName}' към потребител '{user.UserName}'.";

            return Redirect("/Home/Index");
        }

        public IActionResult RemoveRoleFromUserFirstPage()
        {
            var user = new AddRemoveRoleFromUserFormModel
            {
                Users = dbService.GetUsers()
            };

            return View(user);
        }

        [HttpPost]
        public IActionResult RemoveRoleFromUserFirstPage(AddRemoveRoleFromUserFormModel user)
        {
            return RedirectToAction(nameof(RemoveRoleFromUserSecondPage), user);
        }

        public async Task<IActionResult> RemoveRoleFromUserSecondPage(AddRemoveRoleFromUserFormModel userRoles)
        {
            var user = await userManager.FindByIdAsync(userRoles.UserId);

            var userName = await userManager.GetUserNameAsync(user);

            userRoles.UserName = userName;

            userRoles.RolesNames = await userManager.GetRolesAsync(user);

            return View(userRoles);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRoleFromUserSecondPage(AddRemoveRoleFromUserFormModel userRoles, string button)
        {
            var user = await userManager.FindByIdAsync(userRoles.UserId);

            var userName = await userManager.GetUserNameAsync(user);

            userRoles.UserName = userName;

            userRoles.RolesNames = await userManager.GetRolesAsync(user);

            await userManager.RemoveFromRoleAsync(user, userRoles.RoleName);

            TempData[GlobalMessageKey] = $"В база данни успешно се изтри роля '{userRoles.RoleName}' от потребител '{userName}'.";

            return Redirect("/Home/Index");
        }
    }
}