using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit

namespace TestGerenciadorCondominios
{
  public  class AlugueisTest :Test
    {
        [Fact]
        public async Task Deve_Retornar_Todos()
        {
            var service = Resolve<ITipoContatoAppService>();

            Guid _id = Guid.NewGuid();
            UsingDbContext(x => x.TipoContatos.Add(new TipoContato { Id = _id, Tipo = "TESTE" }));

            var retorno = await service.ObterTodos();

            retorno.Count.ShouldBe(1);
        }
    }
}
