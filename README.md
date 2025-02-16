## Clean Architecture
### Objetivo do Repositório
Apresentar os conceitos da arquitetura limpa com um modelo simples, apenas para consulta e estudos.

### Conceito básico de Clean Arch
A Clean Architecture (Arquitetura Limpa) é um padrão de arquitetura de software que visa criar sistemas bem estruturados, altamente testáveis e de fácil manutenção, tendo o foco na separação de responsabilidades e na criação de camadas independentes.

Trata-se de um padrão com princípios semelhantes à arquitetura hexagonal (ports and adapters) e à arquitetura cebola, diferenciando-se na aplicação de determinadas regras.

![clean_arch_design](https://github.com/user-attachments/assets/b5018520-2112-4d0b-bee7-b7d9fcd8f393)

A ideia principal é proteger o domínio/core da aplicação, encapsulando as regras de negócio e as tornando independente de detalhes técnicos. 

Como é possível observar, as dependências devem seguir o fluxo da camada mais externa para a mais interna.

### Estrutura e Dependências
Foram criados 5 projetos dentro da solução para separar as camadas e responsabilidades da aplicação:
1. Core/Domain: Camada de domínio;
2. Core/Application: Camada de casos de uso;
3. Infrastructure/Persistence: Camada de infra da aplicação, mais especificamente para a persistência de dados;
4. Presentation/API: Camada de exibição/contato externo da aplicação;
5. Test/Tests: Projeto com os casos de teste para a aplicação.

Seguindo a ideia da arquitetura limpa apresentada anteriormente, o domínio não possui dependência dos demais projetos. As demais dependências são:
- Application -> domínio;
- Persistence -> Application;
- API -> Application + Persistence.

### Domínio
Nesta camada deve residir o core da aplicação:
- Entidades;
- Aggregates;
- Objetos de valor;
- Eventos de domínio;
- Enums;
- Constantes.

#### Entidades e Interfaces
Geralmente, as aplicações contêm entidades de domínio rico, mas para fins de estudo as classes serão entidades anêmicas. O mais importante é entender a separação das camadas e suas respectivas responsabilidades.

Neste projeto, foi criada uma classe abstrata "BaseEntity" para representar um padrão das entidades do domínio da aplicação, bem como uma classe "User" para extendê-la.

Seguindo a mesma linha de raciocínio, foram criadas interfaces para representar o básico da interação com repositórios. 

A ideia é ter uma interface de repositório base com as operações CRUD e implementar essa interface nos repositórios específicos de cada entidade.

### Infra: Persistência de dados
Parte da camada mais externa que contém a infraestrutura da aplicação, abarcando persistência de dados, observabilidade, mensageria, entre outros.
- Autenticação e Identidade;
- Armazenamento de arquivos e objetos;
- Fila e Mensageria;
- Serviços de notificação (email/sms, etc.);
- Serviços de logging;
- Serviços de pagamento;
- Logins sociais;
- Serviços externos em geral;

Quanto à persistência de dados, podemos ter:
- Data Context;
- Repositórios;
- Migrações;
- Data Seeding;
- Cache em memória;
- Cache.

#### Context e Repositories
 
#### Repository Pattern + Unit of Work
Neste projeto a camada de persistência utiliza o padrão de repositórios em conjunto com a estratégia do "unit of work", mas também seria possível adotar o padrão "active record". 

Trata-se de uma escolha para separar melhor a responsabilidade de executar as consultas no banco de dados, além de permitir maior controle de queries complexas com o "unit of work":
- O padrão repository serve para abstrair a camada de acesso a dados, ocultar a lógica na camada de aplicação;
- O uso do unit of work serve para realizar as consultas dos repositórios como uma transação única, evitando atualizações parciais em queries mais complexas.

Importante observar que o Entity Framework Core já possui um repositório embutido e uma unidade de trabalho através do "DbContext" e do "DbSet", mas a ideia é construir o projeto sem depender diretamente de um ORM específico. É possível, por exemplo, optar por utilizar o ADO ou o Dapper.

Também vale lembrar que cada abordagem possui os seus prós e contras, portanto deve-se considerar os seguintes aspectos:
- Complexidade do projeto;
- Escalabilidade;
- Testabilidade;
- Padrões de equipe;
- Complexidade de transações.

#### Service Extension

### Camada Application (Use Cases)
Nesta camada é onde as regras de negócio são combinadas para executar tarefas concretas, usando as entidades da camada de domínio.
- Abstrações;
- Contratos;
- Interfaces;
- Portas;
- Serviços do negócio;
- Comandos e Consultas (queries);
- Exceções;
- DTOs;
- Modelos de Request e Response;
- Mapeamento de DTOs;
- Validadores;
- Behaviors;
- Especificações.

#### Padrão CQRS

#### Bibliotecas
MediatR (mediator): 
Usado no projeto para ajudar na aplicação do padrão CQRS. 
Insere uma camada intermediária entre os componentes.


#### Use Cases

#### Services

#### Behavior

#### Exceptions

### Camada de apresentação
Parte da camada mais externa que consiste na interface com o cliente/usuário.
- Páginas Web;
- Componentes Web;
- APIs;
- Controllers;
- Views;
- Filtros;
- Atributos;
- Views Models;
- Estilização (Style Sheets);
- Javascript.

### Testes
