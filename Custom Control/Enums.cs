using System.Runtime.Serialization;

namespace TextDashboard.Custom_Control
{
    public enum State
    {
        [EnumMember]
        Normal,
        [EnumMember]
        Activated,
        [EnumMember]
        Deactivated 
    }

    public enum TileSize
    {
        [EnumMember]
        Single,
        [EnumMember]
        Double,
        [EnumMember]
        Quad
    }
}
