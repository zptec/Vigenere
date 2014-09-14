using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigenère_wpf
{
    class Vigenere
    {
        string message;
        int keyLength;
        int messageLength;

        public Vigenere(string key, string message)
        {
            this.Key = key;
            keyLength = key.Length;
            this.message = message;
            messageLength = message.Length;
        }

        UnicodeMap[] uniMaps = new UnicodeMap[]{
            //http://zh.wikipedia.org/zh/%E8%BE%85%E5%8A%A9%E5%B9%B3%E9%9D%A2
            //http://zh.wikibooks.org/wiki/Unicode/0000-0FFF
            //= CONCATENATE("new UnicodeMap(",A1,B1,",",C1,D1,"),//",E1,F1)
            //new UnicodeMap(0x0000,0x001F),//C0控制符	C0 Controls
            //new UnicodeMap(0x0020,0x007F),//基本拉丁文Basic Latin
            new UnicodeMap(0x0021,0x002F),//'!'到'/'
            new UnicodeMap(0x0030,0x0039),//'0'到'9'
            new UnicodeMap(0x003A,0x0040),//':'到'@'
            new UnicodeMap(0x0041,0x005A),//'A'到'Z'
            new UnicodeMap(0x005B,0x0060),//'['到'`'
            new UnicodeMap(0x0061,0x007A),//'a'到'z'
            new UnicodeMap(0x007B,0x007E),//'{'到'~'
            //new UnicodeMap(0x0080,0x009F),//C1控制符C1 Controls
            new UnicodeMap(0x00A1,0x00FF),//拉丁文补充-1Latin 1 Supplement
            new UnicodeMap(0x0100,0x017F),//拉丁文扩展-ALatin Extended-A
            new UnicodeMap(0x0180,0x024F),//拉丁文扩展-BLatin Extended-B
            new UnicodeMap(0x0250,0x02AF),//国际音标扩展IPA Extensions
            new UnicodeMap(0x02B0,0x02FF),//占位修饰符号Spacing Modifiers
            new UnicodeMap(0x300,0x036F),//结合附加符号Combining Diacritics Marks
            new UnicodeMap(0x370,0x03FF),//希腊字母及科普特字母Greek and Coptic
            new UnicodeMap(0x400,0x04FF),//西里尔字母Cyrillic
            new UnicodeMap(0x500,0x052F),//西里尔字母补充Cyrillic Supplement
            new UnicodeMap(0x530,0x058F),//亚美尼亚字母Armenian
            new UnicodeMap(0x590,0x05FF),//希伯来文Hebrew
            new UnicodeMap(0x600,0x06FF),//阿拉伯文Arabic
            new UnicodeMap(0x700,0x074F),//叙利亚文Syriac
            new UnicodeMap(0x750,0x077F),//阿拉伯文补充Arabic Supplement
            new UnicodeMap(0x780,0x07BF),//它拿字母Thaana
            new UnicodeMap(0x07C0,0x07FF),//西非書面語言N'Ko
            new UnicodeMap(0x800,0x083F),//撒玛利亚字母Samaritan
            new UnicodeMap(0x840,0x085F),//曼达语Mandaic
            //new UnicodeMap(0x860,0x08FF),//待定
            new UnicodeMap(0x900,0x097F),//天城文Devanagari
            new UnicodeMap(0x980,0x09FF),//孟加拉文Bengali
            new UnicodeMap(0x0A00,0x0A7F),//果鲁穆奇字母Gurmukhi
            new UnicodeMap(0x0A80,0x0AFF),//古吉拉特文Gujarati
            new UnicodeMap(0x0B00,0x0B7F),//奥里亚文Oriya
            new UnicodeMap(0x0B80,0x0BFF),//泰米尔文Tamil
            new UnicodeMap(0x0C00,0x0C7F),//泰卢固文Telugu
            new UnicodeMap(0x0C80,0x0CFF),//卡纳达文Kannada
            new UnicodeMap(0x0D00,0x0D7F),//马拉雅拉姆文Malayalam
            new UnicodeMap(0x0D80,0x0DFF),//僧伽罗文Sinhala
            new UnicodeMap(0x0E00,0x0E7F),//泰文Thai
            new UnicodeMap(0x0E80,0x0EFF),//老挝文Lao
            new UnicodeMap(0x0F00,0x0FFF),//藏文Tibetan
            new UnicodeMap(0x1000,0x109F),//缅甸文Myanmar
            new UnicodeMap(0x10A0,0x10FF),//格鲁吉亚字母Georgian
            new UnicodeMap(0x1100,0x11FF),//諺文字母Hangul Jamo
            new UnicodeMap(0x1200,0x137F),//埃塞俄比亚语Ethiopic
            new UnicodeMap(0x1380,0x139F),//埃塞俄比亚语补充Ethiopic Supplement
            new UnicodeMap(0x13A0,0x13FF),//切罗基字母Cherokee
            new UnicodeMap(0x1400,0x167F),//统一加拿大土著语音节Unified Canadian Aboriginal Syllabics
            new UnicodeMap(0x1680,0x169F),//欧甘字母Ogham
            new UnicodeMap(0x16A0,0x16FF),//卢恩字母Runic
            new UnicodeMap(0x1700,0x171F),//他加禄字母Tagalog
            new UnicodeMap(0x1720,0x173F),//哈努诺文Hanunóo
            new UnicodeMap(0x1740,0x175F),//布迪文Buhid
            new UnicodeMap(0x1760,0x177F),//塔格巴努亚文Tagbanwa
            new UnicodeMap(0x1780,0x17FF),//高棉文Khmer
            new UnicodeMap(0x1800,0x18AF),//蒙古文Mongolian
            new UnicodeMap(0x18B0,0x18FF),//加拿大原住民音節文字扩展Unified Canadian Aboriginal Syllabics Extended
            new UnicodeMap(0x1900,0x194F),//林布文Limbu
            new UnicodeMap(0x1950,0x197F),//德宏傣文Tai Le
            new UnicodeMap(0x1980,0x19DF),//新傣仂文New Tai Lue
            new UnicodeMap(0x19E0,0x19FF),//高棉文符号Khmer Symbols
            new UnicodeMap(0x1A00,0x1A1F),//布吉文Buginese
            new UnicodeMap(0x1A20,0x1AAF),//老傣文Tai Tham
            //new UnicodeMap(0x1AB0,0x1AFF),//待定
            new UnicodeMap(0x1B00,0x1B7F),//巴厘字母Balinese
            new UnicodeMap(0x1B80,0x1BBF),//巽他字母Sundanese
            new UnicodeMap(0x1BC0,0x1BFF),//巴塔克文Batak
            new UnicodeMap(0x1C00,0x1C4F),//雷布查字母Lepcha
            new UnicodeMap(0x1C50,0x1C7F),//桑塔利文Ol Chiki
            //new UnicodeMap(0x1C80,0x1CBF),//待定
            new UnicodeMap(0x1CC0,0x1CCF),//巽他字母补充Sudanese Supplement
            new UnicodeMap(0x1CD0,0x1CFF),//吠陀梵文Vedic Extensions
            new UnicodeMap(0x1D00,0x1D7F),//语音学扩展Phonetic Extensions
            new UnicodeMap(0x1D80,0x1DBF),//语音学扩展补充Phonetic Extensions Supplement
            new UnicodeMap(0x1DC0,0x1DFF),//结合附加符号补充Combining Diacritics Marks Supplement
            new UnicodeMap(0x1E00,0x1EFF),//拉丁文扩展附加Latin Extended Additional
            new UnicodeMap(0x1F00,0x1FFF),//希腊语扩展Greek Extended
            new UnicodeMap(0x2000,0x206F),//常用标点General Punctuation
            new UnicodeMap(0x2070,0x209F),//上标及下标Superscripts and Subscripts
            new UnicodeMap(0x20A0,0x20CF),//货币符号Currency Symbols
            new UnicodeMap(0x20D0,0x20FF),//组合用记号Combining Diacritics Marks for Symbols
            new UnicodeMap(0x2100,0x214F),//字母式符号Letterlike Symbols
            new UnicodeMap(0x2150,0x218F),//数字形式Number Form
            new UnicodeMap(0x2190,0x21FF),//箭头Arrows
            new UnicodeMap(0x2200,0x22FF),//数学运算符Mathematical Operator
            new UnicodeMap(0x2300,0x23FF),//杂项工业符号Miscellaneous Technical
            new UnicodeMap(0x2400,0x243F),//控制图片Control Pictures
            new UnicodeMap(0x2440,0x245F),//光学识别符Optical Character Recognition
            new UnicodeMap(0x2460,0x24FF),//封闭式字母数字Enclosed Alphanumerics
            new UnicodeMap(0x2500,0x257F),//制表符Box Drawing
            new UnicodeMap(0x2580,0x259F),//方块元素Block Element
            new UnicodeMap(0x25A0,0x25FF),//几何图形Geometric Shapes
            new UnicodeMap(0x2600,0x26FF),//杂项符号Miscellaneous Symbols
            new UnicodeMap(0x2700,0x27BF),//印刷符号Dingbats
            new UnicodeMap(0x27C0,0x27EF),//杂项数学符号-AMiscellaneous Mathematical Symbols-A
            new UnicodeMap(0x27F0,0x27FF),//追加箭头-ASupplemental Arrows-A
            new UnicodeMap(0x2800,0x28FF),//盲文点字模型Braille Patterns
            new UnicodeMap(0x2900,0x297F),//追加箭头-BSupplemental Arrows-B
            new UnicodeMap(0x2980,0x29FF),//杂项数学符号-BMiscellaneous Mathematical Symbols-B
            new UnicodeMap(0x2A00,0x2AFF),//追加数学运算符Supplemental Mathematical Operator
            new UnicodeMap(0x2B00,0x2BFF),//杂项符号和箭头Miscellaneous Symbols and Arrows
            new UnicodeMap(0x2C00,0x2C5F),//格拉哥里字母Glagolitic
            new UnicodeMap(0x2C60,0x2C7F),//拉丁文扩展-CLatin Extended-C
            new UnicodeMap(0x2C80,0x2CFF),//科普特字母Coptic
            new UnicodeMap(0x2D00,0x2D2F),//格鲁吉亚字母补充Georgian Supplement
            new UnicodeMap(0x2D30,0x2D7F),//提非纳文Tifinagh
            new UnicodeMap(0x2D80,0x2DDF),//埃塞俄比亚语扩展Ethiopic Extended
            new UnicodeMap(0x2E00,0x2E7F),//追加标点Supplemental Punctuation
            new UnicodeMap(0x2E80,0x2EFF),//中日韩部首补充CJK Radicals Supplement
            new UnicodeMap(0x2F00,0x2FDF),//康熙部首Kangxi Radicals
            new UnicodeMap(0x2FF0,0x2FFF),//表意文字描述符Ideographic Description Characters
            new UnicodeMap(0x3000,0x303F),//中日韩符号和标点CJK Symbols and Punctuation
            new UnicodeMap(0x3040,0x309F),//日文平假名Hiragana
            new UnicodeMap(0x30A0,0x30FF),//日文片假名Katakana
            new UnicodeMap(0x3100,0x312F),//注音字母Bopomofo
            new UnicodeMap(0x3130,0x318F),//谚文兼容字母Hangul Compatibility Jamo
            new UnicodeMap(0x3190,0x319F),//象形字注释标志Kanbun
            new UnicodeMap(0x31A0,0x31BF),//注音字母扩展Bopomofo Extended
            new UnicodeMap(0x31C0,0x31EF),//中日韩笔画CJK Strokes
            new UnicodeMap(0x31F0,0x31FF),//日文片假名语音扩展Katakana Phonetic Extensions
            new UnicodeMap(0x3200,0x32FF),//带圈中日韩字母和月份Enclosed CJK Letters and Months
            new UnicodeMap(0x3300,0x33FF),//中日韩兼容CJK Compatibility
            new UnicodeMap(0x3400,0x4DBF),//中日韩统一表意文字扩展ACJK Unified Ideographs Extension A
            new UnicodeMap(0x4DC0,0x4DFF),//易经六十四卦符号Yijing Hexagrams Symbols
            new UnicodeMap(0x4E00,0x9FBB),//中日韩统一表意文字CJK Unified Ideographs
            new UnicodeMap(0xA000,0xA48F),//彝文音节Yi Syllables
            new UnicodeMap(0xA490,0xA4CF),//彝文字根Yi Radicals
            new UnicodeMap(0xA4D0,0xA4FF),//老傈僳文Lisu
            new UnicodeMap(0xA500,0xA63F),//瓦伊语Vai
            new UnicodeMap(0xA640,0xA69F),//西里尔字母扩展-BCyrillic Extended-B
            new UnicodeMap(0xA6A0,0xA6FF),//巴姆穆语Bamum
            new UnicodeMap(0xA700,0xA71F),//声调修饰字母Modifier Tone Letters
            new UnicodeMap(0xA720,0xA7FF),//拉丁文扩展-DLatin Extended-D
            new UnicodeMap(0xA800,0xA82F),//锡尔赫特文Syloti Nagri
            new UnicodeMap(0xA830,0xA83F),//Ind. No.
            new UnicodeMap(0xA840,0xA87F),//八思巴字Phags-pa
            new UnicodeMap(0xA880,0xA8DF),//索拉什特拉Saurashtra
            new UnicodeMap(0xA8E0,0xA8FF),//Deva. Ext.
            new UnicodeMap(0xA900,0xA92F),//克耶字母Kayah Li
            new UnicodeMap(0xA930,0xA95F),//勒姜语Rejang
            new UnicodeMap(0xA980,0xA9DF),//爪哇语Javanese
            //new UnicodeMap(0xA9E0,0xA9FF),//待定
            new UnicodeMap(0xAA00,0xAA5F),//占语字母Cham
            new UnicodeMap(0xAA60,0xAA7F),//缅甸语扩展Myanmar ExtA
            new UnicodeMap(0xAA80,0xAADF),//越南傣文Tai Viet
            new UnicodeMap(0xAAE0,0xAAFF),//曼尼普尔文扩展Meetei Ext
            new UnicodeMap(0xAB00,0xAB2F),//埃塞俄比亚文Ethiopic Ext-A
            //new UnicodeMap(0xAB30,0xABBF),//待定
            new UnicodeMap(0xABC0,0xABFF),//Meetei Mayek
            new UnicodeMap(0xAC00,0xD7AF),//谚文音节Hangul Syllables
            new UnicodeMap(0xD7B0,0xD7FF),//谚文字母扩展-BHangul Jamo Extended-B
            new UnicodeMap(0xD800,0xDBFF),//High-half zone of UTF-16
            new UnicodeMap(0xDC00,0xDFFF),//Low-half zone of UTF-16
            new UnicodeMap(0xE000,0xF8FF),//自行使用區域Private Use Zone
            new UnicodeMap(0xF900,0xFAFF),//中日韩兼容表意文字CJK Compatibility Ideographs
            new UnicodeMap(0xFB00,0xFB4F),//字母表達形式（拉丁字母连字、亚美尼亚字母连字、希伯来文表现形式）Alphabetic Presentation Forms
            new UnicodeMap(0xFB50,0xFDFF),//阿拉伯文表達形式AArabic Presentation Forms A
            new UnicodeMap(0xFE00,0xFE0F),//異體字选择符Variation Selector
            new UnicodeMap(0xFE10,0xFE1F),//竖排形式Vertical Forms
            new UnicodeMap(0xFE20,0xFE2F),//组合用半符号Combining Half Marks
            new UnicodeMap(0xFE30,0xFE4F),//中日韩兼容形式CJK Compatibility Forms
            new UnicodeMap(0xFE50,0xFE6F),//小寫变体形式Small Form Variants
            new UnicodeMap(0xFE70,0xFEFF),//阿拉伯文表達形式BArabic Presentation Forms B
            new UnicodeMap(0xFF00,0xFFEF),//半形及全形Halfwidth and Fullwidth Forms
            new UnicodeMap(0xFFF0,0xFFFF),//特殊Specials
        };

        public string Result { get; private set; }


        public string Key { get; private set; }

        private char crypto(char key,char origin,bool Encrypt)
        {
            int offset = 0;
            int range = 0;
            int rangeKey = 0;
            int offsetKey = 0;
            if (!UnicodeMap.CharOffset(this.uniMaps, origin, ref offset, ref range)) return origin;
            if (!UnicodeMap.CharOffset(this.uniMaps, key, ref offsetKey, ref rangeKey)) return origin;
            int messageChar = (int)origin - offset;
            int keyChar = (int)key - offsetKey;
            int newCharValue = (Encrypt ? (keyChar + messageChar) : (messageChar + range * rangeKey - keyChar)) % range;
            char newChar = (char)(newCharValue + offset);
            return newChar;
        }

        // http://en.wikipedia.org/wiki/Vigen%C3%A8re_cipher#Algebraic_description
        internal void Encrypt(bool Encrypt)
        {
            StringBuilder cryptoString = new StringBuilder();
            for (int i = 0; i < messageLength; i++)
                cryptoString.Append(crypto(Key[i % keyLength], message[i], Encrypt));
            Result = cryptoString.ToString();
        }
    }
}
