using System;
using System.Linq;

namespace Proj_Filas_Acessos
{
    public static class Program
    {
        private static Cadastro cadastro = new Cadastro();

        private static void Main()
        {
            while (true)
            {
                MostrarMenu();
                var opc = LerInt("Escolha uma opção: ");

                switch (opc)
                {
                    case 0:
                        return;
                    case 1:
                        CadastrarAmbiente();
                        break;
                    case 2:
                        ConsultarAmbiente();
                        break;
                    case 3:
                        ExcluirAmbiente();
                        break;
                    case 4:
                        CadastrarUsuario();
                        break;
                    case 5:
                        ConsultarUsuario();
                        break;
                    case 6:
                        ExcluirUsuario();
                        break;
                    case 7:
                        ConcederPermissao();
                        break;
                    case 8:
                        RevogarPermissao();
                        break;
                    case 9:
                        RegistrarAcesso();
                        break;
                    case 10:
                        ConsultarLogs();
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Console.WriteLine("\nPressione Enter para continuar...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("=== Menu ===");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Cadastrar ambiente");
            Console.WriteLine("2. Consultar ambiente");
            Console.WriteLine("3. Excluir ambiente");
            Console.WriteLine("4. Cadastrar usuario");
            Console.WriteLine("5. Consultar usuario");
            Console.WriteLine("6. Excluir usuario");
            Console.WriteLine("7. Conceder permissão de acesso ao usuario");
            Console.WriteLine("8. Revogar permissão de acesso ao usuario");
            Console.WriteLine("9. Registrar acesso");
            Console.WriteLine("10. Consultar logs de acesso");
            Console.WriteLine();
        }

        private static int LerInt(string prompt)
        {
            int v;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out v))
            {
                Console.Write("Entrada inválida. " + prompt);
            }
            return v;
        }

        private static string LerString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }

        private static void CadastrarAmbiente()
        {
            var nome = LerString("Nome do ambiente: ");
            var id = cadastro.Ambientes.Count > 0 ? cadastro.Ambientes.Max(a => a.Id) + 1 : 1;
            var ambiente = new Ambiente(id, nome);
            cadastro.adicionarAmbiente(ambiente);
            Console.WriteLine($"Ambiente cadastrado: Id={ambiente.Id}, Nome={ambiente.Nome}");
        }

        private static void ConsultarAmbiente()
        {
            if (!cadastro.Ambientes.Any())
            {
                Console.WriteLine("Não há ambientes cadastrados.");
                return;
            }

            Console.WriteLine("Ambientes cadastrados:");
            foreach (var a in cadastro.Ambientes)
            {
                Console.WriteLine($"Id={a.Id}  Nome={a.Nome}  Logs={a.Logs.Count}");
            }
        }

        private static void ExcluirAmbiente()
        {
            if (!cadastro.Ambientes.Any())
            {
                Console.WriteLine("Não há ambientes para excluir.");
                return;
            }

            var id = LerInt("Id do ambiente a excluir: ");
            var temp = new Ambiente(id, "");
            var ambiente = cadastro.pesquisarAmbiente(temp);
            if (ambiente == null)
            {
                Console.WriteLine("Ambiente não encontrado.");
                return;
            }

            // Remover referências em usuários
            foreach (var u in cadastro.Usuarios)
            {
                u.Ambientes.RemoveAll(a => a.Id == ambiente.Id);
            }

            cadastro.removerAmbiente(ambiente);
            Console.WriteLine($"Ambiente Id={id} removido.");
        }

        private static void CadastrarUsuario()
        {
            var nome = LerString("Nome do usuário: ");
            var id = cadastro.Usuarios.Count > 0 ? cadastro.Usuarios.Max(u => u.Id) + 1 : 1;
            var usuario = new Usuario(id, nome);
            cadastro.adicionarUsuario(usuario);
            Console.WriteLine($"Usuário cadastrado: Id={usuario.Id}, Nome={usuario.Nome}");
        }

        private static void ConsultarUsuario()
        {
            if (!cadastro.Usuarios.Any())
            {
                Console.WriteLine("Não há usuários cadastrados.");
                return;
            }

            Console.WriteLine("Usuários cadastrados:");
            foreach (var u in cadastro.Usuarios)
            {
                var ambientes = u.Ambientes.Any() ? string.Join(", ", u.Ambientes.Select(a => $"{a.Id}:{a.Nome}")) : "Nenhum";
                Console.WriteLine($"Id={u.Id}  Nome={u.Nome}  Ambientes=[{ambientes}]");
            }
        }

