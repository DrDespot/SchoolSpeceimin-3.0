using System.Xml.Linq;
using Bogus.DataSets;
using SQLite;
using static OrganismClasses.Organism;


namespace OrganismClasses
{
    public class Program
    {


        static List<Organism> AllOrganisms = new List<Organism>();




        static void Main(string[] args)
        {
            Console.WriteLine("======================\n" +
                "WELCOME TO ORGANISM EDITOR\n" +
                "MADE BY THE ANIMATES\n" +
                "======================\n");

            MainOptionsMenu();

            

        }

        static void MainOptionsMenu()
        {
            int result = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine("What do you want to do?\n" +
                    "1. Add new organism\n" +
                    "2. View all organisms\n" +
                    "3. Filter organisms\n" +
                    "4. Exit application\n");

                string input = Console.ReadLine();

                if (int.TryParse(input, out result) && result <= 4)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }


            switch (result)
            {
                case 1:
                    AddOrganism();
                    break;

                case 2:
                    //ShowOrganismsList(AllOrganisms);
                    DAL dal = new DAL();
                    List<Organism> organisms = dal.getAllOrganisms();
                    foreach(Organism org in organisms)
                    {
                        Console.WriteLine(org.DryDescription());
                    }
                    MainOptionsMenu();
                    Console.WriteLine("\n");
                    break;

                case 3:
                    FilterOrganisms();
                    break;

                case 4:
                    ExitPrgm();
                    break;

                case 500:
                    Bjorn();
                    break;

                default:
                    Console.WriteLine("Uhm How did you get here?!");
                    break;
            }
        }

        static void AddOrganism()
        {

            int answer = 0;
            Console.WriteLine("\nADDING ORGANISM:");
            answer = AskClosedQuestion("Organism type?\n1. Animal \n2. Plant");

            if (answer == 1)
            {
                Console.WriteLine();
                AddAnimal();
            }
            else
            {
                AddPlant();
            }


        }

        static void AddAnimal()
        {

            string Name = "";
            string Origin = "";
            string Habitat = "";

            Name = AskOpenQuestion("Input plant name: \n");

            int answer = AskClosedQuestion($"Where does the {Name} come from?\n1. Native\n2. Foreign");

            if (answer == 1)
            {
                Origin = "Native";
            }
            else
            {
                Origin = "Foreign";
            }

            Habitat = AskOpenQuestion("Input animal habitat [ex. pond, forest, plains]: \n");



            DAL dal = new DAL();

            List<Organism> allOrganisms = dal.getAllOrganisms();
            // Assuming you have a method to fetch all animals from the database
            int newId = allOrganisms.Count;


            Animal newAnimal = new Animal(newId, Name, Origin, Habitat);



            dal.addAnimal(newAnimal);

        }

