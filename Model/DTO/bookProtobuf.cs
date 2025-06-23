using ProtoBuf;

namespace E_commerce.Server.Model.DTO
{

    [ProtoContract]
    public class bookProtobuf
    {

        [ProtoMember(1)]
        public int Book_Id { get; set; }

        [ProtoMember(2)]
        public required string Title { get; set; }

        [ProtoMember(3)]
        public required string Author { get; set; }

        [ProtoMember(4)]
        public int ISBN { get; set; }

        [ProtoMember(5)]
        public int Quantity { get; set; }

        [ProtoMember(6)]
        public bool IsDeleted { get; set; } = false;

        [ProtoMember(7)]
        public string? FilePath { get; set; }
    }
}
