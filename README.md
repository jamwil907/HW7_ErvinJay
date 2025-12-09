Download the project

Go to: https://github.com/jamwil907/HW7_ErvinJay

Click Code → Download ZIP.

Extract the ZIP to a folder, for example C:\HW7_ErvinJay.
# Turing Machine Simulator



A C# implementation of a Turing Machine that validates if a binary string has the same number of zeros on both sides of a delimiter (`#`).



## What It Does



This Turing Machine simulator accepts strings in the format `left#right` where both sides contain binary digits (0s and 1s). It validates whether the number of zeros on the left side matches the number of zeros on the right side.



### Example Inputs



- `0#0` ->**ACCEPT** (one 0 on each side)

- `010#010` ->**ACCEPT** (two 0s on each side)

- `0100#0010` ->**ACCEPT** (two 0s on each side)

- `01#10` ->**REJECT** (one 0 on left, one 0 on right, but positions differ)

- `#10` ->**REJECT** (zero 0s on left, one 0 on right)



## Prerequisites (macOS)



You need to install the .NET 8.0 SDK on your Mac.



### Installing .NET 8.0



1. **Using Homebrew** (recommended):

   ```bash

   brew install --cask dotnet-sdk

   ```



2. **Manual Installation**:

   - Download the .NET 8.0 SDK for macOS from [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

   - Choose the appropriate installer for your Mac (Intel or Apple Silicon)

   - Run the installer package



3. **Verify Installation**:

   ```bash

   dotnet --version

   ```

   You should see version 8.0.x or higher.



## Running the Project



1. **Clone or download this project** to your Mac



2. **Navigate to the project directory**:

   ```bash

   cd path/to/TuringMachine

   ```



3. **Run the program**:

   ```bash

   dotnet run

   ```



The program will execute several test cases and display the step-by-step execution of the Turing Machine for each input.



## Building the Project



To build without running:

```bash

dotnet build

```



To create a release build:

```bash

dotnet build -c Release

```



## How It Works



The Turing Machine uses the following algorithm:



1. **Mark Phase**: Find the leftmost unmarked `0` on the left side and mark it with `X`

2. **Seek Phase**: Move right across the `#` delimiter

3. **Match Phase**: Find the rightmost unmarked `0` on the right side and mark it with `Y`

4. **Return Phase**: Move back to the left side

5. **Repeat**: Continue until all zeros on the left are matched

6. **Validation Phase**: Verify all zeros on the right side are also matched



The machine uses these states:

- `q0` - Initial state, finding unmatched zeros on the left

- `q1` - Moving right to the delimiter

- `q2` - Finding a matching zero on the right

- `q3` - Returning to the left side

- `q4` - Final validation of the right side

- `qaccept` - Accept state (strings match)

- `qreject` - Reject state (strings don't match)



## Project Structure



- `Program.cs` - Main program and Turing Machine implementation

- `TuringMachine.csproj` - .NET project configuration

- `TuringMachine.slnx` - Solution file



```



Then run the program again with `dotnet run`.



## Troubleshooting



### "dotnet: command not found"

Make sure .NET SDK is properly installed and added to your PATH. You may need to restart your terminal or run:

```bash

export PATH="$PATH:$HOME/.dotnet"

```



### Build errors

Ensure you're using .NET 8.0 or higher:

```bash

dotnet --version
