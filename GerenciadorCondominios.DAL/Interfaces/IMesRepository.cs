using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IMesRepository:IRepositoryGeneric<Mes>
    {
        new Task<IEnumerable<Mes>> PegarTodosMes();
    }
}
