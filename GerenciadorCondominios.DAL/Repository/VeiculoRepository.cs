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
  public  class VeiculoRepository: RepositoryGeneric<Veiculo>, IVeiculoRepository
    {
        private readonly CondominioContext _context;
        public VeiculoRepository(CondominioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Veiculo>> TakeVeiculoPorUsuario(string usuarioId)
        {
            try
            {
                return await _context.Veiculos.Where(x => x.UsuarioId == usuarioId).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
