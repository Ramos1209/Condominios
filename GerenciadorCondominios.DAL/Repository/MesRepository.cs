using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCondominios.DAL.Repository
{
   public class MesRepository: RepositoryGeneric<Mes>, IMesRepository
   {
       private readonly CondominioContext _context;
        public MesRepository(CondominioContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<IEnumerable<Mes>> PegarTodosMes()
        {
            return await _context.Meses.OrderBy(x => x.MesId).ToListAsync();
        }
    }
}
