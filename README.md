# ConsultaEmpresa

## Descrição do Projeto
O **ConsultaEmpresa** é uma aplicação desenvolvida em .NET 9 que permite o gerenciamento de empresas e usuários. A aplicação utiliza autenticação JWT para proteger suas rotas e oferece endpoints para cadastro e listagem de empresas associadas a usuários.

## Funcionalidades
- **Autenticação JWT**: Proteção de rotas com validação de token.
- **Cadastro de Empresas**: Cadastro de empresas utilizando o CNPJ.
- **Listagem de Empresas**: Listagem de empresas associadas ao usuário autenticado.
- **Integração com ReceitaWS**: Busca de informações de empresas por CNPJ.

## Tecnologias Utilizadas
- **.NET 9**
- **Entity Framework Core**
- **Swagger** para documentação e teste de APIs(http://localhost:5059/swagger/index.html)
- **JWT** para autenticação
- **ReceitaWS** para consulta de dados de empresas

## Pré-requisitos
- .NET SDK 9.0 ou superior
- SQL Server configurado
- Visual Studio ou outro editor de código

## Instalação
1. Clone o repositório:
   
