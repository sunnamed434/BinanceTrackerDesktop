using ProtoBuf;

namespace BinanceTrackerDesktop.Core.Localizations.Language
{
    [ProtoContract]
    public class Language : ILanguage
    {
        public Language(Languages name, string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException(nameof(code));
            }

            Name = name;
            Code = code;
        }



        [ProtoMember(1)]
        public Languages Name { get; set;  }

        [ProtoMember(2)]
        public string Code { get; set; }
    }
}
