# Pizza API

<p>Projeto POC para demonstração de conhecimentos, construído usando Aspnet Core.</p>
<p>Base de dados em memória. </p>
<p>Possui módulo de testes.</p>

# Utilização


# Envio do Pedido
## url: /api/pedido/ 
## método: POST
## descrição: Endpoint para envio dos pedidos
## json com usuário sem dados de entrega:
{
   "itens":[
      {
         "idProduto1":1
      }
   ],
   "idUsuario":1
}

## json com usuário sem dados de entrega e pizza de 2 sabores:
{
   "itens":[
      {
          "idProduto1":1,
         "idProduto2":2
      }
   ],
   "idUsuario":1
}


## json sem usuário com dados de entrega:
{
   "itens":[
      {
         "idProduto1":1
      }
   ],
   "enderecoDto":{
      "logradouro":"Rua da Lapa ",
      "numero":"300",
      "complemento":"apto 109"
   }
}

## json sem usuário com dados de entrega e pizza de 2 sabores:
{
   "itens":[
      {
         "idProduto1":1,
         "idProduto2":2
      }
   ],
   "enderecoDto":{
      "logradouro":"Rua da Lapa ",
      "numero":"300",
      "complemento":"apto 109"
   }
}

# Resposta
{
    "id": 1,
    "data": "2020-10-14T11:31:53.132254-03:00",
    "valor": 59.99,
    "observacao": null,
    "itens": [
        {
            "id": 1,
            "nome": "Calabresa X POrtuguesa",
            "valor": 0.0,
            "idProduto1": 1,
            "produto1": {
                "id": 1,
                "nome": "Calabresa",
                "valor": 59.99
            },
            "idProduto2": 2,
            "produto2": {
                "id": 2,
                "nome": "POrtuguesa",
                "valor": 59.99
            },
            "total": 59.99
        }
    ],
    "idUsuario": null,
    "usuario": null,
    "idEndereco": 2,
    "endereco": {
        "id": 2,
        "logradouro": "Rua da Lapa ",
        "numero": "300",
        "complemento": "apto 109"
    }
}

# Consulta de Pedido Por Id do Pedido
## url: /api/pedido/{id} 
## método: GET
## descrição: Endpoint para connsulta de pedido específico 
# Resposta
{
    "id": 1,
    "data": "2020-10-14T11:31:53.132254-03:00",
    "valor": 59.99,
    "itens": [
        {
            "id": 1,
            "nome": "Calabresa X Portuguesa",
            "valor": 0.0,
            "idProduto1": 1,
            "produto1": {
                "id": 1,
                "nome": "Calabresa",
                "valor": 59.99
            },
            "idProduto2": 2,
            "produto2": {
                "id": 2,
                "nome": "POrtuguesa",
                "valor": 59.99
            },
            "total": 59.99
        }
    ],
    "idUsuario": null,
    "usuario": null,
    "idEndereco": 2,
    "endereco": null
}

# Consulta de Pedido Por Id do Usuário
## url: /api/pedido/usuario/{id} 
## método: GET
## descrição: Endpoint para connsulta dos pedidos do usuário 
# Resposta
[
    {
        "id": 2,
        "data": "2020-10-14T11:37:54.255302-03:00",
        "valor": 59.99,
        "itens": [
            {
                "id": 2,
                "nome": "Calabresa X Portuguesa",
                "valor": 0.0,
                "idProduto1": 1,
                "produto1": {
                    "id": 1,
                    "nome": "Calabresa",
                    "valor": 59.99
                },
                "idProduto2": 2,
                "produto2": {
                    "id": 2,
                    "nome": "POrtuguesa",
                    "valor": 59.99
                },
                "total": 59.99
            }
        ],
        "idUsuario": 1,
        "usuario": null,
        "idEndereco": 3,
        "endereco": null
    }
]


# Cadastro de Usuário
## url: /api/usuario/ 
## método: POST
## descrição: Endpoint para cadastrar novos usuários 
## json:
{
   "nome":"Amanda",
   "endereco":{
      "logradouro":"Rua da Lapa ",
      "numero":"300",
      "complemento":"apto 109"
   }
}

# Resposta
{
    "id": 1,
    "nome": "Amanda",
    "idEndereco": 1,
    "endereco": {
        "id": 1,
        "logradouro": "Rua da Lapa ",
        "numero": "300",
        "complemento": "apto 109"
    }
}

# Buscar de Usuário por Id do Usuário
## url: /api/usuario/{id} 
## método: GET
## descrição: Endpoint buscar usuário específico 
# Resposta
{
    "id": 1,
    "nome": "Amanda",
    "idEndereco": 1,
    "endereco": {
        "id": 1,
        "logradouro": "Rua da Lapa ",
        "numero": "300",
        "complemento": "apto 109"
    }
}


# Cadastro de Produtos
## url: /api/produto/ 
## método: POST
## descrição: Endpoint para cadastrar novos usuários 
## json:
{
   "nome":"Portuguesa",
   "valor":59.99
}

# Resposta
{
    "id": 2,
    "nome": "Portuguesa",
    "valor": 59.99
}

# Listar Todos os Produtos 
## url: /api/produto
## método: GET
## descrição: Endpoint listar todos os produtos  
# Resposta
[
    {
        "id": 1,
        "nome": "Calabresa",
        "valor": 59.99
    },
    {
        "id": 2,
        "nome": "Portuguesa",
        "valor": 59.99
    }
]


