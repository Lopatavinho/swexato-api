# Swexato API - Entregáveis simples

## Requisitos cumpridos
- API em C# (.NET 8) com CRUD de Pessoas.
- Validação de CPF no cadastro (ex.: `CpfValidator.Validar`).
- Exemplos demonstrando violação e cumprimento dos princípios DRY e KISS.
- Banco de dados PostgreSQL rodando em container Docker.
- Tests unitários (xUnit) para validação de CPF.
- Scripts `curl` para evidências.

## Pré-requisitos
- .NET 8 SDK
- Docker Desktop
- Git e VS Code

## Como rodar (passo a passo)
1. Subir o PostgreSQL em container:
   ```bash
   docker-compose up -d
