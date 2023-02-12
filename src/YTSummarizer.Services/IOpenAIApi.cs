namespace YTSummarizer.Services
{
    public interface IOpenAIService
    {
        Task<String?> AskChatGPT(String prompt);
    }
}