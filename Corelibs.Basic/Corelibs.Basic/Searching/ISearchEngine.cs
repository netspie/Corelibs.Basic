namespace Corelibs.Basic.Searching
{
    public interface ISearchEngine<T>
    {
        void Add(SearchIndexData data);
        void Add(IEnumerable<SearchIndexData> data);
        SearchIndexData[] Search(string searchTerm);
    }

    public record SearchIndexData(string ID, string Name);
}
