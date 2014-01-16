namespace YearEndParty.Presentation
{
    public class NameValue
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
