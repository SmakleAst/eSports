namespace eSports.Domain.Enum
{
    public enum StatusCode
    {
        PlayerAlreadyExists = 1,
        PlayerNotFound = 2,

        TeamAlreadyExists = 11,
        TeamNotFound = 12,

        Ok = 200,
        InternalServerError = 500
    }
}
