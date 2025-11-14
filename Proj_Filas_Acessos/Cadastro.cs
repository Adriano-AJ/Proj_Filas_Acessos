using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_Filas_Acessos
{
    public class Cadastro
    {
        private List<Usuario> usuarios;
        private List<Ambiente> ambientes;

        public List<Usuario> Usuarios { get => usuarios;}
        public List<Ambiente> Ambientes { get => ambientes;}

        public Cadastro()
        {
            this.usuarios = new List<Usuario>();
            this.ambientes = new List<Ambiente>();
        }

        public void adicionarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
        }

        public bool removerUsuario(Usuario usuario)
        {
            return usuarios.Remove(usuario);
        }

        public Usuario pesquisarUsuario(Usuario usuario)
        {
            return usuarios.Find(u => u.Id == usuario.Id);
        }

        public void adicionarAmbiente(Ambiente ambiente)
        {
            ambientes.Add(ambiente);
        }

        public bool removerAmbiente(Ambiente ambiente)
        {
            return ambientes.Remove(ambiente);
        }

        public Ambiente pesquisarAmbiente(Ambiente ambiente)
        {
            return ambientes.Find(a => a.Id == ambiente.Id);
        }

        public void upload()
        {

        }

        public void download()
        {

        }
    }
}
