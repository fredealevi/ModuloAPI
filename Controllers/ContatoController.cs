using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Context;
using ModuloAPI.Entities;

namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ContatoController : ControllerBase
    {       
        // cria uma propriedade privada somente leitura que recebe o contexto da tabela agenda com nome de _context.
        private readonly AgendaContext _context;


        // O construtor da controler recebe como parametro o contexto da agenda em context
        // em seguida faz uma cópia desse context na propriedade somente leitura criada.
        public ContatoController(AgendaContext context)
        {
            // _context = banco
            _context = context;
        }
        
        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return Ok(contato);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
                // cria uma variavel contato que vai receber os dados do contato do id requisitado.
               var contato = _context.Contatos.Find(id); 

               if(contato == null)
                    return NotFound();

                return Ok(contato);
                //retona o endereço do contato criado
                //return CreatedAtAction(nameof(ObterPorId), new { id = contato.Id}, contato);
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));
            return Ok(contatos);
        }

        [HttpPut("{id}")]

        // o método atualizar contato recebe o id e as informações de contato por requisição 
        public IActionResult AtualizarContato(int id, Contato contatoAtualizado)
        {
            var contatoBanco = _context.Contatos.Find(id);

            if (contatoBanco == null)
                return NotFound();

            contatoBanco.Nome = contatoAtualizado.Nome;
            contatoBanco.Telefone = contatoAtualizado.Telefone;
            contatoBanco.Ativo = contatoAtualizado.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();    

            return Ok(contatoBanco);
        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var contatoBanco = _context.Contatos.Find(id);

            if (contatoBanco == null)
                return NotFound();

            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}