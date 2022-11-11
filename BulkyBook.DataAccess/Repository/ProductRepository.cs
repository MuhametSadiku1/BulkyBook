using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    internal class ProductRepository: Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(x => x.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Author = obj.Author;
                objFromDb.CoverTypeId = obj.CoverTypeId;
                if(obj.ImageURL != null)
                {
                    objFromDb.ImageURL = obj.ImageURL;
                }
            }
        }
    }
}
