// using System;
// using System.Collections.Generic;
// using System.Linq;
//
// namespace KasperTask {
//     public class MainClass {
//         public static void Main(string[] args) {
//             Console.WriteLine(prev_lesser(Console.ReadLine()));
//         }
//         
//         public static string prev_lesser(string s) {
//             char[] chArray = s.ToCharArray();
//             int length = chArray.Length;
//             List<char> buf = new List<char>();
//             List<char> sortedTail = new List<char>();
//             int res = -1;
//             for (int i = length - 1; i > 0; i--) {
//                 for (int k = i - 1; k >= 0; k--) {
//                     if (chArray[k] > chArray[i]) {
//                         buf.Clear();
//                         buf = new List<char>(chArray);
//                         (buf[k], buf[i]) = (buf[i], buf[k]);
//                         if (buf[0] > '0') {
//                             sortedTail.Clear();
//                             sortedTail = new List<char>(buf.GetRange(k + 1, length - k - 1));
//                             sortedTail = sortedTail.OrderByDescending(x => x).ToList();
//                             buf = new List<char>(buf.GetRange(0, k + 1).Concat(sortedTail));
//                             res = Math.Max(res, Int32.Parse(new string(buf.ToArray())));
//                         }
//                         break;
//                     }
//                 }
//             }
//             return res.ToString();
//         }
//     }
// }