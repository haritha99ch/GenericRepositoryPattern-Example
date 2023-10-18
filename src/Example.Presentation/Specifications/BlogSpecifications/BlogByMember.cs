namespace Example.Presentation.Specifications.BlogSpecifications;
public class BlogByMember : Specification<Blog>
{
    public BlogByMember(Guid memberId) : base(e => e.MemberId == memberId) { }
}
