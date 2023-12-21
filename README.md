# payment-order-app

> Executar fechamento do ponto e emitir a ordem de pagamento. 

## Tecnologias:
- ASP.NET Core MVC (.NET 8)
- C# 
- .NET Core Native DI

## Componentes/Serviços:
- xUnit
- Bogus 
- FluentValidator
- Moq
- Swagger UI
- Csv Helper 30.0.1

## Arquitetura:
- Hexagonal Architecture
- Clean Architecture

## Princípios:
- Clean Code
- DDD - Domain Driven Design (Layers and Domain Model Pattern)
- S.O.L.I.D.

## Hospedagem
- IIS
  
# Como usar

**Pré requisitos**

- SDK do .NET Core 8 - pode ser baixado em [https://dot.net/core](https://dotnet.microsoft.com/pt-br/download);
- Visual Studio 2022 - pode ser baixado em [aqui](https://visualstudio.microsoft.com/pt-br/downloads/);

**Passo a passo**

1. Clone este projeto em sua máquina;
2. Através do Visual Studio execute o projeto pressionando F5;
3. No navegador acessar a url ```https://localhost:44335/PaymentOrder```

![image](https://github.com/anderson-araujo-cavalcante/payment-order-app/assets/133878123/8f82d086-5d36-47d3-950e-30f9453a7a21)

4. Selecionar os arquivos a serem processados: 

![image](https://github.com/anderson-araujo-cavalcante/payment-order-app/assets/133878123/39f7bcea-194b-4266-9959-5538829a1454)

Estes arquivos devem respeitar as seguintes regras:
```
Nome do arquivo deve conter: Nome do Departamento, Mês de vigência, Ano de vigência. 
Exemplo: ‘Departamento de Operações Especiais-Abril-2022.csv’
```
```
Os arquivos devem conter a seguinte configuração no header ```Código;Nome;Valor hora;Data;Entrada;Saída;Almoço```
```
```
O arquivo deve conter as seguintes colunas/tipo:
- Código: número
- Nome: Texto
- Valor hora: Dinheiro
- Data: Dia do registro
- Entrada: Hora do registro
- Saída: Hora do registro
- Almoço: Hora de registro 
```

5. Ao clicar em **Processar** será feito o upload dos arquivos para o servidor web;
6. Serão realizado dois processos: ```validação de dados inconsistentes na planilha``` e ```processamento dos dados``` 
Caso contenha algo inconsistente em alguma planilha seja retornado um aviso e não será seguirá com o processamento:

![image](https://github.com/anderson-araujo-cavalcante/payment-order-app/assets/133878123/73f7afe2-382b-4da5-9174-d8ee70f5bd9b)

No processamento será considerado as seguintes regras:

- Dias trabalhados. O trabalho segue de segunda a sexta e é esperado que o funcionário trabalhe 8 
horas por dia mais 1 hora de almoço. 
- Valor da hora da pessoa. 
- Os dias não trabalhados são descontados da pessoa. 
- Horas não trabalhadas são descontadas da pessoa. 
- Horas extras são pagas. 
- Dias extras são pagos.

Após a processamento será gerado um arquivo contendo as seguintes informações: 
- O valor pago por departamento. 
- Os dados da pessoa. 
- Valor pago a cada pessoa. 
- Valor descontado de cada pessoa. 
- Quantidade de horas extras ou horas faltantes. Quando as horas são faltantes Joyce coloca o valor 
negativo. 
- Quantidade de dias extras ou faltantes. Quando os dias são faltantes Joyce coloca o valor 
negativo. 

6. No final do processamento será disponibilizado um arquivo json:

![image](https://github.com/anderson-araujo-cavalcante/payment-order-app/assets/133878123/b22f18e1-9380-4742-af8a-857b06a8efac)

![image](https://github.com/anderson-araujo-cavalcante/payment-order-app/assets/133878123/ba05fc31-bc4c-4fbb-80f1-6183329b266b)


## Próximos passos

- Criar API REST com Asp.Net Web API Core para separar web x api
- Melhorar mensagem de retorno;
- Centralizar as mensagem de erros em um resource;
- Configurar docker compose para subir web x api;
- Medir desempenho com grande volume de dados;
 
