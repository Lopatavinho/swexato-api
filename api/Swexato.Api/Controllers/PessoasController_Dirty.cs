using Microsoft.AspNetCore.Mvc;
using Swexato.Api.Data;
using Swexato.Api.Models;
using System.Text.RegularExpressions;
using System.Linq;

namespace Swexato.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/dirty")]
    public class PessoasController_Dirty : ControllerBase
    {
        private readonly AppDbContext _db;
        public PessoasController_Dirty(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> CriarDirty([FromBody] Pessoa p)
        {
            var cpf = Regex.Replace(p.CPF ?? "", @"\D", "");
            if (cpf.Length != 11) return BadRequest(new { error = "CPF inválido (length)" });
            if (cpf.Distinct().Count() == 1) return BadRequest(new { error = "CPF inválido (repetido)" });

            int[] nums = cpf.Select(c => int.Parse(c.ToString())).ToArray();
            bool ok1=false;
            {
                int sum=0; int w=10;
                for(int i=0;i<9;i++){ sum += nums[i]*w; w--; }
                int r = sum%11; int d = r<2?0:11-r;
                if (d==nums[9]) ok1=true;
            }
            bool ok2=false;
            {
                int sum=0; int w=11;
                for(int i=0;i<10;i++){ sum += nums[i]*w; w--; }
                int r = sum%11; int d = r<2?0:11-r;
                if (d==nums[10]) ok2=true;
            }
            if (!ok1 || !ok2) return BadRequest(new { error = "CPF inválido (digits)" });

            p.CPF = cpf;
            _db.Pessoas.Add(p);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(CriarDirty), new { id = p.Id }, p);
        }
    }
}
