using ServiceDepartamentosEC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDepartamentosEC2.Repositories
{
    public interface IRepositoryDepartamentos
    {
        List<Departamento> GetDepartamentos();

        Departamento BuscarDepartamento(int id);
    }
}
