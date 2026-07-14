# 🏋️ Sistema de Gestão de Academia

Sistema desenvolvido em **C# (.NET)** com foco na aplicação de conceitos de **Programação Orientada a Objetos**, **Entity Framework Core**, **MySQL**, **Padrões de Projeto** e **boas práticas de arquitetura de software**.

O projeto foi desenvolvido com fins de estudo para consolidar conhecimentos em desenvolvimento back-end utilizando a plataforma .NET.

---

## 📌 Funcionalidades

- Cadastro de alunos
- Consulta de alunos
- Atualização de dados cadastrais
- Remoção de alunos
- Cadastro de planos
- Gerenciamento de treinos
- Associação de treinos aos alunos
- Cadastro de exercícios
- Controle de pagamentos
- Validações de regras de negócio
- Tratamento de exceções personalizadas

---

## 🛠 Tecnologias Utilizadas

- C#
- .NET
- Entity Framework Core
- MySQL
- LINQ
- Docker (estrutura preparada)
- Git
- GitHub

---

## 🧱 Arquitetura do Projeto

O projeto foi organizado utilizando arquitetura em camadas:

```
SistemaAcademia
│
├── Classes
├── Repositories
├── Services
├── Records
├── Exceptions
├── Enums
├── Migrations
├── AppDataContext
└── Program
```

Aplicando conceitos como:

- Repository Pattern
- Separação de responsabilidades
- Baixo acoplamento
- Alta coesão
- Tratamento de exceções
- Programação Orientada a Objetos

---

## 🎯 Conceitos Aplicados

Durante o desenvolvimento foram utilizados conceitos como:

### Programação Orientada a Objetos

- Encapsulamento
- Abstração
- Herança
- Polimorfismo

### SOLID

- Single Responsibility Principle (SRP)
- Open/Closed Principle (OCP)
- Liskov Substitution Principle (LSP)
- Dependency Inversion Principle (DIP)

### Banco de Dados

- Entity Framework Core
- Migrations
- Relacionamentos entre entidades
- LINQ
- CRUD completo

### Tratamento de Exceções

- Exceções personalizadas
- Validações de domínio
- Tratamento centralizado das exceções

---

## ⚙️ Configuração

### Clone o repositório

```bash
git clone https://github.com/guigokw/SistemaAcademia.git
```

### Configure a Connection String

Edite o arquivo:

```
appsettings.json
```

e configure sua própria conexão com o banco de dados MySQL.

Ou utilize um arquivo:

```
appsettings.Development.json
```

(como recomendado para desenvolvimento).

---

## 🗄 Banco de Dados

Este projeto utiliza:

- MySQL
- Entity Framework Core

Após configurar a Connection String, execute as migrations para criar a estrutura do banco.

---

## 📚 Objetivo

O principal objetivo deste projeto é colocar em prática conceitos estudados durante a disciplina de Programação Orientada a Objetos e desenvolvimento back-end com .NET, simulando um sistema real de gerenciamento de academia.

---

## 🚀 Melhorias Futuras

- Injeção de Dependência (Dependency Injection)
- API REST com ASP.NET Core
- Autenticação JWT
- Testes Unitários
- Logging
- Paginação
- Soft Delete
- Docker Compose
- Interface Web

## 📖 O que aprendi com este projeto

Durante o desenvolvimento deste sistema pude consolidar conhecimentos sobre:

- Arquitetura em camadas
- Entity Framework Core
- Repository Pattern
- LINQ
- CRUD
- Relacionamentos entre entidades
- Tratamento de exceções
- Programação Orientada a Objetos
- Princípios SOLID

---

## 👨‍💻 Autor

Desenvolvido por **Luiz guilherme**.

GitHub:
https://github.com/guigokw