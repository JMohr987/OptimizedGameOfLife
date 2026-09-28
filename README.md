### Build and run
```bash
dotnet build
dotnet run --project src/OptimizedGameOfLife.Console/OptimizedGameOfLife.Console.csproj --graphics --input [filename]
```
## Running Notes
1. Both --graphics and --input are optional.
2. --graphics must be put before --input flag.
3. Run from the assginment's directory, not any of the subdirectories.

## Input File
1. Put the input file in the data directory.
2. File name is just the file's name, not the path.
3. Output file will be put in the data directory.

## Rules
1. Underpopulation: Any cell with fewer than two neighbors dies
2. Survival: Any live cell with two or three neighbors lives to the next generation
3. Overpopulation: Any live cell with more than three neighbors dies
4. Reproduction: Any dead cell with exactly three neighbors becomes a live cell

## How To Play
Create an input file with the following format
1. grid_width,grid_height
2. Number_of_generations
3. Number_of_live_cells
4. cell1_x,cell1_y
5. cell2_x,cell2_y
6. cell3_x,cell3_y
7. ...
8. cellN_x,cellN_y

## Tiles
1. Dead: "."
2. Alive: "#"
