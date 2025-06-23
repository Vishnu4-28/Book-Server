using ProtoBuf;

namespace E_commerce.Server.Model.DTO
{
    [ProtoContract]
    public class protobuffUser
    {
        [ProtoMember(1)]
        public int User_Id { get; set; }

        [ProtoMember(2)]
        public string Email { get; set; }
    }
}
