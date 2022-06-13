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


# Estimates

## Prepare the solution
Create solution, project structure and prepare everything at `Program.cs` and `App.cs` files. 
> 30 min

## Main logic implementation
Read, Match, Zip and Write
> 2h 30m

## Write this readme
> 20-30 min


# Thoughts

## Overall
During the implementation I found a lot of tricky places where everything could go wrong. In case this is a quick solution with limited amount of time to solve a problem I took a decision to not handle all possible cases. (_i.e. invalid parameters, wrongly shaped lines in files, etc. etc._)

## Unit Testing
In case this is a quick solution that should solve problem right now, there is no unit tests. There could be integration tests, but it will require little bit more time. 

Also, for unit testing I can see only sense for 2 cases: Reading process, Merging lines. For that `FilesReader.cs` should be little bit refactored. But again, for current solution I don't think it has any sense.

## Lib usage
There was a limitation for lib usage. Mostly they were about reading and merging files. But I used default libs to create gerenic host and logging. Hope it is fine for this challenge.