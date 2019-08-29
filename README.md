# SalesProcessor

Sistema de análise de dados de venda que irá importar lotes de arquivos e produzir
um relatório baseado em informações presentes no mesmo.
Existem 3 tipos de dados dentro dos arquivos e eles podem ser distinguidos pelo seu
identificador que estará presente na primeira coluna de cada linha, onde o separador de
colunas é o caractere “ç”.

### Dados do vendedor
Os dados do vendedor possuem o identificador 001 e seguem o seguinte formato:
001çCPFçNameçSalary

### Dados do cliente
Os dados do cliente possuem o identificador 002 e seguem o seguinte formato:
002çCNPJçNameçBusiness Area

### Dados de venda
Os dados de venda possuem o identificador 003 e seguem o seguinte formato:
003çSale IDç[Item ID-Item Quantity-Item Price]çSalesman name

### Exemplo de conteúdo total do arquivo:
001ç1234567891234çPedroç50000
001ç3245678865434çPauloç40000.99
002ç2345675434544345çJose da SilvaçRural
002ç2345675433444345çEduardo PereiraçRural
003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çPedro
003ç08ç[1-34-10,2-33-1.50,3-40-0.10]çPaulo

### Objetivos do sistema
O sistema deverá ler continuamente todos os arquivos dentro do diretório padrão
HOMEPATH\data\in e colocar o arquivo de saída em HOMEPATH\data\out.
No arquivo de saída o sistema deverá possuir os seguintes dados:
• Quantidade de clientes no arquivo de entrada
• Quantidade de vendedores no arquivo de entrada
• ID da venda mais cara
• O pior vendedor

## Instalação
O sistema é um Windows Service que pode ser utilizado via Visual Studio, como prompt, ou como serviço. Para este segundo cenário deve ser executado, como administrador do sistema, o arquivo batch localizado no diretório HOMEPATH\Setup.

## Funcionamento
O serviço será executado a cada 10s buscando arquivos no diretório HOMEPATH\data\in e, ao final do processamento, os dados coletados dos arquivos serão postados no diretório HOMEPATH\data\out.

Para evitar que o sistema busque os mesmos arquivos, após a execução bem sucedida o mesmo será movido do diretório HOMEPATH\data\in para o HOMEPATH\data\processed.

Caso ocorram algum erro na abertura do arquivo, por parte do serviço, o registro será movido do diretório HOMEPATH\data\in para o HOMEPATH\data\error.
