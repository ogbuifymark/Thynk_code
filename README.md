# Thynk_code

Attachecd link is the Flow diagram of the app 
https://app.diagrams.net/?libs=general;flowchart#Hogbuifymark%2FThynk_code%2Fmaster%2FUntitled%20Diagram.drawio

# Recommendation 
- Implement Authentication and authorization 
- Add Email notification/verification module. 
- Add proper reporting dashboard for analytics 
- Add logs for Error
- Implement a middleware for error handling
- 

# Assumptions 
- I assummed that there will be only three type of users 
- i assumed that the user can have multiple bookings


# How to run the app

Open the app with visual studiio by clicking the .sln file.
open your appsetting.json and include the connection string to the database
e.g 
"ConnectionStrings": {
    "DefaultConnection": "Server=serverUrl;Database=Thynk_Code; User Id=Username;password=password;  ConnectRetryCount=0;Trusted_Connection=False;MultipleActiveResultSets=true"
  }
click on Debug(browser) to run.

this will run the server side and the react app on port 5001.



