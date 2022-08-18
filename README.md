# PoleDisplayWeatherForecast

PoleDisplayWeatherForecast is a tool that uses calls weatherapi.com and outputs the returned information to a pole display over serial

## Setup
1. go to weatherapi.com and create an account. Enter your API key in the APIKEY key of the app.config file. Make sure you specify the correct COM port, baud rate, parity, data bits and stop bits. This is configurable in program.cs
2. Build the project and open the executable.
3. Enter the location you want the forecast for and press enter. By default this is refreshed every 5 minutes but this can be changed in program.cs
