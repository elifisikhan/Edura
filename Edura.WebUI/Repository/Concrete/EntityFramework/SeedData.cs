using Edura.WebUI.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edura.WebUI.Repository.Concrete.EntityFramework
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices
                .GetRequiredService<EduraContext>();

            context.Database.Migrate();

            if (!context.Products.Any())
            {
                var products = new[]
                {
                    new Product(){ ProductName="Photo Camera", Price=1000, Image= "product1.jpg", IsApproved= true, IsHome=true, IsFeatured=false , Description="ernjdfgıseırjdjkfnjkjkgnjerngerıogfnmdgıne", HtmlContext="<b>jkdfgjkdfng</b>",DateAdded=DateTime.Now.AddDays(-10)},
                    new Product(){ ProductName="Webcam", Price=200, Image= "product2_thumb.jpg", IsApproved= true, IsHome=false, IsFeatured=false, Description="ernjdfgıseırjdjkfnjkjkgnjerngerıogfnmdgıne", HtmlContext="<b>jkdfgjkdfng</b>",DateAdded=DateTime.Now.AddDays(-1) },
                    new Product(){ ProductName="Hand Bag", Price=500, Image= "product3.jpg", IsApproved= true, IsHome=true, IsFeatured=true , Description="ernjdfgıseırjdjkfnjkjkgnjerngerıogfnmdgıne", HtmlContext="<b>jkdfgjkdfng</b>",DateAdded=DateTime.Now.AddDays(-8)},
                    new Product(){ ProductName="Sofa", Price=3000, Image= "product4.jpg", IsApproved= true, IsHome=false, IsFeatured=false , Description="ernjdfgıseırjdjkfnjkjkgnjerngerıogfnmdgıne", HtmlContext="<b>jkdfgjkdfng</b>",DateAdded=DateTime.Now.AddDays(-70)}
                };
                context.Products.AddRange(products);

                var images = new[]
                {
                    new Image(){ ImageName="product1.jpg", Product= products[0]},
                    new Image(){ ImageName="product2.jpg", Product= products[0]},
                    new Image(){ ImageName="product3.jpg", Product= products[0]},
                    new Image(){ ImageName="product3.jpg", Product= products[0]},

                    new Image(){ ImageName="product4.jpg", Product= products[1]},
                    new Image(){ ImageName="product1.jpg", Product= products[1]},
                    new Image(){ ImageName="product2.jpg", Product= products[1]},
                    new Image(){ ImageName="product1.jpg", Product= products[1]},

                    new Image(){ ImageName="product2.jpg", Product= products[2]},
                    new Image(){ ImageName="product3.jpg", Product= products[2]},
                    new Image(){ ImageName="product1.jpg", Product= products[2]},
                    new Image(){ ImageName="product4.jpg", Product= products[2]},

                    new Image(){ ImageName="product2.jpg", Product= products[3]},
                    new Image(){ ImageName="product3.jpg", Product= products[3]},
                    new Image(){ ImageName="product1.jpg", Product= products[3]},
                    new Image(){ ImageName="product4.jpg", Product= products[3]},

                };
                context.Images.AddRange(images);

                var attributes = new[]
                {
                    new ProductAttribute(){ Attribute="Display", Value="15.6", Product= products[0]},
                    new ProductAttribute(){ Attribute="Processor", Value="Intel i7", Product= products[0]},
                    new ProductAttribute(){ Attribute="RAM Memory", Value="8 GB", Product= products[0]},
                    new ProductAttribute(){ Attribute="Hard Disk", Value="1 TB", Product= products[0]},
                    new ProductAttribute(){ Attribute="Color", Value="Black", Product= products[0]},

                    new ProductAttribute(){ Attribute="Display", Value="15.6", Product= products[1]},
                    new ProductAttribute(){ Attribute="Processor", Value="Intel i7", Product= products[1]},
                    new ProductAttribute(){ Attribute="RAM Memory", Value="8 GB", Product= products[1]},
                    new ProductAttribute(){ Attribute="Hard Disk", Value="1 TB", Product= products[1]},
                    new ProductAttribute(){ Attribute="Color", Value="Black", Product= products[1]},

                    new ProductAttribute(){ Attribute="Display", Value="15.6", Product= products[2]},
                    new ProductAttribute(){ Attribute="Processor", Value="Intel i7", Product= products[2]},
                    new ProductAttribute(){ Attribute="RAM Memory", Value="8 GB", Product= products[2]},
                    new ProductAttribute(){ Attribute="Hard Disk", Value="1 TB", Product= products[2]},
                    new ProductAttribute(){ Attribute="Color", Value="Black", Product= products[2]},

                    new ProductAttribute(){ Attribute="Display", Value="15.6", Product= products[3]},
                    new ProductAttribute(){ Attribute="Processor", Value="Intel i7", Product= products[3]},
                    new ProductAttribute(){ Attribute="RAM Memory", Value="8 GB", Product= products[3]},
                    new ProductAttribute(){ Attribute="Hard Disk", Value="1 TB", Product= products[3]},
                    new ProductAttribute(){ Attribute="Color", Value="Black", Product= products[3]},
                };
                context.ProductAttributes.AddRange(attributes);

                var categories = new[]
                {
                    new Category(){ CategoryName="Electronics"},
                    new Category(){ CategoryName="Accessories"},
                    new Category(){ CategoryName="Furniture"}
                };

                context.Categories.AddRange(categories);

                var productcategories = new[]
                {
                    new ProductCategory(){ Product=products[0],Category=categories[0]},
                    new ProductCategory(){ Product=products[1],Category=categories[0]},
                    new ProductCategory(){ Product=products[3],Category=categories[2]},
                    new ProductCategory(){ Product=products[2],Category=categories[1]}
                };
                context.AddRange(productcategories);

                context.SaveChanges();
            }
        }
    }
}
