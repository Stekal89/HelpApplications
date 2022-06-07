using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace ObjectToXmlInTreeView.Models
{
    class XMLToTreeViewMapper
    {
        private string sourceXmlFile;
        private XDocument xmlData;

        /// <summary>
        /// Default constructor
        /// </summary>
        public XMLToTreeViewMapper()
        {

        }

        /// <summary>
        /// Constrcutor with xml-FilePath
        /// </summary>
        /// <param name="xmlFilePath">File-Path of the XML-File, if the Tree will be created via File directly</param>
        public XMLToTreeViewMapper(string xmlFilePath)
        {
            sourceXmlFile = xmlFilePath;
        }

        /// <summary>
        /// Creates the Nodes of the TreeItem
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="element"></param>
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


        /// <summary>
        /// Loads a xml file and displays it in the TreeView
        /// </summary>
        /// <param name="treeview">TreeView, where the XML-Should be displayed</param>
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

        /// <summary>
        /// Converts an Object into XDocument and creates the TreeView
        /// </summary>
        /// <param name="custObject">Custom Object which should be converted</param>
        /// <param name="treeView">TreeView, where the XML-Should be displayed</param>
        public void ConvertObjectToXML(object custObject, TreeView treeView)
        {
            if (null == custObject)
            {
                System.Diagnostics.Debug.Indent();
                System.Diagnostics.Debug.WriteLine("Object is NULL!");
                System.Diagnostics.Debugger.Break();
                return;
            }
            xmlData = new XDocument();
            try
            {
                using (var writer = xmlData.CreateWriter())
                {
                    // write xml into the writer
                    var serializer = new DataContractSerializer(custObject.GetType());
                    serializer.WriteObject(writer, custObject);

                    if (serializer == null)
                    {
                        throw new System.Xml.XmlException("Cannot serialize Object!");
                    }
                        
                }

                System.Diagnostics.Debug.WriteLine($"\n{xmlData.ToString()}\n");

                TreeViewItem treeNode = new TreeViewItem
                {
                    Header = sourceXmlFile,
                    IsExpanded = true
                };


                BuildNodes(treeNode, xmlData.Root);
                treeView.Items.Add(treeNode);
                
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