        static void AddPlant()
        {
            string Name = "";
            string Origin = "";
            double HeightInMeters = 0;

            Name = AskOpenQuestion("Input plant name: \n");



            int answer = AskClosedQuestion($"Where does the {Name} come from?\n1. Native\n2. Foreign");

            if (answer == 1)
            {
                Origin = "Native";
            }
            else
            {
                Origin = "Foreign";
            }

            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine("Enter a height in meters:");

                string input = Console.ReadLine();

                if (double.TryParse(input, out HeightInMeters))
                {
                    if (HeightInMeters <= 0)
                    {
                        Console.WriteLine("Error: Height must be a positive number.");
                    }
                    else
                    {
                        validInput = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            //Console.WriteLine($"The entered height is: {HeightInMeters} meters");

            Plant newPlant = new Plant(Name, Origin, HeightInMeters);

            DAL dal = new DAL();
            dal.addPlant(newPlant);

        }


        static int AskClosedQuestion(string Question)
        {
            int result = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine(Question);
                string input = Console.ReadLine();

                if (int.TryParse(input, out result))
                {
                    if (result == 1 || result == 2)
                    {
                        validInput = true;
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            Console.WriteLine();
            return result;
        }

        static string AskOpenQuestion(string Question)
        {
            string input = "";
            do
            {
                Console.Write(Question);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid input");
                }

            } while (string.IsNullOrWhiteSpace(input));

            Console.WriteLine();
            return input;
        }


        static void ShowOrganismsList(List<Organism> AllOrganisms)
        {
            Console.WriteLine("\n");
            Console.WriteLine("ALL ORGANISMS:");


            foreach (Organism Organism in AllOrganisms)
            {
                //1st Animal specificies hte type [class animal]
                //2nd animal makes a var animal to refer to 
                //Basically making "Organism" more specified.
                if (Organism is Animal animal)
                {
                    Console.WriteLine(animal.DryDescription());
                }

                if (Organism is Plant plant)
                {
                    Console.WriteLine(plant.DryDescription());
                }

            }
            Console.WriteLine("\n");

            MainOptionsMenu();
        }


        static void FilterOrganisms()
        {

            int result = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine("\nFILTERING ORGANISMS\n" +
                "What filter would you like to apply?\n" +
                "1. Animals\n" +
                "2. Plants\n" +
                "3. Native\n" +
                "4. Foreign");

                string input = Console.ReadLine();

                if (int.TryParse(input, out result) && result <= 4)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }


            switch (result)
            {
                case 1:
                    Console.WriteLine("ANIMALS ONLY FILTER");
                    //This var is an IEnumerable<Animal>
                    var animals = AllOrganisms.OfType<Animal>();

                    foreach (var animal in animals)
                    {
                        Console.WriteLine(animal.DryDescription());
                    }
                    break;

                case 2:
                    Console.WriteLine("PLANTS ONLY FILTER");
                    var plants = AllOrganisms.OfType<Plant>();

                    foreach (var plant in plants)
                    {
                        Console.WriteLine(plant.DryDescription());
                    }
                    break;

                case 3:
                    Console.WriteLine("NATIVE ONLY FILTER");
                    var nativeOrganisms = AllOrganisms.Where(o => o.Origin == "Native").ToList();

                    foreach (var organism in nativeOrganisms)
                    {
                        Console.WriteLine(organism.DryDescription());
                    }
                    break;

                case 4:
                    Console.WriteLine("FOREIGN ONLY FILTER");
                    var foreignOrganisms = AllOrganisms.Where(o => o.Origin == "Foreign").ToList();

                    foreach (var organism in foreignOrganisms)
                    {
                        Console.WriteLine(organism.DryDescription());
                    }
                    break;


                default:
                    Console.WriteLine("Uhm How did you get here?!");
                    break;
            }

            MainOptionsMenu();

        }

        static void ExitPrgm()
        {
            //Hehe
            Console.WriteLine("\nAre you sure you want to exit this program? Charlie will miss you\n" +
                "1. Yes\n" +
                "2. [or other] No\n");

            int result = 0;
            string input = Console.ReadLine();
            int.TryParse(input, out result);

            if (int.TryParse(input, out result) && result == 1)
            {
                Environment.Exit(0);
            }
            else
            {
                MainOptionsMenu();
            }

        }

        static void Bjorn()
        {
            string asciiBjorn = @"################################################################################
###############################(*.             ,/(##############################
########################*      ..,,,,,,,,,,,,,,,..     .(#######################
###################(    .,****/(*,,*,,,,,,,,,,,,,,,,,,,.    ,###################
################.   *###%%&&&&&&%%%%%%%%#(/*,,,,,,,,,,,,,,,,.   (###############
#############,   /%%&&&&&&&&&&&&&&&&&&&&&&&&%#*,,,,,,,,,,,,,,,,,   #############
###########   *(%%&&&@&&@@&&&&&&&&%&&&&&&&&&&&&&#/,,,,,,,,,,,,,,,,.  *##########
#########   (%%%&&@&&@@@@@&&%##((/(((((##%%%&&&&&&%*,*,,,,,,,,,,,,,,,  *########
#######,  *#/%&&&&&@@@&&%#((((////////((((##%%&&&&&%*,,,,,,,,,,,,,,,,,.  (######
######  ./#%&@@@@&&@&@&%/////*****////(((((###%%&@&&%,,,,,,,,,,,,,,,,,,,  ,#####
####(  .*#%&&@&@&&&%&&&/*/*,,,*****//(/((((###%%%&@&%#,,,/,,,,,,,,,,,,,,,  .%###
####  ,,*#%&&@@@&&&%(//**,,,,,*****///((((((###%%%&@&%/(#*,,,,,,,,,,,,,,,,. .%##
###  .,,,%%&&&@@&&%(*,,,,,,,,,,***////(((/(((###%%%%((*,,,,,,,,,,,,,,,,,,,,  *%#
#%*  ,//(#%&&&@&&&#**,,,,,,,,,,,**////((/(((((##%%%%(,,,,,,,,,,,,,,,,,,,,,,,  #%
##  .,,,,,(%&&&&&&#*,,,,,,,,****//((((((((#%%&&@@&%&%,**/%%,,,,,,,,,,,,,,,,,  *%
##  ,.,,,,*(/%&&&%/*,**/(%%######%%%%%&%%%&@@&&&%%%%#*,,,//,,,,,,,,,,,,,,,,,. .%
%#  ...,.,.,,(%#((((#&&(((###%&&%%%%#/*#@&&@@&@@&%&%%#,,,(*,,,,,,,,,,,,,,,,,   %
%#  ,...,,,*(/**/*,,,*#**(((/%&%##%#/**%#*/(&@&&&%%%%%(,/#,,,,,,,,,,,,,,,,,,. .%
%#  ......,*(*///*,,,,*(,,,**/(####/**#%*,**#&@@@&&&&&%,,,,,,,,,,,,,,,,,,,,,  *%
 .   .....,, **/ (/**,,,,*#***// (((((%%#(*****/(%%&&%%%%%/,,,,,,,,,,,,,,,,,,,.  ..
 .......,,, **/**,,,,,,******/(((((/******/(#%%%%%%%%(,,,,,,,,,,,,,,,,***   ..
 ...  ......,,,,//**,,,,,,,****//((#(/(#((/(#&&&&&%%%%%%*****************/   .. 
  ...  .....,,,, ******,,,,, ****/ (#(///////(#(#%&&&&&&%%%/*******/////////   ... 
   ...   .,.,,,,, *************///////*****/((#%&&&&&%%&%////////////////   ...  
    ...   ,, **,,, *************////((//((((((##%%&&&&&&@@@&%%@@@@@@#%&&,  ...    
     ...., ***,/////********//(#(*****/(#%%%%&&&&&@@@@@@@@@@@@@@@@/   ...     
       ...., (%%/**////***////((((/(((##%%%%%%&&&&@@@@@@@@@@@@@@,   ....      
         .... % (*****//(((((((/////(((((##%%%&&&@@@@@@@@@@@@(   .....        
           .....    , *****//((##(((/////(##%%%&&&&@@@@@@@@@@,   ......          
              ......    .* *///////////(###%%&&@@@@@@@@@#     ......             
                 ........      ., *///(((##%&&@@%/,      .,......";

            Console.WriteLine(asciiBjorn);
            MainOptionsMenu();
        }


    }
}