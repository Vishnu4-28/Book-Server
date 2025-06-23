using ProtoBuf;

namespace E_commerce.Server.Model.Entities
{


    [ProtoContract(SkipConstructor = true)]
    public class Protobuff
    {
        [ProtoMember(1)]
        public int User_Id { get; set; }

        [ProtoMember(2)]
        public string Email { get; set; }

    }

}
