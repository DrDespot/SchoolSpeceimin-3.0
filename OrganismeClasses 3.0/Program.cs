using SQLite;

namespace OrganismClasses
{
    public class Program
    {

        static Animal Vos = new Animal("Vos", Organism.Origins.Native, "Forest");
        static Animal Fret = new Animal("Egel", Organism.Origins.Foreign, "Plains");
        static Animal Bionder = new Animal("Bionder", Organism.Origins.Foreign, "Shed");

        static Plant Fern = new Plant("Fern", Organism.Origins.Native, 0.90);
        static Plant Eendenkroes = new Plant("Eendenkroes", Organism.Origins.Native, 0.005);
        static Plant Ambrosia = new Plant("Ambrosia", Organism.Origins.Foreign, 4.00);

        static List<Organism> AllOrganisms = new List<Organism>
        {
            Vos,
            Fret,
            Bionder,
            Fern,
            Eendenkroes,
            Ambrosia
        };


        static void Main(string[] args)
        {
            Console.WriteLine("======================\n" +
                "WELCOME TO ORGANISM EDITOR\n" +
                "MADE BY THE ANIMATES\n" +
                "======================\n");

            MainOptionsMenu();

            Plant plant = new Plant("Brandnetel", Organism.Origins.Native, 0.25);
            DAL dal = new DAL();
            dal.addPlant(plant);

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
                    ShowOrganismsList(AllOrganisms);
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
            int Origin = 0;

            bool validName = false;
            while (!validName)
            {
                //Why wont you automatically create a new line??
                Name = AskOpenQuestion("Input animal name: \n");

                if (AllOrganisms.Any(o => o.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("That name already exists. Please choose a different name.\n");
                }
                else
                {
                    validName = true;
                }
            }

            Origin = AskClosedQuestion($"Where does the {Name} come from?\n1. Native\n2. Foreign");

        }

        static void AddPlant()
        {
            string Name = "";
            int Origin = 0;

            bool validName = false;
            while (!validName)
            {
                //Why wont you automatically create a new line??
                Name = AskOpenQuestion("Input plant name: \n");

                if (AllOrganisms.Any(o => o.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("That name already exists. Please choose a different name.\n");
                }
                else
                {
                    validName = true;
                }
            }

            Origin = AskClosedQuestion($"Where does the {Name} come from?\n1. Native\n2. Foreign");
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
                    var nativeOrganisms = AllOrganisms.Where(o => o.Origin == Organism.Origins.Native).ToList();

                    foreach (var organism in nativeOrganisms)
                    {
                        Console.WriteLine(organism.DryDescription());
                    }
                    break;

                case 4:
                    Console.WriteLine("FOREIGN ONLY FILTER");
                    var foreignOrganisms = AllOrganisms.Where(o => o.Origin == Organism.Origins.Foreign).ToList();

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