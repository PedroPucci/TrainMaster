namespace TrainMaster.Domain.ValueObject
{
    public class Name
    {
        public string Value { get; set; }


        public Name(){}

        public Name(string value)
        {
            Value = value;
        }
    }
}