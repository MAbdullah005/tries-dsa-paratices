using System;
using System.Collections;
namespace Triesdsa
{
    public class Tries
    {
        private class Node
        {
            public char value;
            public Dictionary<char, Node> node = new Dictionary<char, Node>();
            public bool isendofword;
            public Node()
            {

            }
            public Node(char va)
            {
                this.value = va;
            }
            public Node[] getchildren()
            {
                return node.Values.ToArray();
            }
            public bool check_child(Node node)
            {
                return node?.value == null;
            }
            public bool has_child()
            {
                return node.Count == 0;
            }
            public void remove_child(char ch)
            {
                node.Remove(ch);
            }
            public Node getchild(char va)
            {
                return node[va];
            }
        }
        public int num = 0;
        private Node root = new Node(' ');
        public void insert(string val)
        {
            var current = root;
            foreach (var item in val)
            {
                if (!current.node.ContainsKey(item))
                {
                    current.node[item] = new Node(item);
                }
                current = current.node[item];
            }
            num++;
            current.isendofword = true;
        }
        public void Preorder()
        {
            Preorder(root);
        }
        private void Preorder(Node root)
        {
            Console.WriteLine(root.value);
            foreach (var child in root.getchildren())
            {
                Preorder(child);
            }
        }
        public bool contain(string ch)
        {
            if (ch == null) return false;
            string word = "";
            var current = root;
            foreach (var item in ch)
            {
                if (!current.node.ContainsKey(item))
                {
                    Console.WriteLine("match word " + word);
                    return false;
                }
                word += item;
                current = current.node[item];
            }
            Console.WriteLine("match word " + word);
            return current.isendofword;
        }
        public bool contai_recursive(string ch)
        {
            if (ch == null)
            {
                return false;
            }
             return contai_recursive(root,ch,0);
        }
        private bool contai_recursive(Node root,string ch,int index)
        {
            if (index == ch.Length)
            {
                Console.WriteLine("fouud word "+ch);
                return true;
            }
            if (!root.node.ContainsKey(ch[index]))
              {
                Console.WriteLine("found word ");
               for(int i = 0;i<index;i++)
                {
                    Console.Write(ch[i]+" ");
                }
                    return false;
                       
              }
            return contai_recursive(root.node[ch[index]],ch,index+1);
        }
        public void revmove(string word)
        {
            if (word == null) return;
            remove(root, word, 0, new Node());
        }
        private void remove(Node root, string word, int count, Node previos)
        {
            int i = word.Length - 1;
            foreach (var item in root.getchildren())
            {
                if (count != i + 1)
                {
                    if (item.value == word[count])
                    {
                        if (item.isendofword && count == i)
                        {
                            item.isendofword = false;
                            delete(previos);
                            Console.WriteLine("done");
                            return;
                        }
                        if (item.isendofword)
                        {
                            previos = item;
                        }
                        count++;
                    }
                }
                remove(item, word, count, previos);
            }
        }
        private void delete(Node previos)
        {
            foreach (var item in previos.getchildren())
            {
                delete(item);
                if (item.node.Count == 0 && !item.isendofword)
                {
                    previos.node.Remove(item.value);
                }
            }
            num--;
        }
        public void revove_mosh(string word)
        {
            revove_mosh(root, word, 0);
        }
        private void revove_mosh(Node node, string word, int index)
        {
            if (index == word.Length)
            {
                node.isendofword = false;
                return;
            }
            var ch = word[index];
            var child = node.getchild(ch);
            if (child == null)
            {
                return;
            }
            revove_mosh(child, word, index + 1);
            if (node.has_child() && !node.isendofword)
            {
                node.remove_child(ch);
            }
        }
        public List<string> auto_complition(string word)
        {
            List<string> strings = new List<string>();
            var item = find_word(word);
            auto_complition(item, word,strings);
            return strings;
        }
        private void auto_complition(Node root, string prefix, List<string> words)
        {
            if (root == null) return;
            if (root.isendofword)
            {
                words.Add(prefix);
            }
            foreach (var item in root.getchildren())
            {
                auto_complition(item, prefix + item.value, words);
            }
        }
        private Node find_word(string prefix)
        {
            var current = root;
            foreach (var item in prefix)
            {
                var child = current.getchild(item);
                if (child == null)
                {
                    return null!;
                }
                current = child;
            }
            return current;
        }
        public List<string> finword(string prefix)
        {
            List<string> words = new List<string>();
            var lastnode=findlastnode(prefix);
            findwords(lastnode,prefix,words);
            return words;
        }
        private void findwords(Node root,string prefix,List<string> words)
        {
            if(root==null) return;
            if(root.isendofword)
            {
                words.Add(prefix);
            }
            foreach(var item in root.getchildren())
            {
                findwords(item,prefix+item.value,words) ;
            }
        }
        private Node findlastnode(string prefix)
        {
            var current = root;
            foreach(var item in prefix)
            {
                var child=current.getchild(item);
                if(child == null)
                {
                    return null!;
                }
                current = child;
            }
            return current;
        }
        public int count_word()
        {
            return count_word(root, 0);
        }
        private int count_word(Node root,int count)
        {
            foreach(var item in root.getchildren())
            {
                if (item.isendofword)
                {
                    count++;
                    Console.WriteLine("done"+count);
                }
                count=+ count_word(item, count);
            }
            return count;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {

            Tries tries = new Tries();
            tries.insert("car");
            tries.insert("card");
            tries.insert("carefull");
            tries.insert("egg");
            tries.insert("eggs");
            Console.WriteLine("number of word "+tries.count_word());
            tries.revmove("egg");
           Console.WriteLine("number of word "+tries.count_word());
        }

    }
}
