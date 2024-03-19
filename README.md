# GDUPA
Gdupa is a project used for warehouse import management and product management.

## Prerequisites
* .NET SDK 7.0

# Installation


## Clone this repository
```bash
git clone https://github.com/tranduckhuy/Gdupa-web-mvc.git
```

# Initialize the database
## Setup database location
Change the path of WarehouseDB in appsettings.js to specify the path to where you want to save the Database
#### Example:
```bash
"ConnectionStrings": {
    "WarehouseDB": "Data Source=D:\\Warehouse.db"
}
```

## Update database
#### .NET CLI (Terminal)
```bash
dotnet ef database update
```
#### Or with Package Manager Console
```bash
Update-Database
```

# Run the app
```bash
dotnet run
```

# Open http://localhost:5158 in your browser
open http://localhost:5158

### Our Contributors ✨
<a href="https://github.com/tranduckhuy/Gdupa-web-mvc/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=tranduckhuy/Gdupa-web-mvc" />
</a>
