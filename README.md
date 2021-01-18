# Game Of Life

Game of life is Core.NET console app implementation of Convey Game of Life.

## Rules

1. Any live cell with fewer than two live neighbours dies, as if by underpopulation.
2. Any live cell with two or three live neighbours lives on to the next generation.
3. Any live cell with more than three live neighbours dies, as if by overpopulation.
4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

## Usage
Clone repository.
```bash
dotnet run "path to seed file"
```

## Example Seed file

Seed file contains initial pattern an entry point for the first generation.

* '*' living cells
* ' ' dead cells
* '#' boundary

```
############################
#                          #
#                          #
#                          #
#                          #
#           ***            #
#                          #
#                          #
#                          #
#                          #
############################

```
