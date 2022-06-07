using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace LoadXMLFromFile.Models
{
    public class XMLFileToTreeViewMapper
    {
        private string sourceXmlFile;
        private XDocument xmlData;

        public XMLFileToTreeViewMapper(string xmlFilePath)
        {
            sourceXmlFile = xmlFilePath;
        }

        private void BuildNodes(TreeViewItem treeNode, XElement element)
        {

            string attributes = "";
            if (element.HasAttributes)
            {
                foreach (var att in element.Attributes())
                {
                    attributes += " " + att.Name + " = " + att.Value;
                }
            }

            TreeViewItem childTreeNode = new TreeViewItem
            {
                Header = element.Name.LocalName + attributes,
                IsExpanded = true
            };
            if (element.HasElements)
            {
                foreach (XElement childElement in element.Elements())
                {
                    BuildNodes(childTreeNode, childElement);
                }
            }
            else
            {
                TreeViewItem childTreeNodeText = new TreeViewItem
                {
                    Header = element.Value,
                    IsExpanded = true
                };
                childTreeNode.Items.Add(childTreeNodeText);
            }

            treeNode.Items.Add(childTreeNode);
        }



        public void LoadXml(TreeView treeview)
        {
            try
            {
                if (sourceXmlFile != null)
                {
                    xmlData = XDocument.Load(sourceXmlFile, LoadOptions.None);
                    if (xmlData == null)
                    {
                        throw new System.Xml.XmlException("Cannot load Xml document from file : " + sourceXmlFile);
                    }
                    else
                    {
                        TreeViewItem treeNode = new TreeViewItem
                        {
                            Header = sourceXmlFile,
                            IsExpanded = true
                        };


                        BuildNodes(treeNode, xmlData.Root);
                        treeview.Items.Add(treeNode);
                    }
                }
                else
                {
                    throw new System.IO.IOException("Xml file is not set correctly.");
                }
            }
            catch (System.IO.IOException ioex)
            {
                //log
                System.Diagnostics.Debug.Indent();
                System.Diagnostics.Debug.WriteLine(ioex.Message);
                System.Diagnostics.Debugger.Break();
            }
            catch (System.Xml.XmlException xmlex)
            {
                //log
                System.Diagnostics.Debug.Indent();
                System.Diagnostics.Debug.WriteLine(xmlex.Message);
                System.Diagnostics.Debugger.Break();
            }
            catch (Exception ex)
            {
                //log
                System.Diagnostics.Debug.Indent();
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debugger.Break();
            }
        }

    }
}