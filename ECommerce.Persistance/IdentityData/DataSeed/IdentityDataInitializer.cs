using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Percistance.IdentityData.DataSeed
{
    public class IdentityDataInitializer : IDataInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;

        public IdentityDataInitializer(UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager,ILogger<IdentityDataInitializer> logger)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._logger = logger;
        }
        public async Task InitializerAsync(string paht)
        {
            try {

                if (!_roleManager.Roles.Any()) {

                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

                }   

                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser() {
                    DisplayName = "Mohammed Tarek",
                    UserName = "MohammedTarek",
                    Email = "MohammedTarek@gmail.com",
                    PhoneNumber = "01036598754"};
                    var user02 = new ApplicationUser()
                    {
                        DisplayName = "Salma Tarek",
                        UserName = "SalamaTarek",
                        Email = "SalmaTarek@gmail.com",
                        PhoneNumber = "01256598754"
                    };


                      var result  =   await _userManager.CreateAsync(user01,"P@ssword0");

                    if (!result.Succeeded)
                        throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
                    await _userManager.CreateAsync(user02, "P@ssword0");
                        await _userManager.AddToRoleAsync(user01, "Admin");
                        await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                    


                }
            

            
            }catch(Exception ex) {

                _logger.LogError($"Erro while seeding Identity data base Message : {ex.Message}");
            
            }
        }
    }
}
