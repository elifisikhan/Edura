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
        private IProductRepository repository;
        public ProductController(IProductRepository _repo)
        {
            repository = _repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            return View(
                repository.GetAll() //mtdd ile Iqueryable tipindeki product nesnesi geliyor
                                    //getall ile vt tüm nesneleri çağırıyoruz ama yine de tam oalrak gelmiyor hala filtreleme şansım var
                                    // dönüş değeri IQueryable değil de IEnumerable olsaydı veritabanından her şeyi çekmiş daha sonra filtreleme yapmış olacaktı
                                    // bu mtd ile veritabanından tüm product nesnelerini almış olsak da önce filtreden geçiriyoruz
                .Where(i => i.ProductId == id) // tek sayfaya indirdim
                .Include(i => i.ProductCategories) // ProductCategory benim için category ile birleşim tablosu o yüzden iki tabloya gitmem gerekiyor 
                .ThenInclude(i => i.Category)
                .Include(i => i.ProductAttributes) //ekstra tabloları da ekliyorum
                .Include(i => i.Images) // örneğin image den başka tabloya gidecek olsaydım bir sonraki satırda then ınclude kullanmalıyım
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