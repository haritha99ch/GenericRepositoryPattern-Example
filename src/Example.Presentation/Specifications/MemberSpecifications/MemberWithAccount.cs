namespace Example.Presentation.Specifications.MemberSpecifications;
public class MemberWithAccount : Specification<Member>
{
    public MemberWithAccount()
    {
        AddInclude(q => q.Include(m => m.Account));
    }
}
