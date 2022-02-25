using DAL3;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class HomeController : Controller
    {
        private ApplicationContext _dbContext;

        public HomeController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getWorkers")]
        public List<Worker> GetAllWorkers()
        {
            return _dbContext.Worker.ToList();
        }

        [HttpGet("getWorker/{WorkerID}")]
        public List<Worker> GetWorker(int WorkerID)
        {
            return _dbContext.Worker.Where(a => a.Id == WorkerID).ToList();
        }
        
        [HttpPost("addWorker")]
        public List<Worker> PostWorker([FromBody] Worker Name)
        {
            _dbContext.Add(Name);
            _dbContext.SaveChanges();
           return _dbContext.Worker.ToList();
        }
    }
}

