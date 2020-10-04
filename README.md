# POC-Parking
Projeto WebAPI em Asp.net Core 3.1

* Entity-FrameworkCore InMemory

* Autenticação JWT com Identity

* Swagger

## Informações
* O projeto conta com duas Policy: "ADMINISTRATOR" e "USER";

* JWT com duração de 60 minutos;

* Todas as controllers exigem autenticação;

* Policy administrator: admin@teste.com.br

* Policy user: usuario@teste.com.br

* Senha default: Teste@123

### Autenticação
Enviar um body do tipo JSON com o método POST na controller Login, com os seguintes campos: email e password.

{
	"email":"usuario@teste.com.br",
	"password" : "Teste@123"
}

    Em caso de sucesso, será retornado um token.

### Como preencher o Header da Requisição
  Key: Authorization
  
  Value: Bearer TOKEN_JWT

### Cadastro de estabelecimento 
  Para criar um novo estabelecimento, necessário estar autenticado com o usuário com policy administrator, 
  
  Exemplo de Body:
  
  {
	"name" : "park-estacionamentos",
	 "cnpj" : "72768376000151",
	 "address" : "Rua A 343",
	 "phone" : "33333333333",
	 "qtdCars" : 100
  }
  
    Em caso de sucesso, será retornar Status 200.
  
  Além disso, o usuário com policy de administrator pode ainda: Editar um estabelecimento, Buscar todos os estabelecimentos e Deletar caso ainda não esteja em uso.
  
  ### Cadastro de usuário 
  Para criar um novo usuário, necessário estar autenticado com o usuário com policy administrator, necessário informar um cnpj já cadastrado, email e senha.
  
  Exemplo de Body:
  
  {
		 "cnpj" : "72768376000151",
		 "email" : "teste3@teste.com.br",
		 "password" : "Teste@123"
  }
  
    Em caso de sucesso, será retornar Status 200.
  
Cadastro de veículos





