using System;
using System.Collections.Generic;
using System.Linq;

namespace BinanceTrackerDesktop.Core.Validators.String.Utility
{
    public sealed class StringValidatorUtility
    {
        public static bool IsAllSuccess(IEnumerable<IStringValidator> validators)
        {
            if (validators.Any() == false)
                throw new InvalidOperationException(nameof(validators));

            return validators.All(v => v.IsSuccess);
        }

        public static bool IsAllSuccess(params IStringValidator[] validators)
        {
            return IsAllSuccess(validators.ToList());
        }

        public static bool IsAllFailed(IEnumerable<IStringValidator> validators)
        {
            if (validators.Any() == false)
                throw new InvalidOperationException(nameof(validators));

            return validators.All(v => v.IsFailed);
        }

        public static bool IsAllFailed(params IStringValidator[] validators)
        {
            return IsAllFailed(validators.ToList());
        }
    }
}
