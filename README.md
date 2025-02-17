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
A classe AppDbContext cumpre a função de dar o contexto de banco de dados para o fluxo da aplicação, permitindo a interação por transactions antes de efetivar as mudanças.

Os repositórios abrigam as operações de comunicação com o banco de dados. O repositório base contém tarefas comuns para diversos modelos da aplicação, enquanto o "UserRepository" implementa métodos específicos para a entidade usuário. 
 
#### Repository Pattern + Unit of Work
Neste projeto a camada de persistência utiliza o padrão de repositórios em conjunto com a estratégia do "unit of work", mas também seria possível adotar o padrão "active record". 

Trata-se de uma escolha para separar melhor a responsabilidade de executar as consultas no banco de dados, além de permitir maior controle de queries complexas com o "unit of work":
- O padrão repository serve para abstrair a camada de acesso a dados, ocultar a lógica na camada de aplicação;
- O uso do unit of work serve para realizar as consultas dos repositórios como uma transação única, evitando atualizações parciais em queries mais complexas.

Importante observar que o Entity Framework Core já possui um repositório embutido e uma unidade de trabalho através do "DbContext" e do "DbSet", mas a ideia é construir o projeto sem depender diretamente de um ORM específico. É possível, por exemplo, optar por utilizar o ADO ou o Dapper.

Também vale lembrar que cada abordagem possui os seus prós e contras, portanto se deve considerar os seguintes aspectos:
- Complexidade do projeto;
- Escalabilidade;
- Testabilidade;
- Padrões de equipe;
- Complexidade de transações.

#### Service Extension
Classe que serve para orquestrar a injeção de dependências do projeto, onde está a configuração de banco de dados e a ligação entre interfaces e classes que as implementam.

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
CQRS é um padrão de design que separa as operações de leitura e gravação de dados em modelos de dados separados. 

A sigla CQRS significa Command Query Responsibility Segregation, ou seja, Separação de Responsabilidades em Consultas e Comandos.

O CQRS pode ser usado para:
- Evitar problemas de inconsistência e incompatibilidade de informações;
- Melhorar o desempenho, a escalabilidade e a segurança de um aplicativo;
- Minimizar a concorrência de bloqueios;
- Separar as responsabilidades de leitura e escrita, resultando em modelos mais limpos e manuteníveis;
- Permitir que apenas as entidades ou operações de domínio apropriadas tenham permissão para executar ações de gravação nos dados.

#### Bibliotecas
Apesar do padrão CQRS poder ser aplicado sem o uso de bibliotecas, existem determinadas ferramentas que podem facilitar a aplicação desses princípios.
- MediatR (mediator): Serve para aplicar o padrão "mediator", inserindo uma camada intermediária entre os componentes e ajudando no aspecto de separação de responsabilidade entre comandos e queries;
- AutoMapper: Auxilia no mapeamento entre entidades e DTOs;
- FluentValidation: Simplifica a inserção de regras de validação nas requisições que entram na aplicação.

#### Use Cases
Para implementar os casos de uso da aplicação seguindo o CQRS, são utilizados "Commands", "Queries" e seus respectivos "Handlers".

Os "Commands" e "Queries" funcionam basicamente como DTOs, abrigando os dados necessários para que a ação seja executada. Ambas as classes **não devem possuir lógica de negócios**, mas apenas transportar dados.

Por outro lado, as classes "Handlers" têm a responsabilidade de receber os DTOs, validar os dados e executar as ações apropriadas no domínio.

Lembrando:
- Commands são solicitações para modificar o estado do sistema;
- Queries são solicitações para consulta de dados do sistema.

Nesta aplicação de exemplo, as interfaces "IRequest" e "IRequestHandler" do MediatR servem para facilitar a implementação dos "Commands", "Queries" e "Handlers":
- IRequest: representa uma solicitação, serve como DTO;
- IRequestHandler: define a assinatura do método "Handle" para processar a solicitação.

Exemplo: CreateUser
- CreateUserRequest: Representa o Command para a ação de criar um usuário;
- CreateUserResponse: Trata-se do conjunto de dados de resposta ao criar um usuário;
- CreateUserMapper: Realiza o mapeamento de dados entre objetos com estruturas diferentes (nesse caso, os DTOs e a entidade User);
- CreateUserValidator: Define regras de validação para o Command CreateUserRequest;
- CreateUserHandler: Contém a lógica de execução para criar um usuário.

#### Services
Contém a classe Service Extension, a qual injeta as dependências do projeto.

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
