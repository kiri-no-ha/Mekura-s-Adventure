using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("dialogue")]
public class Dialog
{
    [XmlElement("phrase")]
    public List<string> texts = new List<string>();

    public static Dialog Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Dialog));
        StringReader reader = new StringReader(_xml.text);
        Dialog dial = serializer.Deserialize(reader) as Dialog;
        return dial;
    }
}
