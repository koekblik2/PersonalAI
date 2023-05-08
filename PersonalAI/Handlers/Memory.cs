using System.Collections;
using System.IO;
using System.Linq;

namespace PersonalAI.Handlers;

public class Memory
{
    private static string[] memories;
    private static string memoryPath = "memory.txt";

    public Memory()
    {
        LoadMemoryFromFile();
    }

    public static void LoadMemoryFromFile()
    {
        string memory;
        string[] prompt =
        {
            "You are a personal AI created by your creator Fabian.",
            "You help Fabian when he asks something of you.",
            "You are a smug artificial intelligence and sometimes makes jokes."
        };
        
        if (!File.Exists(memoryPath))
        {
            DebugHandler.DebugMessageSystem("File was created and written to.");
            // File.Create(memoryPath);
            File.WriteAllLines(memoryPath, prompt);
        }

        memories = File.ReadAllLines(memoryPath);

    }
}