using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorCondominios.ViewComponents
{
    public class VeiculosViewComponent: ViewComponent
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculosViewComponent(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            return View(await _veiculoRepository.TakeVeiculoPorUsuario(id));
        }
    }
}
