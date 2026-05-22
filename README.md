# 📝 BlogPessoal — Backend

API RESTful para uma plataforma de blog pessoal, desenvolvida em **.NET 10** com autenticação JWT, banco de dados PostgreSQL e integração com **IA Generativa (Gemini)** para geração automática de resumos, tags e categorias.

---

## 🚀 Tecnologias

- [.NET 10](https://dotnet.microsoft.com/)
- [ASP.NET Core](https://learn.microsoft.com/aspnet/core)
- [Entity Framework Core](https://learn.microsoft.com/ef/core) + [Npgsql](https://www.npgsql.org/)
- [PostgreSQL](https://www.postgresql.org/)
- [JWT (JSON Web Token)](https://jwt.io/)
- [Google Gemini API](https://ai.google.dev/)
- [Swagger / OpenAPI](https://swagger.io/)

---

## 📋 Funcionalidades

- Cadastro e autenticação de usuários com JWT
- CRUD de postagens
- CRUD de temas
- Geração automática de **resumo**, **tags** e **categoria** via IA (Gemini)

---

## ⚙️ Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- Conta no [Google AI Studio](https://aistudio.google.com/) para obter a API Key do Gemini

---

## 🔧 Configuração

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/BlogPessoal.git
cd BlogPessoal
```

### 2. Configure o `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=blogpessoal;Username=postgres;Password=SUA_SENHA;"
  },
  "Jwt": {
    "Key": "SuaChaveSuperSecretaComMinimoDeVinteEQuatroCaracteres!",
    "Issuer": "BlogPessoal",
    "Audience": "BlogPessoalUsers"
  },
  "Gemini": {
    "ApiKey": "SUA_API_KEY_GEMINI"
  }
}
```

> ⚠️ Nunca suba credenciais reais para o repositório. Use variáveis de ambiente ou User Secrets em produção.

### 3. Aplique as migrations

```bash
dotnet ef database update
```

### 4. Execute o projeto

```bash
dotnet run
```

A API estará disponível em `http://localhost:5000`.

---

## 📖 Documentação da API

Acesse o Swagger em:

```
http://localhost:5000/swagger
```

### Autenticação

1. Faça login em `POST /api/usuario/login`
2. Copie o token retornado
3. Clique em **Authorize 🔒** no Swagger e cole o token
4. Todas as requisições autenticadas passarão o token automaticamente

---

## 🤖 Integração com IA

O endpoint `/api/ia/resumir` recebe um texto e retorna:

```json
{
  "resumo": "Resumo em até 2 frases",
  "tags": "tag1, tag2, tag3",
  "categoria": "Nome da categoria"
}
```

Utiliza o modelo `gemini-2.0-flash-lite` do Google Gemini.

> ℹ️ O plano gratuito do Gemini possui limites de requisições. Consulte [ai.google.dev/gemini-api/docs/rate-limits](https://ai.google.dev/gemini-api/docs/rate-limits) para mais detalhes.

---

## 🏗️ Estrutura do Projeto

```
BlogPessoal/
├── Config/          # Configurações (JWT, etc.)
├── Controllers/     # Endpoints da API
├── Data/            # DbContext e migrations
├── DTOs/            # Objetos de transferência de dados
├── Models/          # Entidades do banco de dados
├── Repositories/    # Acesso a dados
├── Services/
│   ├── IA/          # Integração com Gemini
│   └── ...          # Outros serviços
└── Program.cs       # Configuração da aplicação
```

---

## 🔍 Análise de Qualidade

O projeto utiliza [SonarQube](https://www.sonarqube.org/) para análise estática de código. Para rodar a análise:

```bash
dotnet sonarscanner begin /k:"blog-pessoal-dotnet" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="SEU_TOKEN"
dotnet build
dotnet sonarscanner end /d:sonar.token="SEU_TOKEN"
```

---

## 📄 Licença

Este projeto foi desenvolvido para fins educacionais no programa **Acelera Maker**.
