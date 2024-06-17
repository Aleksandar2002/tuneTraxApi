using Application;

namespace Implementation;

public class DefaultActorProvider : IApplicationActorProvider
{
    public IApplicationActor GetActor()
    {
        return new UnauthorizedActor();
    }

}
