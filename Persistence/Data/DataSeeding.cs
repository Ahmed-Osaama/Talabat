using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Domain.Entities.OrderModule;
using Domain.Entities.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Persistence.Data
{
    public class DataSeeding(StoreDbContext _dbContext
        ,RoleManager<IdentityRole> _roleManager
        ,UserManager<User> _userManager) : IDataSeeding
    {
        public async Task SeedDataAsync()
        {
			try
			{
                
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if ((PendingMigrations).Any())
                   await _dbContext.Database.MigrateAsync();
                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandsData = File.OpenRead("../Persistence/Data/DataSeeding/brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsData);
                    if (ProductBrands != null && ProductBrands.Any())
                       await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypesData = File.OpenRead("../Persistence/Data/DataSeeding/types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypesData);
                    if (ProductTypes != null && ProductTypes.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                }
                if (!_dbContext.Products.Any())
                {
                    var ProductsData = File.OpenRead("../Persistence/Data/DataSeeding/products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (Products != null && Products.Any())
                        await _dbContext.Products.AddRangeAsync(Products);
                }
                if(!_dbContext.DeliveryMethods.Any())
                {
                    var DeliveryMethodsData = File.OpenRead("../Persistence/Data/DataSeeding/delivery.json");
                    var DeliveryMethod = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodsData);
                    if (DeliveryMethod != null && DeliveryMethod.Any())
                        await _dbContext.DeliveryMethods.AddRangeAsync(DeliveryMethod);
                }
                await _dbContext.SaveChangesAsync();
            }
			catch (Exception ex)
			{

			}
        }

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                //Seed roles [Admin , SuperAdmin]
                if(!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole ("SuperAdmin"));
                }
                //Seed Users[AdminUser , SuperAdminUser]
                if (!_userManager.Users.Any())
                {
                    var AdminUser = new User()
                    {
                        DisplayName = "Admin",
                        UserName = "Admin",
                        Email = "AdminUser@gmail.com",
                        PhoneNumber = "01156980468"
                    };
                    var SuperAdminUser = new User()
                    {
                        DisplayName = "SuperAdmin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdminUser@gmail.com",
                        PhoneNumber = "01075684432"
                    };
                    await _userManager.CreateAsync(AdminUser, "P@ssw0rd");
                    await _userManager.CreateAsync(SuperAdminUser, "Pa$$w0rd");
                    //Assign Roles to Users
                    await _userManager.AddToRoleAsync(AdminUser, "Admin");
                    await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
