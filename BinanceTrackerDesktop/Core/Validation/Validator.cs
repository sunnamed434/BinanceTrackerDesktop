namespace BinanceTrackerDesktop.Core.Authorization
{
    public class Validator
    {
        private readonly string content;



        private bool success = true;



        public bool IsSuccess => success == true;



        public Validator(string content) => this.content = content;



        public Validator MinCharacters(int count)
        {
            if (content.Length < count)
                success = false;

            return this;
        }

        public Validator ContentNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(content))
                success = false;

            return this;
        }
    }
}
