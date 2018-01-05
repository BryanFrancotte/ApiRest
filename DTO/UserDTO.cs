namespace ApiRest.DTO
{
    public class UserDTO
    {
        public string Id{ get; set; }
        public string UserName{ get; set; }
        public string AndroidToken{ get; set; }
        public byte[] VerCol { get; set; }
    }
}