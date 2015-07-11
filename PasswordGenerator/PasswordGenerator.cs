namespace PasswordRandomGenerator
{
    using System;
    using System.IO;

    public class PasswordGenerator
    {
        static void Main()
        {
            int passwordsCount = 0;

            Console.Write("Enter the number of passwords you want: ");
            passwordsCount = Int32.Parse(Console.ReadLine());

            using (StreamWriter writer = new StreamWriter("Passwords.txt"))
            {
                for (int i = 0; i < passwordsCount; i++)
                {
                    string currentPassword = Generator.Generate();
                    Console.WriteLine(currentPassword);
                    writer.WriteLine(currentPassword);
                }
            }

            Console.ReadLine();
        }
    }
}
