using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edura.WebUI.Models;
using Edura.WebUI.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edura.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 2; //bir sayfada kaç ürün gözükecek
        private IProductRepository repository;
        public ProductController(IProductRepository _repo)
        {
            repository = _repo;
        }
        public IActionResult List(string category, int page=1 )
        {
            var products = repository.GetAll();
            if(!string.IsNullOrEmpty(category))
            {
                products = products
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .Where(i => i.ProductCategories.Any(a => a.Category.CategoryName == category));
                    //any mtd ile sorgudaki değer var mı diye bakıyoruz, true veya false dönüyor
            }
            int count = products.Count();
            products = products.Skip((page-1)*PageSize).Take(PageSize);
            //skip mtd öteleme işlemi yapıyoruz
            return View(
                new ProductListModel()
                {
                    Products = products,
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = count
                    }
                });
        }  
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            return View(
                repository.GetAll() //mtd ile Iqueryable tipindeki product nesnesi geliyor
                                    //getall ile vt tüm nesneleri çağırıyoruz ama yine de tam olarak gelmiyor hala filtreleme şansım var
                                    // dönüş değeri IQueryable değil de IEnumerable olsaydı veritabanından her şeyi çekmiş daha sonra filtreleme yapmış olacaktı
                .Where(i => i.ProductId == id) // tek sayfaya indirdim
                .Include(i => i.ProductCategories) // ProductCategory benim için category ile birleşim tablosu o yüzden iki tabloya gitmem gerekiyor 
                .ThenInclude(i => i.Category)
                .Include(i => i.ProductAttributes) //ekstra tabloları da ekliyorum
                .Include(i => i.Images) // örneğin image den başka tabloya gidecek olsaydım bir sonraki satırda thenInclude kullanmalıyım
                .Select(i => new ProductDetailsModel()
                {
                    Product = i,
                    ProductAttributes = i.ProductAttributes,
                    ProductImages = i.Images,
                    Categories =i.ProductCategories.Select(a=>a.Category).ToList()
                })
                .FirstOrDefault());
        }
    }
}