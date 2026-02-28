Assignment Manager

Sistema completo de gerenciamento de tarefas desenvolvido com ASP.NET Core Web API, SQLite e frontend em Bootstrap.

Visão Geral da Arquitetura:

A solução está organizada em três camadas principais:

Solution
│
├── Assignment.Data (Class Library)
│   ├── Entities
│   ├── Data (DbContext)
│   └── Repository
│
├── Assignment.API
│   ├── Controllers
│   ├── Services
│   └── Interfaces
│
└── wwwroot (Frontend)
    ├── index.html
    └── shared/app.js

Arquitetura em camadas:

Controller → Service → Repository → Database

Essa separação garante:

Baixo acoplamento

Organização clara de responsabilidades

Facilidade de manutenção e evolução

Backend
Assignment.Data (Class Library)

Responsável pela camada de dados.

Contém:

Entidades (Assignment, Status, Priority)

DbContext

Implementação do padrão Repository

Essa biblioteca encapsula toda a lógica de persistência.

Assignment.API

Responsável por:

Exposição de endpoints REST

Regras de negócio

Injeção de dependência

Configuração do banco e CORS

Camadas internas:

Controllers: Recebem requisições HTTP

Services: Implementam regras de negócio

Interfaces: Definem contratos para injeção

Repository: Acesso ao banco via Entity Framework Core

Banco de Dados

O projeto utiliza SQLite.

Motivos da escolha:

Banco leve e baseado em arquivo

Não requer servidor externo

Fácil portabilidade

O banco é gerado automaticamente via Entity Framework Core Migrations.

Arquivo criado:

assignments.db
Configuração do Banco
appsettings.json (API)
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=assignments.db"
  }
}

Program.cs (API)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
Migrations – Passo a Passo
1. Instalar pacotes necessários na API
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
2. Criar a Migration

Execute na raiz da solução:

dotnet ef migrations add InitialCreate \
--project Assignment.Data \
--startup-project Assignment.API
3. Aplicar no banco
dotnet ef database update \
--project Assignment.Data \
--startup-project Assignment.API

Esse processo irá:

Criar o arquivo assignments.db

Criar automaticamente as tabelas definidas nas entidades

Como Executar o Projeto
Backend
cd Assignment.API
dotnet run

Swagger disponível em:

https://localhost:PORT/swagger
Frontend

O frontend é servido pelo próprio backend via wwwroot.

Acesse:

https://localhost:PORT/index.html

Caso o frontend seja executado em outra porta, o CORS deve estar configurado na API.

CORS

No Program.cs da API:

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend",
        policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());
});

app.UseCors("Frontend");
Funcionalidades Implementadas

Criar tarefa

Listar tarefas

Filtro por status

Filtro por overdue

Busca por texto

Ordenação múltipla:

Atrasadas primeiro

Prioridade (High > Medium > Low)

DueDate mais próxima (nulos por último)

CreatedAt como critério de desempate

Regra de Negócio – Overdue

O campo Overdue é calculado dinamicamente:

public bool Overdue =>
    DueDate.HasValue && DueDate.Value < DateTime.UtcNow;

Ele não é persistido no banco, evitando inconsistência de dados.

Tecnologias Utilizadas

.NET 8

ASP.NET Core Web API

Entity Framework Core

SQLite

Bootstrap 5

JavaScript (Fetch API)

Decisões Técnicas

Uso de SQLite para simplificar infraestrutura

Separação em Class Library para isolar camada de dados

Service Layer para centralizar regras de negócio

Repository Pattern para abstração de persistência

Cálculo dinâmico de propriedades derivadas

Organização em camadas visando escalabilidade
