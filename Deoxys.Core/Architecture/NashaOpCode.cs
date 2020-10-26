namespace Deoxys.Core
{
    public class NashaOpCode
    {
        public NashaCode Code { get; set; }
        public int RandomValue { get; set; }
        public int NextId { get; set; }

        public NashaOpCode() : this(NashaCode.Nop){}
        public NashaOpCode(NashaCode code) : this(code,0){}
        public NashaOpCode(NashaCode code,int randomValue)
        {
            Code = code;
            RandomValue = randomValue;
        }
    }
}