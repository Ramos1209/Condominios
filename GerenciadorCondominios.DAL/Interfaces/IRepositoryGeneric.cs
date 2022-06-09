using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Interfaces
{
   public interface IRepositoryGeneric<Tentity> where Tentity: class
   {
       Task<IEnumerable<Tentity>> GetAll();
       Task<Tentity> GetbyId(int id);
       Task<Tentity> GetbyId(string id);

       Task Insert(Tentity entity);
       Task Update(Tentity entity);
       Task Excluir(Tentity entity);
       Task Excluir(int id);
       Task Excluir(string id);
    }
}
