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

### Cadastro de estabelecimento 
  Para criar um novo estabelecimento, necessário estar autenticado com o usuário com policy administrator, enviando no header requisição o token obtido na autenticação e preenchendo da seguinte forma:
  Key: Authorization Value: Beaerer TOKEN_JWT
  

Cadastro de veículos





