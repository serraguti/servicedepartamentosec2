using ServiceDepartamentosEC2.Data;
using ServiceDepartamentosEC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDepartamentosEC2.Repositories
{
    public class RepositoryDepartamentos : IRepositoryDepartamentos
    {
        private DepartamentosContext context;

        public RepositoryDepartamentos(DepartamentosContext context)
        {
            this.context = context;
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.context.Departamentos.ToList();
        }

        public Departamento BuscarDepartamento(int id)
        {
            return this.context.Departamentos
                .SingleOrDefault(x => x.IdDepartamento == id);
        }
    }
}
