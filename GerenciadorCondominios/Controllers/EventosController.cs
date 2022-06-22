using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{


    public class EventosController : Controller
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public EventosController(IEventoRepository eventoRepository, IUsuarioRepository usuarioRepository)
        {
            _eventoRepository = eventoRepository;
            _usuarioRepository = usuarioRepository;
        }

        // GET: EventosController
        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepository.PegarUSuarioPeloNome(User);
            if (await _usuarioRepository.VerificaSeUsuarioExisteEmFuncao(usuario, "Morador"))
            {
                return View(await _eventoRepository.PegarPeloId(usuario.Id));
            }
            return View(await _eventoRepository.GetAll());
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Usuario usuario = await _usuarioRepository.PegarUSuarioPeloNome(User);
            Evento evento = new Evento
            {
                UsuarioId = usuario.Id
            };
            return View(evento);
        }

        // POST: EventosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento evento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _eventoRepository.Insert(evento);
                    TempData["NovoRegistro"] = $"Evento {evento.Nome} inserido com sucesso.";
                    return RedirectToAction(nameof(Index));
                }

                return View(evento);
            }
            catch
            {
                return View();
            }
        }

        // GET: EventosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Evento evento = await _eventoRepository.GetbyId(id);
            if (evento == null)
            {
                NotFound();
            }
            return View(evento);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Evento evento)
        {
            if (id != evento.EventoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _eventoRepository.Update(evento);
                TempData["Atualizado"] = $"Evento {evento.Nome} atualizado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }


        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _eventoRepository.Excluir(id);
            TempData["Excluir"]=$"Evento excluido";
            return Json("Evento excluido");
        }
    }
}
