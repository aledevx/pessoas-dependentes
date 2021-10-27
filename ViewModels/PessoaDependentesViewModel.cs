using System.Collections.Generic;
using pessoa_dependentes_serverside.Models;

namespace pessoa_dependentes_serverside.ViewModels
{
    public class PessoaDependentesViewModel
    {
        public Pessoa Pessoas { get; set; }
        public ICollection<Dependente> Dependentes { get; set; }
    }
}