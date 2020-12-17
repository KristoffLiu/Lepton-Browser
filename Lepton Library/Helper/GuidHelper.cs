using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Library.Helper
{
    public class GuidHelper
    {
        public static string ConvertToGuid(Guid guid)
        {
            string guidstring = guid.ToString().ToUpper();
            string sVar = "";
            int i;

            foreach (string sv in new string[] {
                    guidstring.Substring(0, 8), guidstring.Substring(9, 4), guidstring.Substring(14, 4) })
            {
                for (i = sv.Length - 2; i >= 0; i -= 2)
                {
                    sVar += sv.Substring(i, 2);
                }
            }

            guidstring = guidstring.Substring(19).Replace("-", "");
            for (i = 0; i < 8; i++)
            {
                sVar += guidstring.Substring(2 * i, 2);
            }

            return sVar;
        }
    }
}
