using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceDepartamentosEC2.Models;
using ServiceDepartamentosEC2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDepartamentosEC2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        IRepositoryDepartamentos repo;

        public DepartamentosController(IRepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public List<Departamento> GetDepartamentos()
        {
            return this.repo.GetDepartamentos();
        }

        [HttpGet("{id}")]
        public Departamento GetDepartamento(int id)
        {
            return this.repo.BuscarDepartamento(id);
        }
    }
}
