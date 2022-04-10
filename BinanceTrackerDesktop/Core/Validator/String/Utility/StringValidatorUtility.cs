using System;
using System.Collections.Generic;
using System.Linq;

namespace BinanceTrackerDesktop.Core.Validator.String.Utility
{
    public sealed class StringValidatorUtility
    {
        public static bool IsAllSuccess(IEnumerable<StringValidator> validators)
        {
            if (validators.Any() == false)
                throw new InvalidOperationException(nameof(validators));

            return validators.All(v => v.IsSuccess);
        }

        public static bool IsAllSuccess(params StringValidator[] validators)
        {
            return IsAllSuccess(validators.ToList());
        }

        public static bool IsAllFailed(IEnumerable<StringValidator> validators)
        {
            if (validators.Any() == false)
                throw new InvalidOperationException(nameof(validators));

            return validators.All(v => v.IsFailed);
        }

        public static bool IsAllFailed(params StringValidator[] validators)
        {
            return IsAllFailed(validators.ToList());
        }
    }
}
