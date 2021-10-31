# API RESTful

Desenvolva uma API RESTful para possibilitar a leitura da lista de indicados e vencedores da categoria Pior Filme do Golden Raspberry Awards.

# Cliente (Back-End)
Aplicação para retornar informações dos vencedores da categoria Pior Filme do Golden Raspberry Awards.

## Funcionalidades

 1. [ ] GET: [Rota: https://localhost:5001/api/MovieList/list] - Listar Todas as informações
 
Exemplo de Retorno:

[{"id":1,"year":"1980","title":"Can't Stop the Music","studios":"Associated Film Distribution","producers":"Allan Carr","winner":"yes"},{"id":2,"year":"2005","title":"Deuce Bigalow: European Gigolo","studios":"Columbia Pictures","producers":"Adam Sandler and Rob Schneider","winner":""}}]

 2. [ ] GET: [Rota: https://localhost:5001/api/MovieList/winners] - Listar Todos os Vencedores
 
 Exemplo de Retorno:
> [{"id":1,"year":"1980","title":"Can't Stop the Music","studios":"Associated Film Distribution","producers":"Allan Carr","winner":"yes"}]
 
 3. [ ] GET: [Rota: https://localhost:5001/api/MovieList] - Listar o Menor e o Maior intervalo entre dois prêmios.
 
Exemplo de Retorno:
>{"min":[{"id":87,"producer":"Wyck Godfrey, Stephenie Meyer and Karen Rosenfelt","interval":1,"previous_win":2011,"following_win":2012}],"max":[{"id":180,"producer":"Jerry Weintraub","interval":18,"previous_win":1980,"following_win":1998}]}

## Instruções:

Abra um terminal com o Windows PowerShell ou Git Bash, execute o comando abaixo:

git clone https://github.com/pedrohfk/MovieListAPI.git

## Como Executar:

Abra o terminal e acesse a pasta do projeto.

Digite: dotnet run

Abra seu API Cliente de preferência e execute  [https://localhost:5001/api/MovieList/](https://localhost:5001/api/MovieList/)
