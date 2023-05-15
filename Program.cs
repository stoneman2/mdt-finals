using System;
namespace StonemanFinals
{
    public struct Town
    {
        public string name;
        public int contacts;
        public int[] contactList;
        public int infection;
        public bool shouldspread;
        public Town(string name, int contacts, int[] contactList, int infection, bool shouldspread)
        {
            this.name = name;
            this.contacts = contacts;
            this.contactList = contactList;
            this.infection = infection;
            this.shouldspread = shouldspread;
        }
    }
    class ExamRagnarok
    {
        static void ActionSpread(ref Town[] towns)
        {
            Town[] oldTowns = towns;
            // Add 1 to infection if a town is in contact with a town with more infection than itself.
            for (int townID = 0; townID < towns.GetLength(0); townID++)
            {
                towns[townID].shouldspread = false;
                foreach (int contactID in towns[townID].contactList)
                {
                    if (towns[contactID].infection > towns[townID].infection)
                    {
                        towns[townID].shouldspread = true;
                    }
                }
            }

            for (int townID = 0; townID < towns.GetLength(0); townID++)
            {
                if (towns[townID].shouldspread)
                {
                    towns[townID].infection++;
                    if (towns[townID].infection > 3)
                    {
                        towns[townID].infection = 3;
                    }
                }
            }
        }
        static void ActionVaccinate(ref Town[] towns, int townID)
        {
            towns[townID].infection = 0;
        }
        static void ActionLockDown(ref Town[] towns, int townID)
        {
            towns[townID].infection--;
            for (int i = 0; i < towns[townID].contacts; i++)
            {
                if (towns[towns[townID].contactList[i]].infection > 0)
                {
                    towns[towns[townID].contactList[i]].infection--;
                    if (towns[towns[townID].contactList[i]].infection < 0)
                    {
                        towns[towns[townID].contactList[i]].infection = 0;
                    }
                }
            }
        }
        static void ActionOutbreak(ref Town[] towns, int townID)
        {
            if (towns[townID].infection <= 3)
            {
                towns[townID].infection += 2;

                if (towns[townID].infection > 3)
                {
                    towns[townID].infection = 3;
                }
            }

            for (int i = 0; i < towns[townID].contacts; i++)
            {
                if (towns[towns[townID].contactList[i]].infection <= 3)
                {
                    towns[towns[townID].contactList[i]].infection++;
                }
            }
        }
        static void PrintTownStatus(Town[] towns)
        {
            for (int i = 0; i < towns.GetLength(0); i++)
            {
                Town currentTown = towns[i];
                Console.WriteLine("{0} {1} {2}", i, currentTown.name, currentTown.infection);
            }
        }
        static int AskTownID(Town[] towns)
        {
            Console.Write("Insert town ID: ");
            int townID = int.Parse(Console.ReadLine());
            return townID;
        }
        static void AskAction(Town[] towns)
        {
            PrintTownStatus(towns);

            Console.Write("Start an action: ");
            string action = Console.ReadLine();
            switch (action)
            {
                case "Outbreak":
                    ActionOutbreak(ref towns, AskTownID(towns));
                    AskAction(towns);
                    break;
                case "Vaccinate":
                    ActionVaccinate(ref towns, AskTownID(towns));
                    AskAction(towns);
                    break;
                case "Lock down":
                    ActionLockDown(ref towns, AskTownID(towns));
                    AskAction(towns);
                    break;
                case "Spread":
                    ActionSpread(ref towns);
                    AskAction(towns);
                    break;
                case "Exit":
                    return;
                default:
                    Console.WriteLine("Invalid action.");
                    AskAction(towns);
                    break;
            }
        }
        static void BeginSimulation(Town[] towns)
        {
            AskAction(towns);
        }
        static bool IsValidContact(int ID, int count, int currentID, int[] contacts)
        {
            for (int i = 0; i < contacts.GetLength(0); i++)
            {
                if (ID == contacts[i])
                {
                    Console.WriteLine("Invalid ID.");
                    return false;
                }
            }
            if (ID < 0)
            {
                Console.WriteLine("Invalid ID.");
                return false;
            }
            else if (ID >= count)
            {
                Console.WriteLine("Invalid ID.");
                return false;
            }
            else if (ID == currentID)
            {
                Console.WriteLine("Invalid ID.");
                return false;
            }
            else
            {
                return true;
            }
        }
        static int AskContact(int count, int townID, int[] contacts )
        {
            Console.Write("Insert contact ID: ");
            int contactID = int.Parse(Console.ReadLine());

            if (IsValidContact(contactID, count, townID, contacts))
            {
                return contactID;
            }
            else
            {
                return AskContact(count, townID, contacts);
            }
        }
        static int[] GetContacts(int townCount, int contactCount, int townID)
        {
            int[] contacts = new int[contactCount];

            for (int i = 0; i < contacts.GetLength(0); i++)
            {
                contacts[i] = 999;
            }

            for (int i = 0; i < contactCount; i++)
            {
                int contactID = AskContact(townCount, townID, contacts);
                contacts[i] = contactID;
            }
            return contacts;
        }
        static void Ragnarok()
        {
            Console.Write("Insert number of towns: ");
            int townCount = int.Parse(Console.ReadLine());

            Town[] towns = new Town[townCount];
            for (int i = 0; i < townCount; i++)
            {
                Console.Write("Insert town name: ");
                string townName = Console.ReadLine();
                Console.Write("Insert number of contacts: ");
                int townContacts = int.Parse(Console.ReadLine());
                int[] townContactList = GetContacts(townCount, townContacts, i);

                towns[i] = new Town(townName, townContacts, townContactList, 0, false);
            }

            BeginSimulation(towns);
        }
        static void Main(string[] args)
        {
            Ragnarok();
        }
    }
}
