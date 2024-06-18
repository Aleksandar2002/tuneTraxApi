using Domain.Entities;

namespace Implementation;

public static class UseCaseInfo
{
    public static int MaxUseCaseId { get; } = 38;
    public static List<int> UseCasesThatNeedFileUpload { get; } = new List<int> { 6, 8, 31, 15 };

    public static List<UserUseCase> RegisteredUserAllowedUseCases = new List<UserUseCase>{
                    new UserUseCase{
                        UseCaseId= 4
                    },
                    new UserUseCase{
                        UseCaseId= 5
                    },
                    new UserUseCase
                    {
                        UseCaseId = 9
                    },
                    new UserUseCase
                    {
                        UseCaseId = 13
                    },
                    new UserUseCase
                    {
                        UseCaseId = 15
                    },
                    new UserUseCase
                    {
                        UseCaseId = 17
                    },
                    new UserUseCase
                    {
                        UseCaseId = 18
                    },
                    new UserUseCase
                    {
                        UseCaseId = 19
                    },
                    new UserUseCase
                    {
                        UseCaseId = 20
                    },
                    new UserUseCase
                    {
                        UseCaseId = 21
                    },
                    new UserUseCase
                    {
                        UseCaseId = 22
                    },
                    new UserUseCase
                    {
                        UseCaseId = 23
                    },
                    new UserUseCase
                    {
                        UseCaseId = 24
                    },
                    new UserUseCase
                    {
                        UseCaseId = 25
                    },
                    new UserUseCase
                    {
                        UseCaseId = 26
                    },
                    new UserUseCase
                    {
                        UseCaseId = 27
                    },
                    new UserUseCase
                    {
                        UseCaseId = 34
                    },
                    new UserUseCase
                    {
                        UseCaseId = 33
                    },
                    new UserUseCase
                    {
                        UseCaseId = 36
                    },
                };

}
