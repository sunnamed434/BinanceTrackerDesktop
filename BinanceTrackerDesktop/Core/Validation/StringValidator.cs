namespace BinanceTrackerDesktop.Core.Validation
{
    public class StringValidator
    {
        private readonly string content;



        private bool success = true;



        public bool IsSuccess => success == true;



        public StringValidator(string content) => this.content = content;



        public StringValidator MinCharacters(int count)
        {
            if (content.Length < count)
                success = false;

            return this;
        }

        public StringValidator ContentNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(content))
                success = false;

            return this;
        }
    }
}
