# CamelApp Backend API

## Áttekintés

A CamelApp API lehetőséget biztosít tevék kezelésére. Az API támogatja a CRUD (Create, Read, Update, Delete) műveleteket a tevékkel kapcsolatos adatok SQLite adatbázisban történő tárolásával. Az API .NET 8 környezetben készült, Entity Framework Core-ral az adatbázis-kezeléshez. A validációkat FluentValidation végzi, és az API célja, hogy egyszerű és könnyen skálázható backend szolgáltatásokat biztosítson a tevék kezeléséhez.

## Funkciók

- **CRUD Műveletek** tevékkel kapcsolatos adatok kezelésére:
  - Új teve hozzáadása.
  - Minden teve vagy egy konkrét teve lekérése ID alapján.
  - Teve adatok frissítése.
  - Teve törlése ID alapján.
  
- **Validáció** FluentValidation segítségével a teve létrehozása és frissítése előtt.
  
- **Swagger UI** az API végpontjainak tesztelésére.

- **CORS támogatás** a front-end alkalmazások számára.

- **SQLite adatbázis** a tevékkel kapcsolatos adatok tárolására.

## Telepítés

### Előfeltételek

- .NET SDK 8.0 vagy újabb.
- SQLite adatbázis.

### API futtatása helyben

1. Klónozd a repozitóriumot:
   ```bash
   git clone <repository-url>
   cd <repository-directory>
   dotnet restore
   dotnet build
   dotnet run