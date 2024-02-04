using Microsoft.AspNetCore.Mvc;
using ProductHandlerAPI.Models;
using ProductHandlerAPI.Data;

namespace ProductHandlerAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductHandlerController : ControllerBase
	{
		private readonly ApiContext _context;

		public ProductHandlerController(ApiContext context) 
		{
			_context = context;
		}

		// POST
		[HttpPost]
		public JsonResult Create(Product product)
		{
			// Check if product exists
			var productInDB = _context.Products.Find(product.Id);

			// If product exists, return 409
			if (productInDB != null) return new JsonResult(Conflict());

			// If product does not exist, add it to the database
			_context.Products.Add(product);

			// productInDB = product;

			// Save changes
			_context.SaveChanges();

			// Return the created product
			return new JsonResult(Ok(productInDB));
		}

		// PUT
		[HttpPut]
		public JsonResult Edit(Product product)
		{
			// Check if product exists
			var productInDB = _context.Products.Find(product.Id);

			// If product does not exist, return 404
			if (productInDB == null) return new JsonResult(NotFound());

			// If product exists, remove it from the database and add the updated product
			_context.Products.Remove(productInDB);
			_context.Products.Add(product);

			// productInDB = product;

			// Save changes
			_context.SaveChanges();

			// Return the updated product
			return new JsonResult(Ok(productInDB));
		}

		// GET
		[HttpGet]
		public JsonResult Get(int id)
		{
			// Check if product exists
			var result = _context.Products.Find(id);

			// If product does not exist, return 404
			if (result == null) return new JsonResult(NotFound());

			// If product exists, return it
			return new JsonResult(Ok(result));
		}

		// GET ALL
		[HttpGet]
		public JsonResult GetAll()
		{
			// Get all products
			var result = _context.Products.ToList();

			// Return all products
			return new JsonResult(Ok(result));
		}

		// DELETE
		[HttpDelete]
		public JsonResult Delete(int id)
		{
			// Check if product exists
			var product = _context.Products.Find(id);

			// If product does not exist, return 404
			if (product == null) return new JsonResult(NotFound());

			// If product exists, remove it from the database
			_context.Products.Remove(product);

			// Save changes
			_context.SaveChanges();

			// Return 200
			return new JsonResult(Ok());
		}
	}
}
