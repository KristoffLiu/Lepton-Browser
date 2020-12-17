
/*
 * ⚠
 * Warning: 以下部分代码是我在喝了150ml百利甜酒和半瓶红酒后以六亲不认的步伐飞到电脑前写的
 *                                                                  --GrayNekoBean
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Library.Helper
{
    public class LanguageHelper
    {

        public static readonly int ENGLISH_WORD_MAX_LENGTH = 45;
        public static readonly int CHINESE_WORD_MAX_LENGTH = 8;

        struct Boundary {
            public UInt32 start;
            public UInt32 end;
            public Boundary(UInt32 _start, UInt32 _end)
            {
                this.start = _start;
                this.end = _end;
            }
        }

        private static Dictionary<Boundary, Language> UnicodeBoundaries = new Dictionary<Boundary, Language>()
        {

            { new Boundary(65, 90), Language.EN },        //Basic Lattin Capital Letter
            { new Boundary(97, 122), Language.EN },       //Basic Lattin Small Letter
            { new Boundary(0x400, 0x4FF), Language.RU },  //Cyrillic Letter
            { new Boundary(0x1100, 0x11FF), Language.KO },//Hangul Jamo
            { new Boundary(0x3040, 0x31FF), Language.JP },//Hiragana & Katakana
            { new Boundary(0x3130, 0x318F), Language.KO },//Hangul Compatibility Jamo
            { new Boundary(0x3400, 0x4DB5), Language.CN },//CJK extension
            { new Boundary(0x4E00, 0x9FEF), Language.CN },//CJK Unified Ideographs
            { new Boundary(0xA960, 0xA97F), Language.KO },
            { new Boundary(0xAC00, 0xD7AF), Language.KO },//Hangul Syllables
            { new Boundary(0xD7B0, 0xD7FF), Language.KO },
            { new Boundary(0x20000, 0x2EBE0), Language.CN }//CJK supplement A-F

        };

        private static Dictionary<Language, string> languageNames = new Dictionary<Language, string>()
        {
            { Language.CN, "中文" },
            { Language.EN, "英语（English）" },
            { Language.JP, "日语（日本語）" },
            { Language.KO, "韩语（한국어）" },
            { Language.FR, "法语（Français）" },
            { Language.DE, "德语（Deutsch）" },
            { Language.PT, "葡萄牙语（Português）" },
            { Language.HU, "匈牙利语（MagyarName）" },
            { Language.RU, "俄语（русский язык）" },
            { Language.ES, "西班牙语（Español）" }
        };

        private static Dictionary<string, Language> langCodes = new Dictionary<string, Language>()
        {
            { "zh-cn", Language.CN },
            { "en", Language.EN },
        };
        private static string[] codes = langCodes.Keys.ToArray();

        public static string GetLanguageName(Language lang)
        {
            return languageNames[lang];
        }

        public static bool IsLanguage(string text, Language language)
        {
            return _CheckLanguage(text).Equals(language);
        }

        public static string ConvertToLangCode(Language language)
        {
            return codes[(int)language];
        }

        public static Language ConvertFromLangCode(string langCode)
        {
            return langCodes[langCode];
        }

        public static Language _CheckLanguage(string text)
        {
            Language PrioCharLanguage = Language.EN;
            bool ContainsCN = false, 
                 ContainsEN = false, 
                 ContainsOther = false;
            int[] CharCount = new int[Enum.GetValues(typeof(Language)).Length];
            for (int i = 0; i < CharCount.Length; i++)
                CharCount[i] = 0;

            foreach (char c in text) {
                foreach (var bound in UnicodeBoundaries.Keys)
                {
                    if ((UInt32)c <= bound.end)
                    {
                        if((UInt32)c >= bound.start)
                        {
                            Language l = UnicodeBoundaries[bound];

                            if (l == Language.CN)
                            {
                                ContainsCN = true;
                                break;
                            }
                            else if (l == Language.EN)
                            {
                                ContainsEN = true;
                                break;
                            }
                            else
                                ContainsOther = true;

                            int count = CharCount[(int)l];
                            count++;
                            CharCount[(int)l] = count;
                            if (count >= CharCount.Max())
                            {
                                PrioCharLanguage = l;
                            }
                            break;
                        }
                    }
                }
            }

            if (ContainsCN)
            {
                    if (ContainsOther)
                        return PrioCharLanguage;
                    else
                        return Language.CN;
            }
            else
            {
                if (ContainsEN)
                {
                    if (ContainsOther)
                        return PrioCharLanguage;
                    else
                        return Language.EN;
                }
                else
                {
                    return PrioCharLanguage;
                }
            }
        }

        public static bool IsValidWord(string word)
        {

            if (word.Contains('\n') || word.Contains(',') || word.Contains(';') || word.Contains('，') || word.Contains('。') || word.Length > ENGLISH_WORD_MAX_LENGTH * 2 + 1) //the longest English word in the world is "pneumonoultramicroscopicsilicovolcanoconiosis", which has 45 characters
            {
                return false;
            }

            Language language = _CheckLanguage(word);
            if (language == Language.EN)
            {
                if (word.Split(' ').Length > 2)
                {
                    return false;
                }
            }
            else if (language == Language.CN)
            {
                if (word.Length > CHINESE_WORD_MAX_LENGTH)
                {
                    return false;
                }
            }
            else
            {
                //temp code, I'm not sure about other languages
                if (word.Length > 15)
                {
                    return false;
                }
            }
            return true;
        }

        public static string CheckLanguage(string _word)
        {
            if (IsEnglish(_word[0]))
            {
                return "en";
            }
            else if (IsChinese(_word[0]))
            {
                return "zh-cn";
            }
            //继续扩展 else if()
            return "en";
        }

        public static bool IsEnglish(char _char)
        {
            bool isenglish = false;
            if (_char < 127)
                isenglish = true;
            else
                isenglish = false;
            return isenglish;
        }

        public static bool IsChinese(char _char)
        {
            bool ischinese = false;
            char[] c = _char.ToString().ToCharArray();
            if (c[0] >= 0x4e00 && c[0] <= 0x9fbb)
                ischinese = true;
            else
                ischinese = false;
            return ischinese;
        }
    }

    public enum Language
    {

        CN = 0,
        /* 
        * For the reason that code of simplified Chinese and code of traditional Chinese are not continuos in Unicode,
        * So that the two below will not be use until there's a solution. (Such as convert them to GBK or something else,
        *                                                                  I'm not sure yet)
        *                                                                                           --GrayNekoBean
        */
        //CN_splf,
        //CN_trad,

        EN = 1,
        /* 
         * these two below will also not be use recently(No way to distinguish them and meaningless,
         * but still keep them there) 
         */
        //EN_uk,
        //EN_us,

        JP = 2, //Japanese
        KO = 3, //Korean
        FR = 4, //France
        DE = 5, //Germany
        PT = 6, //Portuese
        HU = 7, //Hungary
        RU = 8, //Russian
        ES = 9  //Spanish
    }
}
