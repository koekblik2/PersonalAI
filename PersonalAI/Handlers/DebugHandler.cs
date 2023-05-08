
using System;

namespace PersonalAI.Handlers
{
    public static class DebugHandler
    {
        
        public static string DebugMessageAi(string msg)
        {
            DateTime dateTime = DateTime.Now;
            return $"{dateTime} PersonalAI: {msg}\n";
        }

        public static string DebugMessageSystem(string msg)
        {
            DateTime dateTime = DateTime.Now;
            return $"{dateTime} System: {msg}\n";
        }
        
    }
}

