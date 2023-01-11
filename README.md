Seguindo o padrão do repo abaixo, crie uma API contendo as operações CRUD dos seguintes modelos

https://github.com/torneseumprogramador/api-codigo-do-futuro/tree/dto

Modelos:

PessoaFisica
  - Id
  - Nome
  - Telefone
  - CPF
  - DataCriacao

PessoaJuridica
  - Id
  - Nome
  - Telefone
  - CNJP
  - DataCriacao

Orcamento
  - Id
  - ClienteId
  - FornecedorId
  - DescricaoDoServico
  - ValorTotal
  - DataCriacao
  - DataExpiração

dotnet new webapi