### Build and run
```bash
dotnet build
dotnet run --project src/GameOfLife.Console/GameOfLife.Console.csproj --graphics --input [filename]
```
## Running Notes
Both --graphics and --input are optional
--graphics must be put before --input flag
Run from the assginment's directory, not any of the subdirectories

## Input File
Put the input file in the data directory
File name is just the file's name, not the path
Output file will be put in the data directory

## Rules
1. Underpopulation: Any cell with fewer than two neighbors dies
2. Survival: Any live cell with two or three neighbors lives to the next generation
3. Overpopulation: Any live cell with more than three neighbors dies
4. Reproduction: Any dead cell with exactly three neighbors becomes a live cell

## How To Play
Create an input file with the following format
grid_width,grid_height
Number_of_generations
Number_of_live_cells
cell1_x,cell1_y
cell2_x,cell2_y
cell3_x,cell3_y
...
cellN_x,cellN_y

## Tiles
Dead: "."
Alive: "#"
