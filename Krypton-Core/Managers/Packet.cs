using System;



namespace Krypton_Core.Utils
{
    class Packet
    {
        public const string LoginPacket = "001";
        public const string LogoutPacket = "002";
        public const string AccAddPacket = "003";




        public static string CreatePacket(string context , string packet)
        {
            if(context.ToLower() == "login")
            {
                return $"{LoginPacket}|{packet}";
            }
            else if(context.ToLower() == "logout")
            {
                return $"{LogoutPacket}|{packet}";
            }
            else if(context.ToLower() == "accadd")
            {
                return $"{AccAddPacket}|{packet}";
            }
            else
            {
                return "";
            }
        }
    }
}