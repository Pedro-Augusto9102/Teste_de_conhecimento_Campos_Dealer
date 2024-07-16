### Instruções para rodar a aplicação:

- Clone este repositório e abra o projeto com o Visual Studio Code.
- Em seguida, abra o arquivo appsettings.json. No campo "ConnectionString.CamposDealer", modifique o nome do servidor (Server) com o nome do seu servidor sql. EX: 
-**"AllowedHosts": "*",
"ConnectionStrings": {
  "CamposDealer": "Server="*Seu Servidor SQL*";Database=Campos_Dealer_DB;Trusted_Connection=True;TrustServerCertificate=True"
}**
- Em seguida abra o console gerenciador de pacotes do NuGet e rode os comandos:
Add-Migrations "Migration inicial";
Update-Database

Agora Basta executar a aplicação.

Aplicação desenvolvida com: .Net Core MVC, Entity Framework, Microsoft SQL Server e Bootstrap.
