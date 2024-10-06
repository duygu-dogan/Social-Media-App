using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Common.Helpers;
public class SecureRandomGenerator
{
    public int Next(int minValue, int maxValue)
    {
        if(minValue > maxValue)
            throw new ArgumentOutOfRangeException(nameof(minValue), "minValue cannot be greater than maxValue");

        int range = maxValue - minValue + 1;
        byte[] randomBytes = new byte[4];

        using(RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        int randomValue = BitConverter.ToInt32(randomBytes, 0);
        randomValue = Math.Abs(randomValue % range);

        return randomValue;

    }
}
