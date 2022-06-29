using System;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using GerenciadorCondominios.DAL.Repository;
using Xunit;

namespace ComdominiosTest
{
    public class Alugueis
    {
        private readonly IAluguelRepository _aluguelRepository;
        public Alugueis(IAluguelRepository aluguelRepository)
        {
            _aluguelRepository = aluguelRepository;
        }
        [Fact]
        public async Task Deve_Retornar_Todos()
        {
            var _id = Guid.NewGuid().ToString();
            var _aluguels = await _aluguelRepository.GetbyId(_id);
            Aluguel alu = new Aluguel
            {
                Valor = 250,
                //Mes ="Janeiro",
                Ano = 2021

            };


            Assert.Collection(_aluguels);


        }
    }
}
