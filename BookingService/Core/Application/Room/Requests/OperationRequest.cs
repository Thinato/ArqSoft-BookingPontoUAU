namespace Application.Rooms.Requests
{
    public enum OccupationOp {
        Occupy,
        Disocuppy,
    }

    public readonly record struct OccupationOpQuery(
        OccupationOp Operation
    );
}