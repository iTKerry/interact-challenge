# Build & Distribute

## Prerequisites 
- dotnet v6

To build / debug / publish source code you should be located at `src` folder with terminal.

## Arguments

- `file:#` - Represents path to file. # stands for number of the file if there is few of them
- `out` - Represents output folder path
```
InteractZipper.exe -file:1 /path/file1.csv -file:2 "/your path/file 2.csv" -out /result
```

## Debug

```
src> dotnet run --project InteractZipper -file:1 /path/file1.csv -file:2 "/your path/file 2.csv" -out /result
```

## Publish 

```
src> dotnet publish -c Release -r <runtime_identifier>
```


# Results

Zipped, resulting file could be found in the root folder by name `zipped_result.csv`

Zip process elapsed ~:
>00:00:07.9329011
