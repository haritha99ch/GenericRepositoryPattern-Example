namespace Example.Presentation.Specifications.BlogSpecifications;
public class BlogWithMember : Specification<Blog>
{
    public BlogWithMember()
    {
        AddIncludes();
    }

    public BlogWithMember(string caption) : base(b => b.Caption == caption)
    {
        AddIncludes();
    }

    public BlogWithMember(bool ageRestricted) : base(b => b.AgeRestricted == ageRestricted)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        AddInclude(q => q.Include(b => b.Member).ThenInclude(m => m!.Account));
    }
}
