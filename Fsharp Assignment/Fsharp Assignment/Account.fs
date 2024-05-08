module Account

open System
open System.Threading

//Task 1 and 2
// class
module CustomerAccount =
    type CustomerAccount
        (name:string, accountNumber:string, balance:float) =
        member this.Name = name
        member this.AccountNumber = accountNumber
        member this.Balance = balance

        // Print customer
        member this.Print() = 
            Console.WriteLine($" Name: {this.Name}")
            Console.WriteLine($" Account Number: {this.AccountNumber}")
            Console.WriteLine($" Balance: {this.Balance}")
            Console.WriteLine(" ")

        // Update balance of customer
        member this.UpdateBalance newBalance = new CustomerAccount(this.Name, this.AccountNumber, newBalance)

        // Withdraw from account
        member this.Withdraw(amount: float) =
            if amount <= 0.0 then
                failwith "Withdrawal amount must be greater than zero"
            elif amount > this.Balance then
                failwith "Insufficient funds"
            else
                this.UpdateBalance(this.Balance - amount)

        // Deposit into account
        member this.Deposit(amount: float) =
            if amount <= 0.0 then
                failwith "Deposit amount must be greater than zero"
            else
                this.UpdateBalance(this.Balance + amount)

    let CheckBalance (balance: float) =
        match balance with
        | b when b < 10.0 -> "Balance is low"
        | b when b >= 10.0 && b <= 100.0 -> "Balance is OK"
        | _ -> "Balance is high"

    let run() = 
        // assigning a customer
        let customer = new CustomerAccount("Kate", "0007", 1000)
        let customer1 = new CustomerAccount("John", "0001", 0.0)        
        let customer2 = new CustomerAccount("Alice", "0002", 51.0)        
        let customer3 = new CustomerAccount("Harry", "0003", 5.0)        
        let customer4 = new CustomerAccount("Jacob", "0004", 70.0)        
        let customer5 = new CustomerAccount("Freddie", "0005", 55.0)        
        let customer6 = new CustomerAccount("Lewis", "0006", 100.0)      
        customer.Print()

        // Withdraw from the account
        let customer = customer.Withdraw(300.0)
        printfn "After Withdraw:"
        customer.Print()

        // Deposit into account
        let customer = customer.Deposit(500.0)
        printfn "After Deposit:"
        customer.Print()

// Task 2 Check balance
        Console.WriteLine($" Customer: {customer1.AccountNumber} {CheckBalance customer1.Balance}")
        Console.WriteLine($" Customer: {customer2.AccountNumber} {CheckBalance customer2.Balance}")
        Console.WriteLine($" Customer: {customer3.AccountNumber} {CheckBalance customer3.Balance}")
        Console.WriteLine($" Customer: {customer4.AccountNumber} {CheckBalance customer4.Balance}")
        Console.WriteLine($" Customer: {customer5.AccountNumber} {CheckBalance customer5.Balance}")
        Console.WriteLine($" Customer: {customer6.AccountNumber} {CheckBalance customer6.Balance}")

// Task 3
        // ASK LEIGH /////////////////////////////////////////////////////////////////////////////
        Console.WriteLine("==================================")
        let accounts2 = [ customer1; customer2; customer3; customer4; customer5; customer6 ]
        //let LowList = Seq.filter (fun account -> account.Balance < 50.0) accounts2
        let below501, above501 = List.partition (fun acc -> acc.Balance < 50.0) accounts2

        0
        
// WORKING TASK 3 USING RECORDS
type Account = { Name: string; Balance: float }

    let runAccounts() =
        // Accounts
        let accounts = [
            { Name = "Account1"; Balance = 20.0 }
            { Name = "Account2"; Balance = 60.0 }
            { Name = "Account3"; Balance = 10.0 }
            { Name = "Account4"; Balance = 40.0 }
            { Name = "Account5"; Balance = 100.0 }
            { Name = "Account6"; Balance = 5.0 }
        ]


        // Split the list into two lists based on the balance
        let below50, above50 = List.partition (fun acc -> acc.Balance < 50.0) accounts

        // First list, accounts with balance >= 0 and < 50
        Console.WriteLine("Accounts with balance between 0 and 50:")
        for item in below50 do
            Console.WriteLine(item)

        // Second list, accounts with balance >= 50
        Console.WriteLine("Accounts with balance over 50:")
        for item in above50 do
            Console.WriteLine(item)
        0

//Task 4
type Ticket = {seat:int; customer:string}

let mutable tickets = [for n in 1..10 -> {Ticket.seat = n; Ticket.customer = ""}]
let lockObj = new Object() // object for record locking

let displayTickets () =
    for item in tickets do
        Console.WriteLine($"Customer: {item.customer}, Seat: {item.seat}")
    Console.WriteLine(" ")

let bookSeat (customerName:string) (seatNumber:int) =
    lock lockObj (fun() ->
    if seatNumber >= 1 && seatNumber <= 10 then
        let updatedTickets =
            tickets |> List.mapi (fun i ticket ->
                if i = seatNumber - 1 && ticket.customer = "" then
                    { ticket with customer = customerName }
                else
                    ticket
            )
        if updatedTickets = tickets then
            Console.WriteLine($"Seat: {seatNumber} is already booked or invalid")
        else
            Console.WriteLine($"Seat: {seatNumber} booked for {customerName}")
            tickets <- updatedTickets
    else
        Console.WriteLine($"Seat: {seatNumber} is invalid")
    )
    Thread.Sleep(1000) // simulates work being done


let ticketRun() =
    displayTickets()
    let thread1 = new Thread(fun() -> bookSeat "Harrison" 3) // assinging a thread
    thread1.Start()                                          // starting thread
    let thread2 = new Thread(fun() -> bookSeat "Alice" 5)
    thread2.Start()
    let thread3 = new Thread(fun() -> bookSeat "Jack" 11)
    thread3.Start()

    thread1.Join()
    thread2.Join()
    thread3.Join()

    displayTickets()



// task 5
// check week 10 for comparison table

// make whole program runable by the user not through code