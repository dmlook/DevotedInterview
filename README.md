# Devoted Tech Assessment
Candidate: Dian Look

## About
This in-memory database project is a command line program that reads values from STDIN line by line and executes the functions as they happen. 

Note:`name` and `value` are assumed to be strings with no spaces in them. 

## Functions

`SET [name] [value]` Sets the name in the database to the given value

`GET [name]` Prints the value for the given name. If the value is not in the database, prints NULL

`DELETE [name]` Deletes the value from the database

`COUNT [value]` Returns the number of names that have the given value assigned to them. If that value is not
assigned anywhere, prints 0

`END` Exits the database

`BEGIN` Begins a new transaction

`ROLLBACK` Rolls back the most recent transaction. If there is no transaction to rollback, prints TRANSACTION
`NOT FOUND`

`COMMIT` Commits all of the open transactions

`HELP` Lists all commands

## Running the Program

1. Install requirements: [.NET SDK 6.0.412](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
1. Clone repo: 
1. Navigate to project folder: `cd LookInterview/LookInterview`
1. Launch program: `dotnet run`