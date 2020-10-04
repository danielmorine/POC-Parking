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

* Todos os Métodos do verbo GET, possui o final do endPoint: /format.{format}, você pode optar receber os dados em formato json ou xml, Exemplo:
			
			/api/v1/company/getall/format.json
			
			/api/v1/company/getall/format.xml
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
  
    Em caso de sucesso, será retornado Status 200.
  
  Além disso, o usuário com policy de administrator pode ainda: Editar um estabelecimento, Buscar todos os estabelecimentos e Deletar caso ainda não esteja em uso.
  
  ### Cadastro de usuário 
  Para criar um novo usuário, necessário estar autenticado com o usuário com policy administrator, necessário informar um cnpj já cadastrado, email e senha.
  
  Exemplo de Body:
  
  {
		 "cnpj" : "72768376000151",
		 "email" : "teste3@teste.com.br",
		 "password" : "Teste@123"
  }
  
    Em caso de sucesso, será retornado Status 200.
  
### Cadastro de veículos

  Para criar um novo veículo, necessário estar autenticado com o usuário com policy user.
  
  #### Importante
  * Você pode escolher entre dois tipos de typeID (1 ou 2).
  	** TypeID 1 = Carro;
	** TypeID 2 = Moto;

Exemplo de Body:
  
 {
		"make" : "Marca",
		"model" : "Modelo",
		"color" :"Cor",
		"plate" : "zty-1353",
		"typeID" : 1
}

    Em caso de sucesso, será retornado Status 200.
    
  Além disso, é possível listar todos os veículos, editar e apagar caso ainda não esteja sendo usado.
  
  ### Entrada de Veículos
  
  Para dar entrada de um veículo (carro ou moto), necessário estar autenticado com o usuário com policy user.
  
  #### Importante
  
  * O veículo precisa estar previamente cadastrado no estabelecimento (O id do usuário está vinculado a empresa);
  * Não ter atingido a quantidade a capacidade máxima de veículos.
  
  Enviar um body no verbo POST contendo a placa do veículo
  
  Exemplo de Body:
  
{
	"plate" : "zty-5424"
}

    Em caso de sucesso, será retornado Status 200.
    
    
   ### Saída de Veículos
   
   #### Importante

* É necessário ter dado entrada no veículo.

Enviar um body usando o verbo PUT.

  Exemplo de Body:
  
{
	"plate" : "zty-5424"
}

    Em caso de sucesso, será retornado Status 200.




