using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCondominios.ViewComponents
{
    public class VeiculoViewComponent: ViewComponent
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoViewComponent(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        public async Task<IViewComponentResult> InvoKeAsync(string id)
        {
            return View(await _veiculoRepository.TakeVeiculoPorUsuario(id));
        }
    }
}
