using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Annotations;
using OpenAI_API;


namespace PersonalAI.Handlers
{
    public class OpenAIHandler
    {

        public static OpenAIAPI openai = new OpenAIAPI(MainWindow.GetOpenAiKey());


        public OpenAIHandler()
        {

            // void OpenAICompletion(string prompt)
            // {
            //     openai.Completions.CreateCompletionAsync(new CompletionCreateRequest
            //         {
            //             Model = Models.TextDavinciV3,
            //             MaxTokens = 70,
            //             Temperature = .8f,
            //             Prompt = prompt
            //         });
            // }
        }

        public static async Task CreateSynopsis()
        {
            // Set up the OpenAI API client
            string apiKey = MainWindow.GetOpenAiKey();

            // Define the conversation as a list of messages
            List<string> messages = new List<string>()
            {
                "Person A: Hi there!",
                "Person B: Hey, how's it going?",
                "Person A: Not bad, how about you?",
                "Person B: I'm doing well, thanks.",
                "Person A: That's good to hear.",
                "Person B: So, what's new?",
                "Person A: Not much, just been busy with work.",
                "Person B: Yeah, same here. It never ends, does it?",
                "Person A: Nope, but it pays the bills!",
                "Person B: Ha, that's for sure."
            };

            // Get the last 10 messages of the conversation
            List<string> last10Messages = messages.GetRange(Math.Max(messages.Count - 10, 0), Math.Min(10, messages.Count));

            // Concatenate the last 10 messages into a single string
            string conversationText = string.Join(" ", last10Messages);

            // Generate a summary of the conversation using OpenAI's GPT-3 API
            var summary = await openai.Completions.CreateCompletionAsync(
                model: "text-davinci-002",
                prompt: $"Summarize the following conversation: {conversationText}\n\nSummary:",
                temperature: 0.5,
                max_tokens: 100
            );
        }
    }
}

