namespace LookInterview;

static class Program
{
    public static void Main(string[] _)
    {
        var help = @"
`SET [name] [value]` Sets the name in the database to the given value

`GET [name]` Prints the value for the given name. If the value is not in the database, prints NULL

`DELETE [name]` Deletes the value from the database

`COUNT [value]` Returns the number of names that have the given value assigned to them. If that value is not assigned anywhere, prints 0

`END` Exits the database

`BEGIN` Begins a new transaction

`ROLLBACK` Rolls back the most recent transaction. If there is no transaction to rollback, prints TRANSACTION NOT FOUND

`COMMIT` Commits all of the open transactions

`HELP` Lists all commands";

        Console.WriteLine("Welcome to Dian's in-memory database!");
        var db = new InMemDb();

        while (true)
        {
            var input = Console.ReadLine()?.Trim().Split(' ');
            var command = input?[0].ToLower();
    
            // 'args' is non-nullable and taken by top-level program
            var args = input?.Skip(1).ToArray();
    
            switch (command)
            {
                case null:
                    continue;
                case "set":
                    if (!ValidateArgs(args, command, 2))
                        continue;
                    db.Set(args![0], args[1]);
                    continue;
                case "get":
                    if (!ValidateArgs(args, command, 1))
                        continue;
                    Console.WriteLine(db.Get(args![0]));
                    continue;
                case "delete":
                    if (!ValidateArgs(args, command, 1))
                        continue;
                    db.Delete(args![0]);
                    continue;
                case "count":
                    if (!ValidateArgs(args, command, 1))
                        continue;
                    Console.WriteLine(db.CountValues(args![0]));
                    continue;
                case "end":
                    Console.WriteLine("Exiting program.");
                    return;
                case "begin":
                    db.BeginNewTransaction();
                    continue;
                case "rollback":
                    if (!db.CanRollback)
                    {
                        Console.WriteLine("TRANSACTION NOT FOUND");
                        continue;
                    }

                    db.Rollback();
                    continue;
                case "commit":
                    db.CommitTransactions();
                    continue;
                case "help":
                    Console.WriteLine(help);
                    continue;
                default:
                    Console.WriteLine("Command not found, use 'help' to print available commands.");
                    continue;
            }
        }


        bool ValidateArgs(string[]? args, string command, int expected)
        {
            if (args?.Length != expected)
            {
                Console.WriteLine($"Wrong number of args, {command} should have exactly two args - received {args?.Length ?? 0}.");
                return false;
            }

            if (args.Any(string.IsNullOrEmpty))
            {
                // theoretically this should never happen but we like to be safe
                Console.WriteLine("Arguments are not allowed to be null or empty");
                return false;
            }
    
            return true;
        }
    }
}