# FileOrganizer

This project was generated with VISUAL STUDIO 2019 version 16.9 and ASP CORE 3.1.3
## After cloning this project

Run `dotnet restore in the project file where program is located you can also run dotnet run this would restore necessary dependency needed,build and run the project 


## Development server

Run `dotnet  run ` for a dev server using cmd while in visual studio and click the play button either in debug or undebug mode or contrl + f2 to run . 
![Organiser - Microsoft Visual Studio 06_09_2020 1_26_02 am](https://user-images.githubusercontent.com/54416255/92315634-5f5f1100-efe0-11ea-928d-03e1aa672e43.png)


## Build

Run `dotnect build` to build the project. The build artifacts will be stored in the `bin/` directory. or cntrl + shift  + b

## Install File Organiser
Publish the project into a folder using visual studio publish or command terminal `dotnet publish -c Release -o C:\MyWebs\test` once it has been published to desired folder 
Open your command prompt or power shell has administrator then `Run SC CREATE  binpath= C:\MyWebs\test ` make sure there is a space immediately after equals to in other for the service to install properly you can add a  display name by adding `displayname= MyService` ensure there is space before writing the display name, to enabe auto start add  `start=  true` by default its false, if both are added the command, the command looks like this `SC CREATE  binpath= C:\MyWebs\test  displayname= MyService  start=  true`  once installed 
you get a success message
`[SC] CreateService SUCCESS`
## Further help

To get more help on the microsoft docs https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/sc-create
