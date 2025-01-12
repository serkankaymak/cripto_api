//#nullable enable

//public class Solution
//{
//    static bool isSubset(Dictionary<char, int> first, Dictionary<char, int> second)
//    {
//        foreach (var item in first)
//        {
//            if (!second.ContainsKey(item.Key) || second[item.Key] < item.Value)
//            {
//                return false;
//            }
//        }
//        return true;
//    }

//    static Dictionary<char, int> HarfSayaci(string metin, string? checkMetin = null)
//    {
//        var temizMetin = metin;
//        var frekanslar = new Dictionary<char, int>();

//        foreach (var harf in temizMetin)
//        {
//            if (checkMetin != null && !checkMetin.Contains(harf)) throw new Exception("Harf bulunamadı.");
//            if (frekanslar.ContainsKey(harf))
//            {
//                frekanslar[harf]++;
//            }
//            else
//            {
//                frekanslar[harf] = 1;
//            }
//        }

//        return frekanslar;
//    //}

//    public bool CloseStrings(string word1, string word2)
//    {
//        try
//        {
//            Dictionary<char, int> first = HarfSayaci(word1, word2);
//            Dictionary<char, int> second = HarfSayaci(word2, null);

//            List<int> sayilar1 = new List<int>(first.Values);
//            List<int> sayilar2 = new List<int>(second.Values);

//            ((sayilar1.Sort();
//            sayilar2.Sort();

//            İki liste aynıysa true döner
//            return sayilar1.SequenceEqual(sayilar2);
//        }
//        catch (Exception)
//        {

//            return false;

//        }



//    }
//}

