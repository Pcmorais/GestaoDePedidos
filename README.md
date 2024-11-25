# 📝 GestaoPedidos: Guia de Execução
## 📌 Descrição dos Projetos

  GestaoPedidos.Api: Projeto WebAPI com todos os endpoints documentados pelo Swagger. Fornece serviços de backend, incluindo CRUD para pedidos, clientes e produtos.

  GestaoPedidos.Razor: Projeto em Razor Pages que consome os endpoints da API para exibir e gerenciar dados através de uma interface web intuitiva.

## 🚀 Ordem de Execução

  Executar a WebAPI
  Inicie o projeto GestaoPedidos.Api primeiro. Isso é essencial, pois o GestaoPedidos.Razor depende da API para buscar dados.

  Executar a Interface Razor Pages
  Após iniciar a WebAPI, execute o projeto GestaoPedidos.Razor.

## 💾 Configuração do Banco de Dados e Migrations

Para configurar o banco de dados e aplicar as migrations, siga os passos abaixo:
Abra a solução GestaoPedidos.sln no Visual Studio ou via terminal.
Execute o seguinte comando no PowerShell ou terminal integrado:

    dotnet ef database update --project GestaoPedidos.Infra --startup-project GestaoPedidos.Api

Este comando aplicará todas as migrations pendentes, garantindo que o banco de dados esteja atualizado com a estrutura mais recente.

📝 GestaoPedidos: Guia de Execução
📌 Descrição dos Projetos

    GestaoPedidos.Api: Projeto WebAPI com todos os endpoints documentados pelo Swagger. Fornece serviços de backend, incluindo CRUD para pedidos, clientes e produtos.

    GestaoPedidos.Razor: Projeto em Razor Pages que consome os endpoints da API para exibir e gerenciar dados através de uma interface web intuitiva.

🚀 Ordem de Execução

    Executar a WebAPI
    Inicie o projeto GestaoPedidos.Api primeiro. Isso é essencial, pois o GestaoPedidos.Razor depende da API para buscar dados.

    Executar a Interface Razor Pages
    Após iniciar a WebAPI, execute o projeto GestaoPedidos.Razor.

## 🌐 URL da WebAPI

Após iniciar o projeto GestaoPedidos.Api, a API estará disponível na seguinte URL:

    https://localhost:7213

Certifique-se de que a API está rodando corretamente nesta URL antes de iniciar o projeto Razor Pages. A interface web consome dados desta URL, então qualquer interrupção pode afetar a funcionalidade.
