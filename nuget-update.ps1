#nuget

#tools

rm -Recurse -Force nuget\tools\runtimes
rm nuget\tools\TypeGen.exe
copy -Recurse src\TypeGen\TypeGen.Cli\bin\Release\netcoreapp3.0\publish\* nuget\tools
mv nuget\tools\TypeGen.Cli.exe nuget\tools\TypeGen.exe

#lib

#netstandard1.3
copy src\TypeGen\TypeGen.Core\bin\Release\netstandard1.3\TypeGen.Core.dll nuget\lib\netstandard1.3
copy src\TypeGen\TypeGen.Core\bin\Release\netstandard1.3\TypeGen.Core.xml nuget\lib\netstandard1.3

#netstandard2.0
copy src\TypeGen\TypeGen.Core\bin\Release\netstandard2.0\TypeGen.Core.dll nuget\lib\netstandard2.0
copy src\TypeGen\TypeGen.Core\bin\Release\netstandard2.0\TypeGen.Core.xml nuget\lib\netstandard2.0

nuget pack nuget\TypeGen.nuspec
move TypeGen.2.4.7.nupkg nuget -force

if (Test-Path "local-nuget-path.txt") {
  $localNuGetPath = Get-Content "local-nuget-path.txt"
  copy nuget\TypeGen.2.4.7.nupkg $localNuGetPath
}


#nuget - dotnetcli


rm -Recurse -Force nuget-dotnetcli\tools\netcoreapp2.1\any\runtimes
copy -Recurse src\TypeGen\TypeGen.Cli\bin\Release\netcoreapp2.1\publish\* nuget-dotnetcli\tools\netcoreapp2.1\any

rm -Recurse -Force nuget-dotnetcli\tools\netcoreapp2.2\any\runtimes
copy -Recurse src\TypeGen\TypeGen.Cli\bin\Release\netcoreapp2.2\publish\* nuget-dotnetcli\tools\netcoreapp2.2\any

rm -Recurse -Force nuget-dotnetcli\tools\netcoreapp3.0\any\runtimes
copy -Recurse src\TypeGen\TypeGen.Cli\bin\Release\netcoreapp3.0\publish\* nuget-dotnetcli\tools\netcoreapp3.0\any

nuget pack nuget-dotnetcli\dotnet-typegen.nuspec
move dotnet-typegen.2.4.7.nupkg nuget-dotnetcli -force

if (Test-Path "local-nuget-path.txt") {
  $localNuGetPath = Get-Content "local-nuget-path.txt"
  copy nuget-dotnetcli\dotnet-typegen.2.4.7.nupkg $localNuGetPath
}
