# PowerShell to EXE converter
This Tool generates an Executable that runs the specified PowerShell code.

Check it out: https://ps2exe.azurewebsites.net/

## Arguments
All arguments from the executable will be forwarded to the PowerShell as a string array Variable named: $args
If the arguments contains a "/debug" variable, the executable will dump the included powershell
