# 🔑 Projeto Acesso

Sistema de console para simulação de um controle de acesso corporativo, com gerenciamento de usuários, ambientes e um log detalhado de auditoria.

## 📄 Sobre o Projeto

Este projeto simula um sistema de controle de acesso para uma empresa. O objetivo é gerenciar quais usuários podem entrar em quais ambientes e, mais importante, registrar cada tentativa de acesso (seja ela autorizada ou negada) em um log de segurança para auditoria futura.

-----

## 🚀 Funcionalidades Principais

O sistema permite um gerenciamento completo dos pilares de um controle de acesso:

  * 🏢 **Gestão de Ambientes:**

      * Cadastrar novas áreas (salas de reunião, servidores, escritórios).
      * Consultar ambientes existentes.
      * Excluir ambientes.

  * 👤 **Gestão de Usuários:**

      * Adicionar novos usuários ao sistema.
      * Consultar usuários cadastrados.
      * Excluir usuários (seguindo regras de negócio).

  * 🛡️ **Controle de Permissões:**

      * Conceder acesso de um usuário a um ambiente específico.
      * Revogar o acesso de um usuário a um ambiente.

  * 🖥️ **Simulação e Auditoria:**

      * Registrar uma tentativa de acesso (informando usuário e ambiente) para testar as permissões.
      * Consultar o histórico de logs de um ambiente, com filtros para:
          * ✅ Acessos Autorizados
          * ❌ Acessos Negados
          * 🔄 Todos os Registros

  * 💾 **Persistência de Dados:**

      * Os dados são **carregados automaticamente** ao iniciar a aplicação.
      * Os dados são **salvos automaticamente** ao encerrar a aplicação.

-----

## 📋 Regras de Negócio Implementadas

Para garantir a integridade e o realismo do sistema, as seguintes regras foram implementadas:

1.  **Log Rotativo:** Cada ambiente armazena no máximo os **100 últimos logs**. Ao atingir o limite, o registro mais antigo é automaticamente descartado para dar lugar ao novo (FIFO - *First-In, First-Out*).
2.  **Permissão Única:** Um usuário só pode ter **uma permissão por ambiente**. O sistema não permite conceder a mesma permissão duas vezes.
3.  **Segurança na Remoção:** Um usuário **não pode ser removido** do sistema se ele ainda possuir permissões de acesso ativas em qualquer ambiente. É preciso revogar todas as suas permissões primeiro.

-----

## ▶️ Como Usar

Ao executar a aplicação, você será recebido por um menu interativo que guia todas as operações:

> **Menu Principal:**
>
> 0.  Sair
> 1.  Cadastrar ambiente
> 2.  Consultar ambiente
> 3.  Excluir ambiente
> 4.  Cadastrar usuario
> 5.  Consultar usuario
> 6.  Excluir usuario
> 7.  Conceder permissão de acesso ao usuario
> 8.  Revogar permissão de acesso ao usuario
> 9.  Registrar acesso
> 10. Consultar logs de acesso

-----

## 🛠️ Como Executar o Projeto

1.  Clone o repositório:

    ```bash
    git clone https://github.com/seu-usuario/PROJETO-ACESSO.git
    ```

2.  Acesse o diretório do projeto:

    ```bash
    cd PROJETO-ACESSO
    ```

3.  Compile e execute a aplicação:

    ```bash
    dotnet run
    ```
