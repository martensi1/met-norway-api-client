# MET Norway API Client

Small and simple API client for the aviation weather REST API provided by [MET Norway](https://api.met.no/weatherapi/tafmetar/1.0/documentation#/).

Features:
 * Fetch METARs and TAFs

## Example
```csharp
using (var client = new MetNorwayClient())
{
	string metar = MetNorwayClient.FetchMetar("ESSA");
	string taf = MetNorwayClient.FetchTaf("ESSA");

	Console.WriteLine(metar);
	Console.WriteLine(taf);
}
```

## Installation
Install with NuGet:

```
dotnet add package PilotAppLibs.Clients.MetNorway
```

## License

This repository is licensed with the [MIT](LICENSE) license


## Author

Simon Mårtensson (martensi)
