namespace Application.Rooms.Requests
{
    public enum OccupationOp {
        Occupy,
        Disocuppy,
    }

    public class OccupationOpQuery
    {
        public int RoomId { get; set; }
        public OccupationOp Operation { get; set; }
    }
}