# Produtos API

## 1. Tema escolhido e objetivo da API

**Tema:** Catálogo de produtos de um e-commerce.

**Objetivo:** Disponibilizar uma Web API RESTful, construída em **ASP.NET Core (.NET 10)**, que permita gerenciar o
cadastro de produtos de uma loja virtual (criar, consultar, atualizar e remover produtos), com persistência
simulada em memória e documentação interativa via Swagger/OpenAPI.

## 2. Integrantes

| Nome completo | RM |
| Ana Clara Melo | RM 559021 |
| David Murillo de Oliveira Soares | RM 559078 |
| Lucas Serrano | RM555170 |
| Yasmin Gonçalves Coelho | RM 559147 |

## 3. Estrutura do projeto

```
produtos-api/
└── ProdutosApi/
    ├── Controllers/
    │   └── ProdutosController.cs   # Endpoints REST (herda de ControllerBase)
    ├── DTOs/
    │   └── ProdutoRequest.cs       # DTO de entrada (criação/atualização), sem Id
    ├── Models/
    │   └── Produto.cs              # Entidade de domínio
    ├── Data/
    │   └── AppDbContext.cs         # "Banco de dados" em memória (lista), Singleton
    ├── Program.cs                  # Configuração da aplicação, DI e Swagger
    └── ProdutosApi.csproj
```

## 4. Entidade de domínio

**Produto**

| Campo | Tipo | Descrição |
|---|---|---|
| `Id` | `int` | Identificador único, gerado pela API |
| `Nome` | `string` | Nome do produto |
| `Preco` | `decimal` | Preço de venda |
| `Categoria` | `string` | Categoria do produto |
| `Estoque` | `int` | Quantidade disponível em estoque |

O `Id` **não** é aceito no payload de entrada (`ProdutoRequest`): ele é sempre controlado pela API, tanto na
criação quanto na atualização.

## 5. Persistência em memória

A classe `Data/AppDbContext.cs` mantém uma `List<Produto>` em memória, simulando um banco de dados, e já vem
com 3 produtos de exemplo (seed) para facilitar os testes. Ela é registrada como **Singleton** no `Program.cs`:

```csharp
builder.Services.AddSingleton<AppDbContext>();
```

Isso garante que a mesma instância (e os mesmos dados) seja compartilhada por toda a aplicação durante sua
execução. Como é uma simulação em memória, os dados são perdidos ao reiniciar a aplicação.

## 6. Como executar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Conexão com a internet na primeira execução (para restaurar o pacote NuGet do Swagger)

### Passos

```bash
# 1. Clonar o repositório
git clone <URL_DO_REPOSITORIO>
cd produtos-api/ProdutosApi

# 2. Restaurar as dependências
dotnet restore

# 3. Executar a aplicação
dotnet run
```

Por padrão a API sobe em `http://localhost:5255` (ver `Properties/launchSettings.json`).
Ao acessar `http://localhost:5255/` você é redirecionado automaticamente para o Swagger.

> Se o `dotnet restore` falhar por algum problema de rede/proxy, rode `dotnet add package Swashbuckle.AspNetCore`
> dentro da pasta `ProdutosApi/` para reobter a referência do pacote e tente novamente.

## 7. Documentação Swagger/OpenAPI

Com a aplicação em execução, acesse:

```
http://localhost:5255/swagger
```

A interface do Swagger UI lista todos os endpoints, seus parâmetros, os DTOs de entrada/saída e permite executar
chamadas de teste diretamente pelo navegador ("Try it out").

O documento OpenAPI (JSON) fica disponível em:

```
http://localhost:5255/swagger/v1/swagger.json
```

## 8. Endpoints (CRUD)

Base route: `/api/v1/produtos`

| Verbo | Rota | Descrição | Sucesso | Erros |
|---|---|---|---|---|
| `GET` | `/api/v1/produtos` | Lista todos os produtos | `200 OK` | — |
| `GET` | `/api/v1/produtos/{id}` | Busca um produto pelo Id | `200 OK` | `404 Not Found` |
| `POST` | `/api/v1/produtos` | Cria um novo produto | `201 Created` | `400 Bad Request` |
| `PUT` | `/api/v1/produtos/{id}` | Atualiza um produto existente | `200 OK` | `400 Bad Request` / `404 Not Found` |
| `DELETE` | `/api/v1/produtos/{id}` | Remove um produto pelo Id | `204 No Content` | `404 Not Found` |

### Exemplos de chamadas (curl)

**Listar todos**
```bash
curl http://localhost:5255/api/v1/produtos
```

**Buscar por Id**
```bash
curl http://localhost:5255/api/v1/produtos/1
```

**Criar**
```bash
curl -X POST http://localhost:5255/api/v1/produtos \
  -H "Content-Type: application/json" \
  -d '{"nome":"Headset Gamer","preco":249.90,"categoria":"Áudio","estoque":15}'
```

**Atualizar**
```bash
curl -X PUT http://localhost:5255/api/v1/produtos/1 \
  -H "Content-Type: application/json" \
  -d '{"nome":"Teclado Mecânico RGB","preco":399.90,"categoria":"Periféricos","estoque":30}'
```

**Remover**
```bash
curl -X DELETE http://localhost:5255/api/v1/produtos/1
```

Também há um arquivo `ProdutosApi/ProdutosApi.http` pronto com todas essas requisições, para uso direto no
Visual Studio / VS Code (extensão REST Client).

### Exemplo de corpo de requisição (`ProdutoRequest`)

```json
{
  "nome": "Headset Gamer",
  "preco": 249.90,
  "categoria": "Áudio",
  "estoque": 15
}
```

### Exemplo de resposta (`Produto`)

```json
{
  "id": 4,
  "nome": "Headset Gamer",
  "preco": 249.90,
  "categoria": "Áudio",
  "estoque": 15
}
```

## 9. Prints dos testes no Swagger

- `docs/swagger-get-all.png`
- `docs/swagger-get-by-id.png`
- `docs/swagger-post.png`
- `docs/swagger-put.png`
- `docs/swagger-delete.png`
# CP1-CSharp-ProductsAPI