        private static void ExcluirUsuario()
        {
            if (!cadastro.Usuarios.Any())
            {
                Console.WriteLine("Não há usuários para excluir.");
                return;
            }

            var id = LerInt("Id do usuário a excluir: ");
            var temp = new Usuario(id, "");
            var usuario = cadastro.pesquisarUsuario(temp);
            if (usuario == null)
            {
                Console.WriteLine("Usuário não encontrado.");
                return;
            }

            cadastro.removerUsuario(usuario);
            Console.WriteLine($"Usuário Id={id} removido.");
        }

        private static void ConcederPermissao()
        {
            var usuario = SelecionarUsuario();
            if (usuario == null) return;

            var ambiente = SelecionarAmbiente();
            if (ambiente == null) return;

            if (usuario.Ambientes.Any(a => a.Id == ambiente.Id))
            {
                Console.WriteLine("Permissão já concedida.");
                return;
            }

            usuario.Ambientes.Add(ambiente);
            Console.WriteLine($"Permissão concedida ao usuário {usuario.Nome} para o ambiente {ambiente.Nome}.");
        }

        private static void RevogarPermissao()
        {
            var usuario = SelecionarUsuario();
            if (usuario == null) return;

            var ambiente = SelecionarAmbiente();
            if (ambiente == null) return;

            var removed = usuario.Ambientes.RemoveAll(a => a.Id == ambiente.Id);
            if (removed > 0)
                Console.WriteLine($"Permissão revogada do usuário {usuario.Nome} para o ambiente {ambiente.Nome}.");
            else
                Console.WriteLine("Permissão não encontrada para esse usuário/ambiente.");
        }

        private static void RegistrarAcesso()
        {
            var usuario = SelecionarUsuario();
            if (usuario == null) return;

            var ambiente = SelecionarAmbiente();
            if (ambiente == null) return;

            var autorizado = usuario.Ambientes.Any(a => a.Id == ambiente.Id);
            var log = new Log(DateTime.Now, usuario, autorizado);
            ambiente.registrarLog(log);

            Console.WriteLine($"Acesso registrado: Usuário={usuario.Nome} Ambiente={ambiente.Nome} Resultado={(autorizado ? "Autorizado" : "Negado")}");
        }

        private static void ConsultarLogs()
        {
            var ambiente = SelecionarAmbiente();
            if (ambiente == null) return;

            Console.WriteLine("Filtrar logs: 0-Todos  1-Autorizados  2-Negados");
            var filtro = LerInt("Escolha: ");
            var logs = ambiente.Logs.ToArray().AsEnumerable();

            if (filtro == 1)
                logs = logs.Where(l => l.TipoAcesso);
            else if (filtro == 2)
                logs = logs.Where(l => !l.TipoAcesso);

            Console.WriteLine($"Logs do ambiente {ambiente.Nome}:");
            foreach (var l in logs)
            {
                Console.WriteLine($"{l.DtAcesso:yyyy-MM-dd HH:mm:ss}  Usuário={l.Usuario.Id}:{l.Usuario.Nome}  {(l.TipoAcesso ? "Autorizado" : "Negado")}");
            }
        }

        private static Usuario? SelecionarUsuario()
        {
            if (!cadastro.Usuarios.Any())
            {
                Console.WriteLine("Nenhum usuário cadastrado.");
                return null;
            }

            Console.WriteLine("Usuários:");
            foreach (var u in cadastro.Usuarios)
                Console.WriteLine($"Id={u.Id}  Nome={u.Nome}");

            var id = LerInt("Id do usuário: ");
            var temp = new Usuario(id, "");
            var usuario = cadastro.pesquisarUsuario(temp);
            if (usuario == null)
                Console.WriteLine("Usuário não encontrado.");
            return usuario;
        }

        private static Ambiente? SelecionarAmbiente()
        {
            if (!cadastro.Ambientes.Any())
            {
                Console.WriteLine("Nenhum ambiente cadastrado.");
                return null;
            }

            Console.WriteLine("Ambientes:");
            foreach (var a in cadastro.Ambientes)
                Console.WriteLine($"Id={a.Id}  Nome={a.Nome}");

            var id = LerInt("Id do ambiente: ");
            var temp = new Ambiente(id, "");
            var ambiente = cadastro.pesquisarAmbiente(temp);
            if (ambiente == null)
                Console.WriteLine("Ambiente não encontrado.");
            return ambiente;
        }
    }
}
