using Domain.Rooms.ValueObjects;

namespace Application.Errors
{
    public class OccupationOpException : Exception
    {
        public OccupyResult.Failed Fail { get; init; }

        public OccupationOpException(string message, OccupyResult.Failed fail) : base(message)
        {
            Fail = fail;
        }
    }
}