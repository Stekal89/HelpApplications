using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

// Not working
// https://michaelscodingspot.com/displaying-any-net-object-in-a-wpf-treeview/

namespace TreeViewExample.Models
{
    public class TreeNode
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();

        public static TreeNode CreateTree(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(serialized);
            var root = new TreeNode();
            root.Name = "Root";
            BuildTree(dic, root);
            return root;
        }

        private static void BuildTree(object item, TreeNode node)
        {
            if (item is KeyValuePair<string, object>)
            {
                KeyValuePair<string, object> kv = (KeyValuePair<string, object>)item;
                TreeNode keyValueNode = new TreeNode();
                keyValueNode.Name = kv.Key;
                keyValueNode.Value = GetValueAsString(kv.Value);
                node.Children.Add(keyValueNode);
                BuildTree(kv.Value, keyValueNode);
            }
            else if (item is ArrayList)
            {
                ArrayList list = (ArrayList)item;
                int index = 0;
                foreach (object value in list)
                {
                    TreeNode arrayItem = new TreeNode();
                    arrayItem.Name = $"[{index}]";
                    arrayItem.Value = "";
                    node.Children.Add(arrayItem);
                    BuildTree(value, arrayItem);
                    index++;
                }
            }
            else if (item is Dictionary<string, object>)
            {
                Dictionary<string, object> dictionary = (Dictionary<string, object>)item;
                foreach (KeyValuePair<string, object> d in dictionary)
                {
                    BuildTree(d, node);
                }
            }
        }

        private static string GetValueAsString(object value)
        {
            if (value == null)
                return "null";
            var type = value.GetType();
            if (type.IsArray)
            {
                return "[]";
            }

            if (value is ArrayList)
            {
                var arr = value as ArrayList;
                return $"[{arr.Count}]";
            }

            if (type.IsGenericType)
            {
                return "{}";
            }

            return value.ToString();
        }
    }
}
