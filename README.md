# Telegram Japanese Bot

Bot for learning foreign languages (especially Japanese)

## About
Bot uses ASP.Net Core 3.1 for hosting and Entity Framework Core for DB context and storing user data.
It was not nessesary for using EF Core, but i wanted to try latest EF build.

## Configuration
All settings stored in appsettings.json.

- ConnectionStrings:  
  DefaultConnection - Connection string to your DB  
- Telegram:  
  Token - Token for your Telegram Bot(you can get it from BotFather)  
- Proxy:  
  Host - IP for your Proxy(supports HTTP and HTTPS)  
  Port - Port for your Proxy  
- DictionaryResouces:  
  Wikidictionary - link for random page in Wikidictionary site  
  
## NuGet uses
- EF Core
- AngleSharp
- Autofac
- Telegram.Bot

## License 
MIT
