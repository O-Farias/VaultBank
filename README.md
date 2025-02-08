# VaultBank - Sistema Bancário em C#

VaultBank é um sistema bancário desenvolvido em C# que simula operações bancárias básicas através de uma interface de console.

## Funcionalidades

- Criação de contas bancárias
- Depósitos
- Saques (limite diário de R$ 1.000,00)
- Transferências (limite de R$ 5.000,00 por operação)
- Consulta de saldo
- Extrato de transações
- Listagem de todas as contas

## Tecnologias Utilizadas

- .NET 8.0
- C# 
- xUnit (para testes unitários)
- Newtonsoft.Json

## Estrutura do Projeto

```
VaultBank/
├── Models/
│   ├── Account.cs
│   └── Transaction.cs
├── Services/
│   └── BankService.cs
├── Data/
│   └── BankDatabase.cs
├── Utils/
│   ├── DisplayHelper.cs
│   └── InputHelper.cs
├── Tests/
│   └── BankServiceTests.cs
└── Program.cs
```

## Como Executar

1. Clone o repositório:
```bash
git clone https://github.com/O-Farias/VaultBank.git
```

2. Entre no diretório do projeto:
```bash
cd VaultBank
```

3. Execute o projeto:
```bash
dotnet run
```

## Testes

Para executar os testes unitários:

```bash
dotnet test
```

## Limites de Operações

- Saque diário: R$ 1.000,00
- Transferência: R$ 5.000,00 por operação

## Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
