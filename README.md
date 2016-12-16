# PowerShell to EXE converter
This Tool generates an Executable that runs the specified PowerShell code.

Check it out: https://ps2exe.azurewebsites.net/

## Arguments
All arguments from the executable will be forwarded to the PowerShell as a string array Variable named: $args
If the arguments contains a "/debug" variable, the executable will dump the included powershell

## Changes
### V1.1.0.0 16.Dec.2016
- Compress PowerShell Code in EXE to prevent script extraction.
- Hide Console after startup as there is no output from the PowerShell 

### V1.0.0.0 14.Nov.2016
Initial Release...
