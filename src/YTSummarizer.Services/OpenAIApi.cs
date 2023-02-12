using OpenAI_API;

namespace YTSummarizer.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly OpenAIAPI _openAIApi;
        public OpenAIService()
        {
            _openAIApi = new OpenAIAPI("");
        }
        public async Task<String?> AskChatGPT(String prompt)
        {
            var result = await _openAIApi.Completions.GetCompletion(prompt);
            return result;
        }

        public async Task<String?> AskChatGPTasStream(String prompt)
        {
            var result = await _openAIApi.Completions.GetCompletion(prompt);
            return result;
        }
    }
}