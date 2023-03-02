namespace Corelibs.Basic.Searching
{
    public interface ISearchEngine<T>
    {
        void Add(SearchIndexData data);
        void Add(IEnumerable<SearchIndexData> data);
        SearchIndexData[] Search(string name, SearchType searchType = SearchType.Full);
    }

    public record SearchIndexData(string ID, string Name);
    
    public enum SearchType
    {
        Substring,
        Full,
        Start,
        End
    }
}
