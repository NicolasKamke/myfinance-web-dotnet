# MyFinanceWeb

## Descrição do Projeto

O MyFinanceWeb é um sistema de gerenciamento financeiro que permite aos usuários gerenciar planos de contas, transações financeiras e tipos de contas. O projeto foi desenvolvido como parte do trabalho prático da disciplina Práticas de Implementação e Evolução de Software.

## Arquitetura Utilizada

Este projeto adota uma arquitetura baseada no modelo cliente-servidor, desempenhando exclusivamente o papel de servidor, responsável por disponibilizar endpoints para consumo pelo frontend. A organização interna segue uma estrutura clara e modular, descrita abaixo:

- **Controllers**: Camada responsável por gerenciar as requisições HTTP. Os controladores processam as entradas do cliente e retornam respostas adequadas, interagindo com os serviços e outras camadas quando necessário.
- **Domain**: Contém os modelos de domínio e as entidades principais que definem os dados e as regras de negócio do sistema. Essa camada reflete o núcleo da aplicação, representando os conceitos fundamentais.
- **Infrastructure**: Abrange a configuração do banco de dados e o contexto do Entity Framework Core, incluindo mapeamentos e conexões com a base de dados. Essa camada é responsável por viabilizar a persistência e o acesso às informações.

## Tecnologias

As principais tecnologias utilizadas no projeto são:

- **ASP.NET Core**: Framework para construção de aplicações web.
- **Entity Framework Core**: ORM para acesso ao banco de dados.
- **SQL Server**: Banco de dados relacional.
- **Swagger**: Ferramenta para documentação de APIs.

## Configuração para Startup do Projeto

Para configurar e iniciar o projeto, siga os passos abaixo:

1. **Clone o repositório**:
   ```sh
   git clone https://github.com/seu-repositorio/myfinance-web-dotnet.git
   cd myfinance-web-dotnet
   ```
2. Configure a string de conexão: No arquivo `MyFinanceDbContext.cs`, configure a string de conexão para o seu banco de dados SQL Server:
   ```
   var connectionString = @"Server=host.docker.internal,1433;Database=master;User Id=SA;Password=Password_123#;TrustServerCertificate=True";
   ```
3. Crie as tabelas no banco de dados:
   1. Execute o script sql: sql\create_tables.sql
4. Adicione os dados de exemplo>
   1. Execute o script: sql\add_data.sql
5. Restaurar pacotes NuGet:
```sh
   dotnet restore
```
4. Iniciar o projeto:
   ```sh
   dotnet run
   ```
5. Acessar a aplicação: Abra o navegador e acesse http://localhost:5011/index.html para visualizar todos os endpoints disponiveis.

## Endpoints

### AccountTypeController

- **GET /AccountType/GetTypes**: Retorna uma lista de tipos de conta.
  - **Resposta de Sucesso**: `200 OK` com a lista de tipos de conta.
  - **Resposta de Falha**: `204 No Content` se não houver tipos de conta, `400 Bad Request` em caso de erro.

### AccountPlanController

- **POST /AccountPlan/AddPlan**: Adiciona um novo plano de conta.
  - **Parâmetros**: `AccountPlan` no corpo da requisição.
  - **Resposta de Sucesso**: `200 OK` se o plano for adicionado com sucesso.
  - **Resposta de Falha**: `409 Conflict` se o nome já estiver registrado, `400 Bad Request` em caso de erro.

- **GET /AccountPlan/GetPlans**: Retorna uma lista de planos de conta com paginação.
  - **Parâmetros**: `page` e `pageSize` como query parameters.
  - **Resposta de Sucesso**: `200 OK` com a lista de planos de conta.
  - **Resposta de Falha**: `400 Bad Request` em caso de erro.

- **GET /AccountPlan/GetAllPlans**: Retorna uma lista de todos os planos de conta.
  - **Resposta de Sucesso**: `200 OK` com a lista de todos os planos de conta.
  - **Resposta de Falha**: `400 Bad Request` em caso de erro.

- **PUT /AccountPlan/UpdatePlan**: Atualiza um plano de conta existente.
  - **Parâmetros**: `AccountPlan` no corpo da requisição.
  - **Resposta de Sucesso**: `200 OK` se o plano for atualizado com sucesso.
  - **Resposta de Falha**: `404 Not Found` se o plano não for encontrado, `400 Bad Request` em caso de erro.

- **DELETE /AccountPlan/DeletePlan**: Remove um plano de conta existente.
  - **Parâmetros**: `id` como query parameter.
  - **Resposta de Sucesso**: `200 OK` se o plano for removido com sucesso.
  - **Resposta de Falha**: `404 Not Found` se o plano não for encontrado, `400 Bad Request` em caso de erro.

### AccountTransactionController

- **GET /AccountTransaction/GetTransactions**: Retorna uma lista de transações de conta com paginação.
  - **Parâmetros**: `page` e `pageSize` como query parameters.
  - **Resposta de Sucesso**: `200 OK` com a lista de transações de conta.
  - **Resposta de Falha**: `400 Bad Request` em caso de erro.

- **PUT /AccountTransaction/UpdateTransactions**: Atualiza uma transação de conta existente.
  - **Parâmetros**: `AccountTransaction` no corpo da requisição.
  - **Resposta de Sucesso**: `200 OK` se a transação for atualizada com sucesso.
  - **Resposta de Falha**: `404 Not Found` se a transação não for encontrada, `400 Bad Request` em caso de erro.

- **POST /AccountTransaction/AddTransaction**: Adiciona uma nova transação de conta.
  - **Parâmetros**: `AccountTransaction` no corpo da requisição.
  - **Resposta de Sucesso**: `200 OK` se a transação for adicionada com sucesso.
  - **Resposta de Falha**: `400 Bad Request` em caso de erro.

- **DELETE /AccountTransaction/DeleteTransaction**: Remove uma transação de conta existente.
  - **Parâmetros**: `id` como query parameter.
  - **Resposta de Sucesso**: `200 OK` se a transação for removida com sucesso.
  - **Resposta de Falha**: `404 Not Found` se a transação não for encontrada, `400 Bad Request` em caso de erro.

- **GET /AccountTransaction/GetMonthlySummary**: Retorna um resumo mensal das transações.
  - **Parâmetros**: `month` e `year` como query parameters.
  - **Resposta de Sucesso**: `200 OK` com o resumo mensal das transações.
  - **Resposta de Falha**: `400 Bad Request` em caso de erro.