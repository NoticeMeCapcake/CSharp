using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KasperTask {


    public class MainClass {
        
        // private class BaseClass {
            // public void PrintFirst() => Console.Write("42; ");
            // public void PrintSecond() => Console.Write("Second Value; ");
        // }
        
        // private class InheritedClass : BaseClass {
            // public void PrintFirst() => Console.Write("First Value; ");
            // public void PrintSecond() => Console.Write("999; ");
        // }
        
        public class SomeClass {
            public int Value { get; set; }

            public static bool operator ==(SomeClass a, SomeClass b) {
                if (a is null || b is null) {
                    return true;
                }
                return a.Value == b.Value;
            }
            
            public static bool operator !=(SomeClass a, SomeClass b) {
                return !(a == b); 
            }
        }

        private static bool First() {
            Console.Write("1");
            return true;
        }
        
        private static bool Second() {
            Console.Write("2");
            return false;
        }
        
        public static void Main() {
            var bytes = new byte[5];

            unsafe {
                fixed (byte* ptr = bytes) {
                    var uintPtr = (uint*)ptr;
                    *uintPtr = 0xdf66e913u;
                }
            }

            foreach (var b in bytes) {
                Console.Write(Convert.ToString(b, 16));
            }



            // Console.WriteLine((IsValid(Console.ReadLine())? "Success" : "Fail"));
        }
        
        public static bool IsValid(string s) {
            var stack = new Stack<char>();
            bool sticks = false;
            foreach (var c in s) {
                switch (c) {
                    case '{':
                    case '(':
                    case '[':
                        stack.Push(c);
                        break;

                    case '|':
                        if (!sticks) {
                            stack.Push(c);
                            sticks = true;
                        }
                        else {
                            if (stack.Count == 0) return false;
                            if (stack.Pop() != '|') return false;
                            sticks = false;
                        }

                        break;
                    case '}':
                        if (stack.Count == 0) return false;
                        if (stack.Pop() != '{') return false;
                        break;
                    case ']':
                        if (stack.Count == 0) return false;
                        if (stack.Pop() != '[') return false;
                        break;
                    case ')':
                        if (stack.Count == 0) return false;
                        if (stack.Pop() != '(') return false;
                        break;
                }
            }

            return stack.Count == 0;
        }
    }

    public class Smth {
        public readonly int i = 0;

        public Smth(int a) {
            i = a;
            i = 2;
            i = 3;
        }

        // public void Sas() {
            // i = 3;
        // }
    }
}