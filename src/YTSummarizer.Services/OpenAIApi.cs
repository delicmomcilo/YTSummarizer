using OpenAI_API;

namespace YTSummarizer.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly OpenAIAPI _openAIApi;
        public OpenAIService()
        {
            _openAIApi = new OpenAIAPI("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");
        }
        public async Task<String?> AskChatGPT(String prompt)
        {
            var result = await _openAIApi.Completions.GetCompletion(prompt);
            return result;
        }
    }
}