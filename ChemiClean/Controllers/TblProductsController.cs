using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChemiClean.Models;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace WebApplication5.Controllers
{
    
    [Route("api/[controller]")]
    public class TblProductsController : Controller
    {
        private readonly ChemiCleanContext _context;

        public TblProductsController(ChemiCleanContext context)
        {
            _context = context;
        }

        // GET: TblProducts
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int page = 1)
        {
            var products = await _context.TblProducts.ToListAsync();

            int pageSize = 10;
            int maxPageNumber = products.Count / pageSize;

            if (page < 1 || page > maxPageNumber)
                page = 1;

            var pagedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToArray();

            return Ok(pagedProducts);
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] int page = 1)
        {
            var products = await _context.TblProducts.ToListAsync();

            int pageSize = 10;
            int maxPageNumber = products.Count / pageSize;

            if (page < 1 || page > maxPageNumber)
                page = 1;

            var pagedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToArray();

            return Ok(pagedProducts);
        }

        [HttpGet("PagesCount")]
        public async Task<IActionResult> PagesCount()
        {
            var products = await _context.TblProducts.ToListAsync();

            int pageSize = 10;
            int maxPageNumber = (int)Math.Ceiling((float)products.Count / pageSize);

            return Ok(maxPageNumber);
        }
    }
}
