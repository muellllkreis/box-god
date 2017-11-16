using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;

public class Level {
    [XmlAttribute("name")]
    public string Name;

    public int resources;
    public int time;
}
