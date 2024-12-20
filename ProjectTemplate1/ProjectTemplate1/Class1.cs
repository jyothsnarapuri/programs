
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System;
using System.Diagnostics;
using System.IO;


namespace ProjectTemplate1
{
    public class Class1
    {
        static void Main()
        {
            string folderPath = @"C:\TrumpfMetamation";
            string filePath = Path.Combine(folderPath, "Welcome.txt");
            string content = "Welcome to Trumpf Metamation!";

            try
            {
                // Step 1: Open File Explorer
                Process.Start("explorer.exe", @"C:\");
                Console.WriteLine("Opened File Explorer at C:\\\n");

                // Step 2: Create the folder
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Console.WriteLine($"Folder '{folderPath}' created successfully.");
                }

                // Step 3: Create the file and write content
                File.WriteAllText(filePath, content);
                Console.WriteLine($"File '{filePath}' created and content written successfully.");

                // Step 4: Verify file content
                string fileContent = File.ReadAllText(filePath);
                if (fileContent == content)
                {
                    Console.WriteLine("File content is correct.");
                }
                else
                {
                    Console.WriteLine("File content is incorrect.");
                }

                // Step 5: Delete the file
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine($"File '{filePath}' deleted successfully.");
                }

                // Step 6: Delete the folder
                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath);
                    Console.WriteLine($"Folder '{folderPath}' deleted successfully.");
                }

                // Step 7: Confirm deletion
                if (!File.Exists(filePath) && !Directory.Exists(folderPath))
                {
                    Console.WriteLine("File and folder have been deleted successfully.");
                }
                else
                {
                    Console.WriteLine("File or folder deletion failed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

}
}
