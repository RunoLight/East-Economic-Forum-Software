namespace Tourism.Localization
{
    public interface ILocalized
    {
        public string Term { get; }
        public void SetTerm(string newTerm);
    }
}