using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_Filas_Acessos
{
    public class Ambiente
    {
        private int id;
        private string nome;
        private Queue<Log> logs;

        public int Id { get => id; }
        public string Nome { get => nome; set => nome = value; }
        public Queue<Log> Logs { get => logs; }

        public Ambiente(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
            this.logs = new Queue<Log>();
        }

        public void registrarLog(Log log)
        {
            logs.Enqueue(log);
        }
    }
}
