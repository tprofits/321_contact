namespace _321contact {
    internal class Program {
        struct Contact {
            public string firstName;
            public string lastName;
            public string address;
            public string city;
            public string state;
            public string zipcode;
            public string title;
        }//END STRUCT
        static void Main(string[] args) {
            int userSelection = 0;

            while (userSelection != 2) {
                userSelection = Menu();

                switch (userSelection) {
                    case 0: { SearchContact(); break; }
                    case 1: { InputContact() ; break; }
                }//END SWITCH

                if (userSelection != 2) {
                    Input("Press enter to go back to main menu.");
                }//END IF
            }//END WHILE

            Console.WriteLine();
            Console.WriteLine("Program shutting down.");

        }//END MAIN

        static int Menu() {
            int userSelectionOut = 0;
            string userSelection = "";
            bool parsingSuccessful = false;
            

            do {
                Console.Clear();
                Console.WriteLine("Please make a selection from the menu.");
                Console.WriteLine("0. Search contacts");
                Console.WriteLine("1. Enter new contact");
                Console.WriteLine("2. Quit");

                do {
                    userSelection = Input("Selection: ");
                    parsingSuccessful = int.TryParse(userSelection, out userSelectionOut);
                }while (parsingSuccessful == false);

            } while (userSelectionOut < 0 || userSelectionOut > 2);

            return userSelectionOut;
        }//END FUNCTION

        static string Input(string prompt) {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }//END FUNCTION

        static void SearchContact() {
            Console.Clear();
            string path = @"C:\Users\Trevor\Downloads\contacts (1).dat"; 
            string data = "";
            string currentRecord = "";
            string searchName = "";
            StreamReader readFile = new StreamReader(path);

            while (readFile.EndOfStream == false) {
                data = readFile.ReadLine();
            }

            readFile.Close();

            string[] records = data.Split((char)30);

            Contact[] people = new Contact[records.Length-1];

            for (int i = 0; i < people.Length; i++) {

                currentRecord = records[i];
                string[] fields = currentRecord.Split((char)31);

                people[i].firstName = fields[0];
                people[i].lastName  = fields[1];
                people[i].address   = fields[2];
                people[i].city      = fields[3];
                people[i].state     = fields[4];
                people[i].zipcode   = fields[5];
                people[i].title     = fields[6];

            }//END FOR
         
            string newSearch = "";
           
            do {

                searchName = Input("Search name: ");
                Console.Clear();

                bool doesContain = false;

                for (int i = 0; i < people.Length; i++) {
                    if (people[i].firstName.Contains(searchName, StringComparison.OrdinalIgnoreCase)) {
                        doesContain = true;
                        Console.WriteLine($"Name   : {people[i].title}. {people[i].firstName} {people[i].lastName}");
                        Console.WriteLine($"Address: {people[i].address}");
                        Console.WriteLine($"         {people[i].city}, {people[i].state}, {people[i].zipcode}");
                        Console.WriteLine();
                    }//END IF
                }//END FOR

                if (doesContain == false) {
                    Console.WriteLine("Sorry that name does not exist in contacts.\n");
                }//END IF
                
                    newSearch = Input("Would you like to search again(y/n): ").ToLower();
                    
                    Console.Clear();

            }while (newSearch == "y");

        }//END FUNCTION

        static void InputContact() {
            Console.Clear();
            string newEntry = "";
            string data ;
            string path = @"C:\Users\Trevor\Downloads\contacts (1).dat";

            do {
                Console.Clear();
                Contact dataEntry = new Contact();

                dataEntry.title = Input("Title         : ");
                dataEntry.firstName = Input("First name: ");
                dataEntry.lastName = Input("Last  name : ");
                dataEntry.address = Input("Address     : ");
                dataEntry.city = Input("City           : ");
                dataEntry.state = Input("State         : ");
                dataEntry.zipcode = Input("ZipCode     : ");

                data = ($"{dataEntry.firstName}{(char)31}{dataEntry.lastName}{(char)31}{dataEntry.address}{(char)31}{dataEntry.city}{(char)31}{dataEntry.state}{(char)31}{dataEntry.zipcode}{(char)31}{dataEntry.title}{(char)30}");

                WriteToFile(path, data);

                Console.WriteLine();
               
                    newEntry = Input("Would you like to enter another contact, y/n: ").ToLower();
               

            }while (newEntry == "y");

        }//END FUNCTION

        static void WriteToFile(string path, string data) {
            StreamWriter outfile = File.AppendText(path);

            outfile.Write(data);

            outfile.Close();

        }//END FUNCTION   

    }//END CLASS
}//NAMESPACE