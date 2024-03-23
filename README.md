# Projeto-API-Vendas

⭐ API de Gestão de Estoque e Vendas
- Bem-vindo(a) à documentação da API de Gestão de Estoque e Vendas! Esta API foi desenvolvida para auxiliar no gerenciamento de estoque e processos de vendas. Com ela, você pode realizar operações relacionadas a produtos, estoque, clientes e vendas. Este documento fornece informações essenciais para começar a utilizar a API em seus projetos.

🔷 Conteúdo
1. Visão Geral
2. Configuração
3. Níveis de Acesso
4. Endpoint
   - Estoque
   - Clientes
   - Vendas
   
🔷 Visão Geral
- A API de Gestão de Estoque e Vendas oferece funcionalidades para facilitar o gerenciamento de estoque e vendas de um negócio. Ela permite a adição, remoção e atualização de produtos, controle de estoque, cadastro de clientes.

🔷 Configuração
- Clone este repositório para o seu ambiente local.
- Abra a solução em um ambiente de desenvolvimento.
- Defina como projeto de inicialização a UI e a API.
- Dentro do projeto da API, mude o arquivo appsettings.json para o caminho das pastas que se encontra o Desafio_Processo.db dentro de sua maquina, para assim fazer conexão com banco de dados.

🔷 Níveis de Acesso
- Para acessar a aplicação existem dois atores, que são:
   + Administrador - Login: Administrador@gmail.com / Senha: 12345
   + Cliente - Tanto pode cadastrar um novo, quanto usar Login: teste@gmail.com / senha: 123
- Todos os cadastros feitos na aplicação por enquanto, estão entrando como clientes, existe apenas 1 perfil de administrador para gerir o estoque e ser mostrado para os clientes.

🔷 Endpoints
- A API oferece os seguintes endpoints principais:
  🔘Produtos:
  + GET Loja/PesquisaProdutos: Retorna a lista de produtos disponíveis.
  + POST Loja/CadastrarProduto: Adiciona um novo produto.
  + PUT Loja/AtualizaProd: Atualiza os detalhes de um produto existente.
  + DELETE Loja/ExcluirProd: Remove um produto do sistema.
  
  🔘Clientes/Cadastros:
  + GET Loja/PesquisaUsuario: Retorna a lista de clientes cadastrados.
  + POST Loja/CadastrarUsuario: Cadastra um novo cliente.
  
  🔘Vendas:
  + Loja/BaixaProdutos: Dá baixa nos produtos em estoque e registra uma nova venda.

🔷 Contato
- Se você tiver alguma dúvida ou precisar de suporte, entre em contato conosco em mthiago299@gmail.com
"# ProjetoIntegradorEniac" 
"# ProjetoIntegradorEniac" 
