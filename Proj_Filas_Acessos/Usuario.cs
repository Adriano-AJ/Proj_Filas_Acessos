using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_Filas_Acessos
{
    public class Usuario
    {
        private int id;
        private string nome;
        private List<Ambiente> ambientes;

        public int Id { get => id;}
        public string Nome { get => nome; set => nome = value; }
        public List<Ambiente> Ambientes { get => ambientes;}

        public Usuario(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
            this.ambientes = new List<Ambiente>();
        }

        public bool concederPermissao(Ambiente ambiente)
        {
            return false;
        }
        public bool revogarPermissao(Ambiente ambiente)
        {
            return false;
        }
    }
}
