@echo off
md artifacts\nuget 2>nul
nuget pack src\IharBury.Algorithms\IharBury.Algorithms.csproj -Build -Prop Configuration=Release -OutputDirectory artifacts\nuget