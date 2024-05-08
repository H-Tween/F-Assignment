open System

Console.WriteLine(" Select: ")
Console.WriteLine(" 1. Task 1 and 2 ")
Console.WriteLine(" 2. Task 3 ")
Console.WriteLine(" 3. Task 4 ")
Console.WriteLine(" 4. Exit ")
Console.WriteLine("")

let rec getUserInput () =
    Console.WriteLine("Enter your choice: ")
    let input = Console.ReadLine()
    match Int32.TryParse(input) with
    | (true, choice) when choice >= 1 && choice <= 4 -> choice
    | _ ->
        Console.WriteLine("Invalid input, try again")
        Console.WriteLine("")
        getUserInput ()

let choice = getUserInput()
// ask leigh /////////////////////////////////////////////////
let runChoice (choice) =
    match choice with
        | 1 -> Account.CustomerAccount.run()
        | 2 -> Account.runAccounts()
        | 3 -> Account.ticketRun()
        | 4 -> exit 0
        | _ -> failwith "Invalid choice."
    
//Account.CustomerAccount.run()
//Account.runAccounts()
//Account.ticketRun()