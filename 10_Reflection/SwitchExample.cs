namespace IteaReflection
{
    public enum SomeEnum
    {
        Some1,
        Some2,
        Some3,
        Some4
    }
    public class SwitchExample
    {
        public string GetBySomeEnum(SomeEnum some)
        {
            switch (some)
            {
                case SomeEnum.Some1: return "We find 1";
                case SomeEnum.Some2: return "We find 2";
                case SomeEnum.Some3: return "We find 3";
                case SomeEnum.Some4: return "We find 4";
                default: return "";
            }
        }

        public string GetBySomeEnumNew(SomeEnum some) => some switch
        {
            SomeEnum.Some1 => "We find 1",
            SomeEnum.Some2 => "We find 2",
            SomeEnum.Some3 => "We find 3",
            SomeEnum.Some4 => "We find 4",
            _ => default
        };
    }
}
