using AppoinmentApp.Data;
using AppoinmentApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppoinmentApp.Seed
{
    public static class IntlizerDb
    {
        public static AppDbContext _db;

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            //Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(Helpers.Hepler.Admin))
                await roleManager.CreateAsync(new IdentityRole(Helpers.Hepler.Admin));
            if (!await roleManager.RoleExistsAsync(Helpers.Hepler.Doctor))
                await roleManager.CreateAsync(new IdentityRole(Helpers.Hepler.Doctor));
            if (!await roleManager.RoleExistsAsync(Helpers.Hepler.Patinet))
                await roleManager.CreateAsync(new IdentityRole(Helpers.Hepler.Patinet));

            //Roles
            var roles = new List<IdentityRole>()
            {
                new IdentityRole{ Name=Helpers.Hepler.Admin ,Id="1"},
                new IdentityRole{ Name=Helpers.Hepler.Doctor,Id="2"},
                new IdentityRole{ Name=Helpers.Hepler.Patinet,Id="3"}
            };
           await _db.AddRangeAsync(roles);
            await _db.SaveChangesAsync();

            }
        }
    }

