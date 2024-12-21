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