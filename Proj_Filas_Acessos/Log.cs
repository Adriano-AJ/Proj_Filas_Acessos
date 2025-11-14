using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_Filas_Acessos
{
    public class Log
    {
        private DateTime dtAcesso;
        private Usuario usuario;
        private bool tipoAcesso;

        public DateTime DtAcesso { get => dtAcesso; }
        public Usuario Usuario { get => usuario; }
        public bool TipoAcesso { get => tipoAcesso; }

        public Log(DateTime dtAcesso, Usuario usuario, bool tipoAcesso)
        {
            this.dtAcesso = dtAcesso;
            this.usuario = usuario;
            this.tipoAcesso = tipoAcesso;
        }
    }
}
