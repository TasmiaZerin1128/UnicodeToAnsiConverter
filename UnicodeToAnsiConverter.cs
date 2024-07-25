using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UnicodeAnsiConvert : MonoBehaviour
{

    private static readonly Dictionary<string, string> PRE_CONVERSION_MAP = new Dictionary<string, string>
    {
        { " ", " +" },
        { "y", "yy" },
        { "v", "vv" },
        { "„", "„„" },
        { "­", "­­" },
        { "u‡", "‡u" },
        { "uw", "wu" },
        { ",", " ," },
        { "\\|", " \\|" },
        { "\n", "\n +" },
        { "\n\n", "\n\n\n" },
    };

    private static readonly Dictionary<string, string> UNICODE_ANSI_MAP = new Dictionary<string, string>
    {
        { "ক্ক", "°" }, { "ক্ট", "±" }, { "ক্ষ্ণ", "²" }, { "ক্ত", "³" }, { "ক্ম", "´" }, { "ক্র", "µ" }, { "ক্ষ", "¶" },
        { "ক্স", "·" }, { "গু", "¸" }, { "গ্দ", "º" }, { "গ্ধ", "»" }, { "ঙ্ক", "¼" }, { "ঙ্গ", "½" }, { "জ্জ", "¾" },
        { "্ত্র", "¿" }, { "জ্ঝ", "À" }, { "জ্ঞ", "Á" }, { "ঞ্চ", "Â" }, { "ঞ্ছ", "Ã" }, { "ঞ্জ", "Ä" }, { "ঞ্ঝ", "Å" },
        { "ট্ট", "Æ" }, { "ড্ড", "Ç" }, { "ণ্ট", "È" }, { "ণ্ঠ", "É" }, { "ণ্ড", "Ê" }, { "ত্ত", "Ë" }, { "ত্থ", "Ì" },
        { "ত্র", "Î" }, { "দ্দ", "Ï" }, { "-", "Ñ" }, { "\"", "Ò" }, { "'", "Ô" }, { "দ্ধ", "×" }, { "দ্ব", "Ø" },
        { "দ্ম", "Ù" }, { "ন্ঠ", "Ú" }, { "ন্ড", "Û" }, { "ন্ধ", "Ü" }, { "ন্স", "Ý" }, { "প্ট", "Þ" }, { "প্ত", "ß" },
        { "প্প", "à" }, { "প্স", "á" }, { "ব্জ", "â" }, { "ব্দ", "ã" }, { "ব্ধ", "ä" }, { "ভ্র", "å" }, { "ম্ফ", "ç" },
        { "ল্ক", "é" }, { "ল্গ", "ê" }, { "ল্ট", "ë" }, { "ল্ড", "ì" }, { "ল্প", "í" }, { "ল্ফ", "î" }, { "শু", "ï" },
        { "শ্চ", "ð" }, { "শ্ছ", "ñ" }, { "ষ্ণ", "ò" }, { "ষ্ট", "ó" }, { "ষ্ঠ", "ô" }, { "ষ্ফ", "õ" }, { "স্খ", "ö" },
        { "স্ট", "÷" }, { "স্ন", "ø" }, { "স্ফ", "ù" }, { "হু", "û" }, { "হৃ", "ü" }, { "হ্ন", "ý" }, { "হ্ম", "þ" },
        { "ন্ব", "š^" }, 
        { "অ", "A" }, { "আ", "Av" }, { "ই", "B" }, { "ঈ", "C" }, { "উ", "D" }, { "ঊ", "E" }, { "ঋ", "F" },
        { "এ", "G" }, { "ঐ", "H" }, { "ও", "I" }, { "ঔ", "J" }, { "ক", "K" }, { "খ", "L" }, { "গ", "M" },
        { "ঘ", "N" }, { "ঙ", "O" }, { "চ", "P" }, { "ছ", "Q" }, { "জ", "R" }, { "ঝ", "S" }, { "ঞ", "T" },
        { "ট", "U" }, { "ঠ", "V" }, { "ড", "W" }, { "ঢ", "X" }, { "ণ", "Y" }, { "ত", "Z" }, { "থ", "_" },
        { "দ", "`" }, { "ধ", "a" }, { "ন", "b" }, { "প", "c" }, { "ফ", "d" }, { "ব", "e" }, { "ভ", "f" },
        { "ম", "g" }, { "য", "h" }, { "র", "i" }, { "ল", "j" }, { "শ", "k" }, { "ষ", "l" }, { "স", "m" },
        { "হ", "n" }, { "ড়", "o" }, { "ঢ়", "p" }, { "য়", "q" }, { "ৎ", "r" }, 
        { "ং", "s" }, { "ঃ", "t" }, { "ঁ", "u" },
        { "০", "0" }, { "১", "1" }, { "২", "2" }, { "৩", "3" }, { "৪", "4" }, { "৫", "5" }, { "৬", "6" },
        { "৭", "7" }, { "৮", "8" }, { "৯", "9" },
        { "ঙ্", "•" }, { "।", "|" },
    };

    private static readonly Dictionary<string, string> PRE_SYMBOLS_MAP = new Dictionary<string, string>
    {
        { "ষ্", "®" },
        { "স্", "¯" },
        { "চ্", "”" },
        { "দ্", "˜" },
        { "ন্", "š" },
        { "ম্", "¤" },
    };

    private static readonly Dictionary<string, string> REFF = new Dictionary<string, string>
    {
        { "র্", "©" },
    };

    private static readonly Dictionary<string, string> POST_SYMBOLS_MAP = new Dictionary<string, string>
    {
        { "্‌", "&" },
        { "্প", "ú" },
        { "্ন", "è" },
        { "্ব", "^" },
        { "্তু", "‘" },
        { "্থ", "’" },
        { "্ক", "‹" },
        { "্ক্র", "Œ" },
        { "্ত", "—" },
        { "্ভ", "¢" },
        { "্ভ্র", "£" },
        { "্ম", "¥" },
        { "্য", "¨" },
        { "্ল", "¬" },
        { "্র", "Ö" },
    };

    private static readonly Dictionary<string, string> KAARS = new Dictionary<string, string>
    {
        { "া", "v" },
        { "ি", "w" },
        { "ী", "x" },
        { "ু", "y" },
        { "ূ", "~" },
        { "ৃ", "…" },
        { "ে", "‡" },
        { "ৈ", "ˆ" },
        { "ৗ", "Š" },
    };

    private static readonly Dictionary<string, string> O_KAAR_OU_KAAR = new Dictionary<string, string>
    {
        { "ো", "‡v" },
        { "ৌ", "‡Š" },
    };

    public void Convert()
    {
        string dummyText = "গ্রীক দার্শনিকরা নিচের কোনটিকে মৌলিক পদার্থ হিসেবে চিহ্নিত করেছেন?";
        string ansiText = UnicodeToAnsi(dummyText);
    }

    public static string UnicodeToAnsi(string unicodeText)
    {
        string ansiText = "";
        int i = 0;
        bool found;
        bool hasReff = false;
        while (i < unicodeText.Length)
        {
            found = false;
            if (i + 3 <= unicodeText.Length) // Check for 3 character unicode (Jukto borno (গ্র))
            {
                (ansiText, found, hasReff) = GetJuktoBornoAnsiValues(unicodeText, 3, found, i, ansiText, hasReff);
                if (found) i += 3;
            }
            if (!found && (i + 2 <= unicodeText.Length)) // Check for 2 character unicode (Jukto borno (ক্ক))
            {
                (ansiText, found, hasReff) = GetJuktoBornoAnsiValues(unicodeText, 2, found, i, ansiText, hasReff);
                if (found) i += 2;
            }
            if (!found)  // Check for 1 character unicode (Okkhor)
            {
                (ansiText, found, hasReff) = GetSingleAnsiValues(unicodeText, i, ansiText, hasReff);
                if (found) i++;
            }
            if (!found)
            {
                ansiText += unicodeText[i];
                i++;
            }
        }

        return ansiText;
    }

    private static (string ansiText, bool found, bool hasReff) GetJuktoBornoAnsiValues(string unicodeText, int length, bool found, int index, string ansiText, bool hasReff)
    {
        if (UNICODE_ANSI_MAP.TryGetValue(unicodeText.Substring(index, length), out string ansiChr))
        {
            ansiText += ansiChr;
            found = true;
        }
        else if (KAARS.TryGetValue(unicodeText.Substring(index, length), out string ansiKaar))
        {
            ansiText += ansiKaar;
            found = true;
        }
        else if (POST_SYMBOLS_MAP.TryGetValue(unicodeText.Substring(index, length), out string ansiSymbol))
        {
            ansiText += ansiSymbol;
            found = true;
        }
        else if (REFF.TryGetValue(unicodeText.Substring(index, length), out string ansiReff))
        {
            ansiText += ansiReff;
            found = true;
            hasReff = true;
        }

        return (ansiText, found, hasReff);
    }

    private static (string ansiText, bool found, bool hasReff) GetSingleAnsiValues(string unicodeText, int index, string ansiText, bool hasReff)
    {
        bool found = false;

        if (UNICODE_ANSI_MAP.TryGetValue(unicodeText[index].ToString(), out string ansiChar))
        {
            if (hasReff)
            {
                ansiText = SwapCharWithKaar(ansiText, ansiChar, false);
                hasReff = false;
            }
            else
            {
                ansiText += ansiChar;
            }
            found = true;
        }
        else if (KAARS.TryGetValue(unicodeText[index].ToString(), out string ansiKaar))
        {
            Debug.Log("Kaar " + ansiKaar);
            if (ansiKaar == "v" || ansiKaar == "x" || ansiKaar == "Š" || ansiKaar == "y" || ansiKaar == "~" || ansiKaar == "…")     // আ-কার, ঈ-কার, উ-কার, ঊ-কার, ঋ-কার পরে বসবে
            {
                ansiText += ansiKaar;
            }
            else   // ই-কার, ঐ-কার আগে বসবে, switch
            {
                ansiText = SwapCharWithKaar(ansiText, ansiKaar, false);
            }
            found = true;
        }
        else if (POST_SYMBOLS_MAP.TryGetValue(unicodeText[index].ToString(), out string ansiSymbol))
        {
            Debug.Log("Symbol " + ansiSymbol);
            ansiText += ansiSymbol;
            found = true;
        }
        else if (O_KAAR_OU_KAAR.TryGetValue(unicodeText[index].ToString(), out string ansiComplexKaar))  // ো কার, ৌ কার
        {
            ansiText = SwapCharWithKaar(ansiText, ansiComplexKaar, true);
            found = true;
        }

        return (ansiText, found, hasReff);
    }

    private static string SwapCharWithKaar(string ansiText, string ansiKaarOrReff, bool complexChar)
    {
        char temp = ansiText.Last();
        ansiText = ansiText.Remove(ansiText.Length - 1, 1);
        ansiText += ansiKaarOrReff[0];
        ansiText += temp;
        if (complexChar)
        {
            ansiText += ansiKaarOrReff[1];
        }
        return ansiText;
    }

}
